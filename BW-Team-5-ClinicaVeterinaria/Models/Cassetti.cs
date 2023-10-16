namespace BW_Team_5_ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cassetti")]
    public partial class Cassetti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cassetti()
        {
            Prodotti = new HashSet<Prodotti>();
        }

        [Key]
        public int IdCassetti { get; set; }

        public int IdArmadietti { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }

        public virtual Armadietti Armadietti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prodotti> Prodotti { get; set; }
    }
}
