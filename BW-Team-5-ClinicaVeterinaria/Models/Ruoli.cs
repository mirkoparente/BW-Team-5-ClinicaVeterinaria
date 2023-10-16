namespace BW_Team_5_ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ruoli")]
    public partial class Ruoli
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ruoli()
        {
            Utente = new HashSet<Utente>();
        }

        [Key]
        public int IdRuoli { get; set; }

        [Required]
        [StringLength(20)]
        public string Ruolo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Utente> Utente { get; set; }
    }
}
