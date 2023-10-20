namespace BW_Team_5_ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            ProdottiAcquistati = new HashSet<ProdottiAcquistati>();
        }

        [Key]
        public int IdProdotti { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Display(Name = "Quantità disponibile")]
        public int QuantitaDisponibile { get; set; }

        [Column(TypeName = "money")]
        [Display(Name ="Prezzo unitario")]
        public decimal PrezzoUnitario { get; set; }

        [Display(Name ="Foto prodotto")]
        public string FotoProdotto { get; set; }

        public int IdFornitori { get; set; }

        public int IdCategoria { get; set; }

        public int IdCassetti { get; set; }

        [NotMapped]
        public int QuantitaAcquistata {  get; set; }

        public virtual Cassetti Cassetti { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Fornitori Fornitori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdottiAcquistati> ProdottiAcquistati { get; set; }
    }
}
