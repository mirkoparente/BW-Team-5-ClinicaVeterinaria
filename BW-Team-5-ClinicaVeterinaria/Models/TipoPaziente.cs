namespace BW_Team_5_ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoPaziente")]
    public partial class TipoPaziente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoPaziente()
        {
            Paziente = new HashSet<Paziente>();
        }

        [Key]
        public int IdTipo { get; set; }

        [StringLength(100)]
        public string Tipologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paziente> Paziente { get; set; }
    }
}
