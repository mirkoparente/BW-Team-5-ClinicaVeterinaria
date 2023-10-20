namespace BW_Team_5_ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Fornitori")]
    public partial class Fornitori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fornitori()
        {
            Prodotti = new HashSet<Prodotti>();
        }

        [Key]
        public int IdFornitori { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Ragione sociale")]
        public string RagioneSociale { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        [Required]
        [StringLength(50)]
        public string Piva { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Città")]
        public string Citta { get; set; }

        [Required]
        [StringLength(50)]
        public string Recapito { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prodotti> Prodotti { get; set; }
    }
}
