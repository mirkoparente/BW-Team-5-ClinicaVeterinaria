namespace BW_Team_5_ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProdottiAcquistati")]
    public partial class ProdottiAcquistati
    {
        [Key]
        public int IdProdottoAcquistato { get; set; }

        public int IdProdotti { get; set; }

        public int IdClienti { get; set; }

        public int Quantita { get; set; }

        public string NumeroRicetta { get; set; }

        [Column(TypeName = "money")]
        public decimal? Totale { get; set; }

        public virtual Clienti Clienti { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
