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
            Prodotti p = db.Prodotti.Find(id);
            ProdottoCarrello prdCar = new ProdottoCarrello(
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

            if (Session["Carrello"] == null)
            {
                List<ProdottoCarrello> carrello = new List<ProdottoCarrello>();
                carrello.Add(prdCar);
                Session["Carrello"] = carrello;
            }
            else
            {
                List<ProdottoCarrello> carrello = Session["Carrello"] as List<ProdottoCarrello>;
                carrello.Add(prdCar);
                Session["Carrello"] = carrello;
            }

            var response = new
            {
                Status = true,
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Carrello()
        {
            List<ProdottoCarrello> carrello;
            if (Session["Carrello"] != null)
            {
                carrello = Session["Carrello"] as List<ProdottoCarrello>;
            }
            else
            {
                carrello = new List<ProdottoCarrello>();
            }

            ViewBag.Carrello = carrello;

            return View();
        }

        
        public ActionResult Checkout(string cf, string ricetta)
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

            return View();
        }


        public ActionResult editPrdCarello (int id, int quantita)
        {
            List<ProdottoCarrello> carrello = Session["Carrello"] as List<ProdottoCarrello>;
            foreach (var i in carrello)
            {
                if(id == i.IdProdotti) 
                {
                    i.QuantitaAcquistata = quantita;
                                  
                }
                
            }
            Session["Carrello"]=carrello;


            return Json(carrello, JsonRequestBehavior.AllowGet);


        }


    }
}

      