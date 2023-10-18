using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BW_Team_5_ClinicaVeterinaria.Models
{
    public class Ordinis
    {
        [Key]
        public int IdOrdini { get; set; }


        public DateTime DataOrdine { get; set; }


        public int IdClienti { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotaleOrdine { get; set; }

        public virtual Clienti Clienti { get; set; }

        public virtual ICollection<ProdottiAcquistati> ProdottiAcquistati { get; set; }


    }
}