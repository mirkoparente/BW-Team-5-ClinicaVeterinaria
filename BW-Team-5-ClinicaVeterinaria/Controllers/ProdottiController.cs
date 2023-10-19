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
    public class ProdottiController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // GET: Prodotti
        public ActionResult Index()
        {
            var prodotti = db.Prodotti.Include(p => p.Cassetti).Include(p => p.Categoria).Include(p => p.Fornitori);
            return View(prodotti.ToList());
        }

        // GET Dettagli prodotti
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // GET Crea prodotti
        public ActionResult Create()
        {
            ViewBag.IdCassetti = new SelectList(db.Cassetti, "IdCassetti", "Nome");
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Tiplogia");
            ViewBag.IdFornitori = new SelectList(db.Fornitori, "IdFornitori", "RagioneSociale");
            return View();
        }

        // POST Crea Prodotti
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProdotti,Nome,Descrizione,QuantitaDisponibile,PrezzoUnitario,IdFornitori,IdCategoria,IdCassetti")] Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Prodotti.Add(prodotti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCassetti = new SelectList(db.Cassetti, "IdCassetti", "Nome", prodotti.IdCassetti);
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Tiplogia", prodotti.IdCategoria);
            ViewBag.IdFornitori = new SelectList(db.Fornitori, "IdFornitori", "RagioneSociale", prodotti.IdFornitori);
            return View(prodotti);
        }

        // GET Modifica prodotti
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCassetti = new SelectList(db.Cassetti, "IdCassetti", "Nome", prodotti.IdCassetti);
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Tiplogia", prodotti.IdCategoria);
            ViewBag.IdFornitori = new SelectList(db.Fornitori, "IdFornitori", "RagioneSociale", prodotti.IdFornitori);
            return View(prodotti);
        }

        // POST Modifica prodotti
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProdotti,Nome,Descrizione,QuantitaDisponibile,PrezzoUnitario,IdFornitori,IdCategoria,IdCassetti")] Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodotti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCassetti = new SelectList(db.Cassetti, "IdCassetti", "Nome", prodotti.IdCassetti);
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Tiplogia", prodotti.IdCategoria);
            ViewBag.IdFornitori = new SelectList(db.Fornitori, "IdFornitori", "RagioneSociale", prodotti.IdFornitori);
            return View(prodotti);
        }

        // GET Cancella prodotti
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST Cancella prodotti
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotti);
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


        public ActionResult CercaProdotti()
        {
            Prodotti p=new Prodotti();
            return View(p);
        }


        
        public JsonResult Cerca(string Nome)
        {
            List<Prodotti> p = db.Prodotti.Where(n=>n.Nome.Contains(Nome)).ToList();
            List<ProdottiToJson> pro = new List<ProdottiToJson>();

            foreach(Prodotti pitem in p)
            {
                ProdottiToJson pt =new ProdottiToJson();

                pt.Nome =pitem.Nome;
                pt.IdProdotti =pitem.IdProdotti;
                pt.NomeCassetto = pitem.Cassetti.Nome;
                pt.NomeArmadietto = pitem.Cassetti.Armadietti.Nome;
                pt.Descrizione = pitem.Descrizione;
                pro.Add(pt);
            }

            return Json(pro,JsonRequestBehavior.AllowGet);
        }
    }
}
