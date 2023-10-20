using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BW_Team_5_ClinicaVeterinaria.Models.Classi
{
    public class OrdiniToJson
    {
        public int IdOrdini { get; set; }


        public string DataOrdine { get; set; }


        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? TotaleOrdine { get; set; }

        public int Quantita { get; set; }
        public string Nome { get; set; }


    }
}