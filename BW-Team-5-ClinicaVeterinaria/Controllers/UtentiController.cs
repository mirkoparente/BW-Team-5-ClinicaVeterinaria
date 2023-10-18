﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BW_Team_5_ClinicaVeterinaria.Models.Classi;
using BW_Team_5_ClinicaVeterinaria.Models;
using System.Data.Entity;

namespace BW_Team_5_ClinicaVeterinaria.Controllers
{
    public class UtentiController : Controller
    {
        ContextDbModel dbContext = new ContextDbModel();

      
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Auth u)
        {
            if (ModelState.IsValid)
            {

                Utente user = dbContext.Utente.FirstOrDefault(e => e.Email == u.Email);
                if (user.Email == u.Email && user.Password == u.Password)
                {
                    dbContext.Dispose();
                    if (u.RememberMe)
                    {
                        FormsAuthentication.SetAuthCookie(user.Email, true);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(user.Email, false);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Credenziali non valide");
                    return View(u);
                }

            }
            else
            {
                ModelState.AddModelError("", "Credenziali non valide");
                return View(u);

            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "IDRuoli")] Utente user)
        {
            List<Utente> Utenti = dbContext.Utente.ToList();
            bool isNewUtente = true;
            foreach (var item in Utenti)
            {
                if (item.Email == user.Email) 
                {
                    isNewUtente = false;
                    break;
                }
            }

            if (isNewUtente) 
            { 
            
            if (user.Password== user.ConfirmPassword)
            {
                if (ModelState.IsValid)
                {
                    user.IdRuoli = 1;
                    dbContext.Utente.Add(user);
                    try
                    {
                        dbContext.SaveChanges();
                        return RedirectToAction("Login", "Utenti");
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError("Si è verificato un errore durante la registrazione.", ex);
                        return View(user);

                    }
                    finally
                    {
                        dbContext.Dispose();
                    }
                }
            }

            ViewBag.password = "le password non coincidono";
            user.Password = null;
            user.ConfirmPassword=null;
            return View(user);
            }

            ViewBag.password = "utente già registrato";
            return View(user);
        }

        public ActionResult EditProfile (int id) 
        {
            Utente user = dbContext.Utente.Find(id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(Utente user)
        {
            List<Utente> Utenti = dbContext.Utente.ToList();
            bool isNewUtente = true;
            foreach (var item in Utenti)
            {
                if (item.Email == user.Email)
                {
                    isNewUtente = false;
                    break;
                }
            }

            if (isNewUtente)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            dbContext.Entry(user).State = EntityState.Modified;
                            dbContext.SaveChanges();
                            return RedirectToAction("Index", "Home");
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", "Errore di comuncazione col server");
                            return View(user);
                        }
                        finally { dbContext.Dispose(); }
                    }
                    else
                    {
                        ViewBag.password = "compila tutti i campi";
                        user.Password = null;
                        user.ConfirmPassword = null;
                        return View(user);
                    }
                }
                else {
                    ViewBag.password = "le password non coincidono";
                    user.Password = null;
                    user.ConfirmPassword = null;
                    return View(user);
                }
            }
            ViewBag.password = "utente già registrato";
            user.Password = null;
            user.ConfirmPassword = null;
            return View(user);

        }
    }
}