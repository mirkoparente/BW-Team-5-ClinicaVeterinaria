using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BW_Team_5_ClinicaVeterinaria.Models.Classi;
using BW_Team_5_ClinicaVeterinaria.Models;

namespace BW_Team_5_ClinicaVeterinaria.Controllers
{
    public class UtentiController : Controller
    {
        ContextDbModel dbContext = new ContextDbModel();

        // GET: Utenti
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
                    }else
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

        public ActionResult Logout () 
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
        public ActionResult Register([Bind(Exclude = "IDRuolo")] Utente user)
        {
            return View();
        }

    }
}