using System;
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
                if (user != null) 
                {
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
                        ViewBag.Error="Credenziali non valide";
                        return View(u);
                    }
                }
                else 
                {
                    ViewBag.Error = "Tutti i campi sono obbligatori";
                    return View(u);
                }

            }
            else
            {
                ViewBag.Error="Compila tutti i campi";
                return View(u);

            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
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

                        ViewBag.Password="Si è verificato un errore durante la registrazione.";
                        return View(user);

                    }
                    finally
                    {
                        dbContext.Dispose();
                    }
                }
            }

            ViewBag.password = "Le password non coincidono";
            user.Password = null;
            user.ConfirmPassword=null;
            return View(user);
            }

            ViewBag.password = "Utente già registrato";
            return View(user);
        }

        public ActionResult EditProfile () 
        {
            string userEmail = User.Identity.Name; 
            Utente user = dbContext.Utente.FirstOrDefault(e=>e.Email==userEmail);
            user.ConfirmPassword = user.Password;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(Utente user)
        {
            List<Utente> Utenti = dbContext.Utente.ToList();
            bool mailIsFree = true;
            foreach (var item in Utenti)
            {
                if(item.IdUtente != user.IdUtente)
                { 
                    if (item.Email == user.Email)
                    {
                        mailIsFree = false;
                        break;
                    }
                }
            }

            if (mailIsFree)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    ContextDbModel db2 = new ContextDbModel();
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            db2.Entry(user).State = EntityState.Modified;
                            db2.SaveChanges();
                            return RedirectToAction("Index", "Home");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Password="Errore di comuncazione col server";
                            return View(user);
                        }
                        finally { dbContext.Dispose(); db2.Dispose(); }
                    }
                    else
                    {
                        ViewBag.password = "Compila tutti i campi";
                        user.Password = null;
                        user.ConfirmPassword = null;
                        return View(user);
                    }
                }
                else {
                    ViewBag.password = "Le password non coincidono";
                    user.Password = null;
                    user.ConfirmPassword = null;
                    return View(user);
                }
            }
            ViewBag.password = "Mail già registrata";
            user.Password = null;
            user.ConfirmPassword = null;
            return View(user);

        }
    }
}