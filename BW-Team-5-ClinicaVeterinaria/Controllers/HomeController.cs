using BW_Team_5_ClinicaVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BW_Team_5_ClinicaVeterinaria.Controllers
{
    public class HomeController : Controller
    {
        ContextDbModel dbModel = new ContextDbModel();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public JsonResult searchAnimalbyChipCode(string code) 
        {
            Paziente animale= dbModel.Paziente.FirstOrDefault(e=>e.Microchip==code);
            if (animale != null) 
            {
               var response = new {
               id= animale.IdPaziente
               
               };

                return Json(response,JsonRequestBehavior.AllowGet );
            }
            else
            { 
                return Json("nessun animale con questo numero di chip trovato", JsonRequestBehavior.AllowGet);
            }
            
        }





    }


}
