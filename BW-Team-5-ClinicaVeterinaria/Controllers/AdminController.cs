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
    public class AdminController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // GET: Admin
        public ActionResult Index()
        {
            List<Ruoli> ruoli = db.Ruoli.ToList();
            List<SelectListItem> dropRuoli = ruoli.Select(e => new SelectListItem
            {
                Value = e.IdRuoli.ToString(),
                Text = e.Ruolo.ToString(),

            }).ToList();


            ViewBag.Ruoli = dropRuoli;



            var utente = db.Utente.Include(u => u.Ruoli);
            return View(utente.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.IdRuoli = new SelectList(db.Ruoli, "IdRuoli", "Ruolo");
            return View();
        }

        // POST: Admin/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtente,Nome,Cognome,Email,Password,IdRuoli")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Utente.Add(utente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRuoli = new SelectList(db.Ruoli, "IdRuoli", "Ruolo", utente.IdRuoli);
            return View(utente);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRuoli = new SelectList(db.Ruoli, "IdRuoli", "Ruolo", utente.IdRuoli);
            return View(utente);
        }

        // POST: Admin/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtente,Nome,Cognome,Email,Password,IdRuoli")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRuoli = new SelectList(db.Ruoli, "IdRuoli", "Ruolo", utente.IdRuoli);
            return View(utente);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utente utente = db.Utente.Find(id);
            db.Utente.Remove(utente);
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

        public ActionResult editRuolo(int idRuolo, int idUtente) 
        {
            Utente u = db.Utente.Find(idUtente);
            if(u.IdRuoli != idRuolo) 
            { 
            u.IdRuoli = idRuolo;
            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();
                       
            }

            var response = new
            {
                status = true,
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }


    }
}
