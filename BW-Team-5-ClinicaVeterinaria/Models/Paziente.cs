namespace BW_Team_5_ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Paziente")]
    public partial class Paziente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paziente()
        {
            Visite = new HashSet<Visite>();
        }

        [Key]
        public int IdPaziente { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataNascita { get; set; }

        [Required]
        [StringLength(50)]
        public string ColoreMantello { get; set; }

        [StringLength(10)]
        public string Microchip { get; set; }

        public string Foto { get; set; }


        [Required]
        public bool IsHospitalized { get; set; }

        public int? IdClienti { get; set; }

        public int IdTipo { get; set; }

        public virtual Clienti Clienti { get; set; }

        public virtual TipoPaziente TipoPaziente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visite> Visite { get; set; }
    }
}
