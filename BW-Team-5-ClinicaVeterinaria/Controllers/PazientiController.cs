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

        public ActionResult ListaPazienti()
        {
            var paziente = db.Paziente.Include(p => p.Clienti).Include(p => p.TipoPaziente);
            return View(paziente.ToList());
        }

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

        public ActionResult AddPaziente()
        {
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome");
            ViewBag.IdTipo = new SelectList(db.TipoPaziente, "IdTipo", "Tipologia");
            return View();
        }

       
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
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome", paziente.IdClienti);
            ViewBag.IdTipo = new SelectList(db.TipoPaziente, "IdTipo", "Tipologia", paziente.IdTipo);
            return View(paziente);
            }

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
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome", paziente.IdClienti);
            ViewBag.IdTipo = new SelectList(db.TipoPaziente, "IdTipo", "Tipologia", paziente.IdTipo);
            return View(paziente);
        }

        
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
            ViewBag.IdClienti = new SelectList(db.Clienti, "IdClienti", "Nome", paziente.IdClienti);
            ViewBag.IdTipo = new SelectList(db.TipoPaziente, "IdTipo", "Tipologia", paziente.IdTipo);
            return View(paziente);
        }

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


        public ActionResult Ricoveri()
        {
            return View(db.TipoPaziente.ToList());
        }

        public JsonResult RicoveriAttivi()
        {
            List<Paziente> p = db.Paziente.Where(o => o.IsHospitalized == true).ToList();
            List<PazientiToJson> pat=new List<PazientiToJson>();
            foreach(Paziente paz in p)
            {
                PazientiToJson pa=new PazientiToJson();
                pa.IdPaziente=paz.IdPaziente;
                pa.Foto=paz.Foto;
                pa.Nome=paz.Nome;
                foreach(Visite v in paz.Visite)
                {
                    pa.DataRicovero = v.Data.Date;
                }

                pat.Add(pa);
            }

            return Json(pat, JsonRequestBehavior.AllowGet);
        }    
        
        
        public JsonResult RicoveriAttiviTipo(int id)
        {
            List<Paziente> p = db.Paziente.Where(o => o.IsHospitalized == true && o.IdTipo==id).ToList();
            List<PazientiToJson> pat=new List<PazientiToJson>();
            foreach(Paziente paz in p)
            {
                PazientiToJson pa=new PazientiToJson();
                pa.IdPaziente=paz.IdPaziente;
                pa.Foto=paz.Foto;
                pa.Nome=paz.Nome;
                foreach (Visite v in paz.Visite)
                {
                    pa.DataRicovero = v.Data;
                }
                pat.Add(pa);
            }

            return Json(pat, JsonRequestBehavior.AllowGet);
        }
    }
}
