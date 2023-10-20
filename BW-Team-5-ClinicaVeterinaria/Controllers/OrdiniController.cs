using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BW_Team_5_ClinicaVeterinaria.Models;
using BW_Team_5_ClinicaVeterinaria.Models.Classi;
namespace BW_Team_5_ClinicaVeterinaria.Controllers
{
    public class OrdiniController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        public ActionResult AggiungiAlCarrello(int id, int quantita)
        {
            List<ProdottoCarrello> carrello;
            Prodotti p = db.Prodotti.Find(id);
            ProdottoCarrello prdCar;
            bool IsSession = false;
            bool newProduct = true;

            if (Session["Carrello"] == null)
            {
                carrello = new List<ProdottoCarrello>();

            }
            else
            {
                carrello = Session["Carrello"] as List<ProdottoCarrello>;
                IsSession = true;
            }

            if(IsSession) 
            { 
                foreach (var i in carrello)
                {
                    if (i.IdProdotti == id)
                    {
                        i.QuantitaAcquistata += quantita;
                        newProduct = false;

                        break;
                    }
                }
            }

            if (newProduct) 
            {
                prdCar = new ProdottoCarrello(
                   p.IdProdotti,
                   p.Nome,
                   p.Descrizione,
                   p.QuantitaDisponibile,
                   p.PrezzoUnitario,
                   p.IdFornitori,
                   p.IdCategoria,
                   p.IdCassetti,
                   quantita,
                   p.FotoProdotto);

                carrello.Add(prdCar);

            }

                Session["Carrello"] = carrello;

               

           var response = new
            {
                Status = true,
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Carrello()
        {
            List<ProdottoCarrello> carrello;
                decimal Totale = 0;
            if (Session["Carrello"] != null)
            {
                carrello = Session["Carrello"] as List<ProdottoCarrello>;
                foreach (var e in carrello)
                {
                    Totale += (e.QuantitaAcquistata * e.PrezzoUnitario);

                }
                ViewBag.Totale = Totale;
            }
            else
            {
                carrello = new List<ProdottoCarrello>();
                ViewBag.Totale = Totale;
            }

            ViewBag.Carrello = carrello;

            return View();
        }

        
        public ActionResult Checkout(string cf, string ricetta)
        {
            bool cfIsValid = false;
            List<Clienti> clienti= db.Clienti.ToList();
            foreach (var item in clienti)
            {
                if(item.CodiceFiscale.ToLower() == cf.ToLower())
                { 
                    cfIsValid = true;
                    break;
                }
            }

            if(cfIsValid)
            {
    
            Ordini ordine = new Ordini();
            ordine.DataOrdine=DateTime.Now;
            Clienti Cli = db.Clienti.FirstOrDefault(e=>e.CodiceFiscale==cf);
            ordine.IdClienti= Cli.IdClienti;

            decimal Totale = 0;
            List<ProdottoCarrello> carrello = Session["Carrello"] as List<ProdottoCarrello>;
            foreach (var e in carrello)
            {
                Totale += (e.QuantitaAcquistata * e.PrezzoUnitario);
              
            }
            ordine.TotaleOrdine = Totale;

            db.Ordini.Add(ordine);
            db.SaveChanges();

            foreach (var i in carrello)
            {
                ProdottiAcquistati prd = new ProdottiAcquistati();
                prd.IdProdotti = i.IdProdotti;
                prd.IdOrdini = ordine.IdOrdini;
                prd.Quantita = i.QuantitaAcquistata;
                if(ricetta != null) 
                { 
                    prd.NumeroRicetta = ricetta;
                }
                prd.Totale = (i.QuantitaAcquistata*i.PrezzoUnitario);
                db.ProdottiAcquistati.Add(prd);
                db.SaveChanges();
            }
            db.Dispose();
            Session.Clear();
                var respone = new
                {
                    statusCF = true,

                };

                return Json(respone, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var respone = new
                {
                    statusCF = false,

                };

                return Json (respone,JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult editPrdCarello (int id, int quantita)
        {
            List<ProdottoCarrello> carrello = Session["Carrello"] as List<ProdottoCarrello>;
            decimal Totale = 0;
            foreach (var i in carrello)
            {
                if (id == i.IdProdotti) 
                {
                    i.QuantitaAcquistata = quantita;
                                  
                }
                
                Totale += (i.QuantitaAcquistata * i.PrezzoUnitario);
            }
            Session["Carrello"]=carrello;
            ViewBag.Totale = Totale;


            return Json(carrello, JsonRequestBehavior.AllowGet);


        }

        public ActionResult ListaOrdini()
        {
            return View(db.Ordini.ToList());
        }

        [HttpGet]
        public JsonResult Lista(DateTime data)
        {
            DateTime D = data.Date;
            List<Ordini> medicinaliInData = db.Ordini
                .Where(m => DbFunctions.TruncateTime(m.DataOrdine) == D)
                .ToList();

            return Json(medicinaliInData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MedByCf (string cf) 
        { 
            List<Ordini> ordini = db.Ordini.Where(e=> e.Clienti.CodiceFiscale.ToLower() == cf.ToLower()).ToList();

            var result = ordini.Select(e => new
            {
                Nome = e.Clienti.Nome,
                Cognome = e.Clienti.Cognome,
                DataOrdine = e.DataOrdine,
                Totale = e.TotaleOrdine,
                Articoli =
                e.ProdottiAcquistati.Select(i => new
                {
                    Nome = i.Prodotti.Nome,
                    quantita = i.Quantita,
                    prezzo = i.Totale

                }).ToArray()

            }).ToArray();
  
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}

      