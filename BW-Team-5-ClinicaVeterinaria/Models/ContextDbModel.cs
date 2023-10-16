using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BW_Team_5_ClinicaVeterinaria.Models
{
    public partial class ContextDbModel : DbContext
    {
        public ContextDbModel()
            : base("name=ContextDbModel")
        {
        }

        public virtual DbSet<Armadietti> Armadietti { get; set; }
        public virtual DbSet<Cassetti> Cassetti { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Clienti> Clienti { get; set; }
        public virtual DbSet<Fornitori> Fornitori { get; set; }
        public virtual DbSet<Paziente> Paziente { get; set; }
        public virtual DbSet<Prodotti> Prodotti { get; set; }
        public virtual DbSet<ProdottiAcquistati> ProdottiAcquistati { get; set; }
        public virtual DbSet<Ruoli> Ruoli { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TipoPaziente> TipoPaziente { get; set; }
        public virtual DbSet<Utente> Utente { get; set; }
        public virtual DbSet<Visite> Visite { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Armadietti>()
                .HasMany(e => e.Cassetti)
                .WithRequired(e => e.Armadietti)
                .HasForeignKey(e => e.IdArmadietti)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cassetti>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.Cassetti)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clienti>()
                .HasMany(e => e.ProdottiAcquistati)
                .WithRequired(e => e.Clienti)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fornitori>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.Fornitori)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Paziente>()
                .HasMany(e => e.Visite)
                .WithRequired(e => e.Paziente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotti>()
                .Property(e => e.PrezzoUnitario)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prodotti>()
                .HasMany(e => e.ProdottiAcquistati)
                .WithRequired(e => e.Prodotti)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProdottiAcquistati>()
                .Property(e => e.Totale)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ruoli>()
                .HasMany(e => e.Utente)
                .WithRequired(e => e.Ruoli)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoPaziente>()
                .HasMany(e => e.Paziente)
                .WithRequired(e => e.TipoPaziente)
                .WillCascadeOnDelete(false);
        }
    }
}
