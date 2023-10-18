using BW_Team_5_ClinicaVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
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
            Paziente animale= dbModel.Paziente.FirstOrDefault(e=>e.Microchip==code && e.IsHospitalized== true);
            if (animale != null) 
            {
                var response = new {
                    status = true,
               id= animale.IdPaziente
               
               };

                return Json(response,JsonRequestBehavior.AllowGet );
            }
            else
            {
                var response = new
                {
                    status = false,
                   

                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            
        }





    }


}
