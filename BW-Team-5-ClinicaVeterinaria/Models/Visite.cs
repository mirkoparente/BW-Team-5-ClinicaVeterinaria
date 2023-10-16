namespace BW_Team_5_ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visite")]
    public partial class Visite
    {
        [Key]
        public int IdVisite { get; set; }

        public int IdPaziente { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public string Ananmnesi { get; set; }

        public virtual Paziente Paziente { get; set; }
    }
}
