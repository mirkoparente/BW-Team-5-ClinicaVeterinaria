﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using BW_Team_5_ClinicaVeterinaria.Models;

namespace BW_Team_5_ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Veterinario")]
    public class VisiteController : Controller
    {
        private ContextDbModel db = new ContextDbModel();

        // GET: Visite
        public ActionResult Index()
        {
            return View(db.Visite.ToList());
        }

        // GET: Visite/Create
        public ActionResult Create(int id)
        {
            Visite newVisita = new Visite();
            newVisita.IdPaziente = id;
            return View(newVisita);
        }

        // POST: Visite/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Visite visite)
        {
            if (ModelState.IsValid)
            {
                visite.Data = DateTime.Now;
                db.Visite.Add(visite);
                db.SaveChanges();
                return RedirectToAction("ListaPazienti", "Pazienti");
            }

            ViewBag.IdPaziente = new SelectList(db.Paziente, "IdPaziente", "Nome", visite.IdPaziente);
            return View(visite);
        }

        // GET: Visite/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visite visite = db.Visite.Find(id);
            if (visite == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPaziente = new SelectList(db.Paziente, "IdPaziente", "Nome", visite.IdPaziente);
            return View(visite);
        }

        // POST: Visite/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdVisite,Data,IdPaziente,Descrizione,Ananmnesi")] Visite visite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaPazienti", "Pazienti");
            }
            ViewBag.IdPaziente = new SelectList(db.Paziente, "IdPaziente", "Nome", visite.IdPaziente);
            return View(visite);
        }
    }
}
