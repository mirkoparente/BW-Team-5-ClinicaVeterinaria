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
    public class TipoPazienteController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // GET TipoPaziente
        public ActionResult Index()
        {
            return View(db.TipoPaziente.ToList());
        }

        // GET Dettagli TipoPaziente
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPaziente tipoPaziente = db.TipoPaziente.Find(id);
            if (tipoPaziente == null)
            {
                return HttpNotFound();
            }
            return View(tipoPaziente);
        }

        // GET Crea TipoPaziente
        public ActionResult Create()
        {
            return View();
        }

        // POST Crea TipoPaziente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipo,Tipologia")] TipoPaziente tipoPaziente)
        {
            if (ModelState.IsValid)
            {
                db.TipoPaziente.Add(tipoPaziente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoPaziente);
        }

        // GET Modifica TipoPaziente
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPaziente tipoPaziente = db.TipoPaziente.Find(id);
            if (tipoPaziente == null)
            {
                return HttpNotFound();
            }
            return View(tipoPaziente);
        }

        // POST Modifica TipoPaziente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipo,Tipologia")] TipoPaziente tipoPaziente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPaziente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoPaziente);
        }

        // GET Elimina TipoPaziente
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPaziente tipoPaziente = db.TipoPaziente.Find(id);
            if (tipoPaziente == null)
            {
                return HttpNotFound();
            }
            return View(tipoPaziente);
        }

        // POST Elimina TipoPaziente
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPaziente tipoPaziente = db.TipoPaziente.Find(id);
            db.TipoPaziente.Remove(tipoPaziente);
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
