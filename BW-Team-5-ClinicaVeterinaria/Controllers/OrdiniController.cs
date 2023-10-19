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
                quantita);

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

        [HttpPost]
        public ActionResult Carrello(List<Prodotti> carrello)
        {
            carrello = Session["Carrello"] as List<Prodotti>;
            return View(carrello);
        }
    }
}

      