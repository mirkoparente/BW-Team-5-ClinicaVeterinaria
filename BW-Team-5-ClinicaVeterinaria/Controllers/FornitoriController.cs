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
    public class FornitoriController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // GET: Fornitori
        public ActionResult Index()
        {
            return View(db.Fornitori.ToList());
        }

        // GET Dettagli fornitore
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornitori fornitori = db.Fornitori.Find(id);
            if (fornitori == null)
            {
                return HttpNotFound();
            }
            return View(fornitori);
        }

        // GET Crea fornitore
        public ActionResult Create()
        {
            return View();
        }

        // POST Crea fornitore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RagioneSociale,Indirizzo,Piva,Citta,Recapito")] Fornitori fornitori)
        {
            if (ModelState.IsValid)
            {
                db.Fornitori.Add(fornitori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fornitori);
        }

        // GET Modifica fornitore
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornitori fornitori = db.Fornitori.Find(id);
            if (fornitori == null)
            {
                return HttpNotFound();
            }
            return View(fornitori);
        }

        // POST Modifica fornitore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFornitori,RagioneSociale,Indirizzo,Piva,Citta,Recapito")] Fornitori fornitori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fornitori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornitori);
        }

        // GET Cancella fornitore
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornitori fornitori = db.Fornitori.Find(id);
            if (fornitori == null)
            {
                return HttpNotFound();
            }
            return View(fornitori);
        }

        // POST Cancella fornitore
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fornitori fornitori = db.Fornitori.Find(id);
            db.Fornitori.Remove(fornitori);
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
