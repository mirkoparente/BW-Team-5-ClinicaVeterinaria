using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BW_Team_5_ClinicaVeterinaria.Models;

namespace BW_Team_5_ClinicaVeterinaria.Controllers
{
    public class PazientiController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // GET: Pazienti
        public ActionResult ListaPazienti()
        {
            var paziente = db.Paziente.Include(p => p.Clienti).Include(p => p.TipoPaziente);
            return View(paziente.ToList());
        }

        // GET: Pazienti/Details/5
        public ActionResult DettagliPaziente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paziente paziente = db.Paziente.Find(id);
            if (paziente == null)
            {
                return HttpNotFound();
            }
            return View(paziente);
        }

        // GET: Pazienti/Create
        public ActionResult AddPaziente()
        {
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome");
            ViewBag.IdTipo = new SelectList(db.TipoPaziente, "IdTipo", "Tipologia");
            return View();
        }

        // POST: Pazienti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPaziente([Bind(Include = "IdPaziente,Nome,DataNascita,ColoreMantello,Microchip,Foto,IsHospitalized,IdClienti,IdTipo")] Paziente paziente, HttpPostedFileBase Foto)
        {

           
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    string source = Path.Combine(Server.MapPath("~/Content/img"), Foto.FileName);
                    Foto.SaveAs(source);
                    paziente.Foto = Foto.FileName;
                    }
                    else
                    {
                        paziente.Foto = "";
                    }
                db.Paziente.Add(paziente);
                db.SaveChanges();
                return RedirectToAction("ListaPazienti");
            

        }
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome");
            ViewBag.IdTipo = new SelectList(db.TipoPaziente, "IdTipo", "Tipologia", paziente.IdTipo);
            return View(paziente);
            }

        // GET: Pazienti/Edit/5
        public ActionResult EditPaziente(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paziente paziente = db.Paziente.Find(id);
            if (paziente == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome");
            ViewBag.IdTipo = new SelectList(db.TipoPaziente, "IdTipo", "Tipologia", paziente.IdTipo);
            return View(paziente);
        }

        // POST: Pazienti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPaziente([Bind(Include = "IdPaziente,Nome,DataNascita,ColoreMantello,Microchip,Foto,IsHospitalized,IdClienti,IdTipo")] Paziente paziente, HttpPostedFileBase Foto)
        {

                if (ModelState.IsValid)
                {
                    if (Foto != null)
                    {
            string source = Path.Combine(Server.MapPath("~/Content/img"), Foto.FileName);
                        Foto.SaveAs(source);
                        paziente.Foto = Foto.FileName;
                }
                else
                {
                    paziente.Foto = "";
                }
                    db.Entry(paziente).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ListaPazienti");
                }
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome");
            ViewBag.IdTipo = new SelectList(db.TipoPaziente, "IdTipo", "Tipologia", paziente.IdTipo);
            return View(paziente);
        }

        // GET: Pazienti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paziente paziente = db.Paziente.Find(id);
            if (paziente == null)
            {
                return HttpNotFound();
            }
            return View(paziente);
        }

        // POST: Pazienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paziente paziente = db.Paziente.Find(id);
            db.Paziente.Remove(paziente);
            db.SaveChanges();
            return RedirectToAction("ListaPazienti");
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
