using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BW_Team_5_ClinicaVeterinaria.Models
{
    public class PazientiToJson
    {
        public int IdPaziente { get; set; }

        public string Foto { get; set; }
        public string Nome { get; set; }

        public string? DataRicovero { get; set; }


    }
}