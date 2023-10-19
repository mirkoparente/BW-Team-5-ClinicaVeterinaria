using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BW_Team_5_ClinicaVeterinaria.Models;

namespace BW_Team_5_ClinicaVeterinaria.Controllers
{
    public class OrdiniController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        public ActionResult AggiungiAlCarrello(int id)
        {
            Prodotti prodottoSelezionato = db.Prodotti.Find(id);
            if (Session["Carrello"]==null)
            {
                List<Prodotti> carrello = new List<Prodotti>();
                carrello.Add(prodottoSelezionato);
                Session["Carrello"]=carrello;
            }
            else
            {
                List<Prodotti> carrello = Session["Carrello"] as List<Prodotti>;
                carrello.Add(prodottoSelezionato);
                Session["Carrello"]= carrello;
            }
            return RedirectToAction("CercaProdotti", "Prodotti");
        }

        public ActionResult Carrello()
        {
            List<Prodotti> carrello;
            if (Session["Carrello"] != null)
            {
                carrello = Session["Carrello"] as List<Prodotti>;
            }
            else
            {
                carrello = new List<Prodotti> ();
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

        // GET: Ordini
        public ActionResult Index()
        {
            var ordini = db.Ordini.Include(o => o.Clienti);
            return View(ordini.ToList());
        }

        // GET: Ordini/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordinis ordinis = db.Ordini.Find(id);
            if (ordinis == null)
            {
                return HttpNotFound();
            }
            return View(ordinis);
        }

        // GET: Ordini/Create
        public ActionResult Create()
        {
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome");
            return View();
        }

        // POST: Ordini/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrdini,DataOrdine,IdClienti,TotaleOrdine")] Ordinis ordinis)
        {
            if (ModelState.IsValid)
            {
                db.Ordini.Add(ordinis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome", ordinis.IdClienti);
            return View(ordinis);
        }

        // GET: Ordini/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordinis ordinis = db.Ordini.Find(id);
            if (ordinis == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome", ordinis.IdClienti);
            return View(ordinis);
        }

        // POST: Ordini/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrdini,DataOrdine,IdClienti,TotaleOrdine")] Ordinis ordinis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordinis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome", ordinis.IdClienti);
            return View(ordinis);
        }

        // GET: Ordini/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordinis ordinis = db.Ordini.Find(id);
            if (ordinis == null)
            {
                return HttpNotFound();
            }
            return View(ordinis);
        }

        // POST: Ordini/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ordinis ordinis = db.Ordini.Find(id);
            db.Ordini.Remove(ordinis);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
