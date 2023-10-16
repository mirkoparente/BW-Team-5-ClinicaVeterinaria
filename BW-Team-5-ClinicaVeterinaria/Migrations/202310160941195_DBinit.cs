namespace BW_Team_5_ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Armadietti",
                c => new
                    {
                        IdArmadietto = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdArmadietto);
            
            CreateTable(
                "dbo.Cassetti",
                c => new
                    {
                        IdCassetti = c.Int(nullable: false, identity: true),
                        IdArmadietti = c.Int(nullable: false),
                        Nome = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdCassetti)
                .ForeignKey("dbo.Armadietti", t => t.IdArmadietti)
                .Index(t => t.IdArmadietti);
            
            CreateTable(
                "dbo.Prodotti",
                c => new
                    {
                        IdProdotti = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Descrizione = c.String(nullable: false),
                        QuantitaDisponibile = c.Int(nullable: false),
                        PrezzoUnitario = c.Decimal(nullable: false, storeType: "money"),
                        IdFornitori = c.Int(nullable: false),
                        IdCategoria = c.Int(nullable: false),
                        IdCassetti = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProdotti)
                .ForeignKey("dbo.Categoria", t => t.IdCategoria)
                .ForeignKey("dbo.Fornitori", t => t.IdFornitori)
                .ForeignKey("dbo.Cassetti", t => t.IdCassetti)
                .Index(t => t.IdFornitori)
                .Index(t => t.IdCategoria)
                .Index(t => t.IdCassetti);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        IdCategoria = c.Int(nullable: false, identity: true),
                        Tiplogia = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdCategoria);
            
            CreateTable(
                "dbo.Fornitori",
                c => new
                    {
                        IdFornitori = c.Int(nullable: false, identity: true),
                        RagioneSociale = c.String(nullable: false, maxLength: 100),
                        Indirizzo = c.String(nullable: false),
                        Piva = c.String(nullable: false, maxLength: 50),
                        Citta = c.String(nullable: false, maxLength: 100),
                        Recapito = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdFornitori);
            
            CreateTable(
                "dbo.ProdottiAcquistati",
                c => new
                    {
                        IdProdottoAcquistato = c.Int(nullable: false, identity: true),
                        IdProdotti = c.Int(nullable: false),
                        IdClienti = c.Int(nullable: false),
                        Quantita = c.Int(nullable: false),
                        NumeroRicetta = c.String(),
                        Totale = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.IdProdottoAcquistato)
                .ForeignKey("dbo.Clienti", t => t.IdClienti)
                .ForeignKey("dbo.Prodotti", t => t.IdProdotti)
                .Index(t => t.IdProdotti)
                .Index(t => t.IdClienti);
            
            CreateTable(
                "dbo.Clienti",
                c => new
                    {
                        IdClienti = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Cognome = c.String(nullable: false, maxLength: 50),
                        Telefono = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdClienti);
            
            CreateTable(
                "dbo.Paziente",
                c => new
                    {
                        IdPaziente = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        DataNascita = c.DateTime(storeType: "date"),
                        ColoreMantello = c.String(nullable: false, maxLength: 50),
                        Microchip = c.String(maxLength: 10),
                        Foto = c.String(),
                        IdClienti = c.Int(),
                        IdTipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPaziente)
                .ForeignKey("dbo.Clienti", t => t.IdClienti)
                .ForeignKey("dbo.TipoPaziente", t => t.IdTipo)
                .Index(t => t.IdClienti)
                .Index(t => t.IdTipo);
            
            CreateTable(
                "dbo.TipoPaziente",
                c => new
                    {
                        IdTipo = c.Int(nullable: false, identity: true),
                        Tipologia = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.IdTipo);
            
            CreateTable(
                "dbo.Visite",
                c => new
                    {
                        IdVisite = c.Int(nullable: false, identity: true),
                        IdPaziente = c.Int(nullable: false),
                        Descrizione = c.String(nullable: false),
                        Ananmnesi = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdVisite)
                .ForeignKey("dbo.Paziente", t => t.IdPaziente)
                .Index(t => t.IdPaziente);
            
            CreateTable(
                "dbo.Ruoli",
                c => new
                    {
                        IdRuoli = c.Int(nullable: false, identity: true),
                        Ruolo = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.IdRuoli);
            
            CreateTable(
                "dbo.Utente",
                c => new
                    {
                        IdUtente = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Cognome = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        IdRuoli = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdUtente)
                .ForeignKey("dbo.Ruoli", t => t.IdRuoli)
                .Index(t => t.IdRuoli);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utente", "IdRuoli", "dbo.Ruoli");
            DropForeignKey("dbo.Cassetti", "IdArmadietti", "dbo.Armadietti");
            DropForeignKey("dbo.Prodotti", "IdCassetti", "dbo.Cassetti");
            DropForeignKey("dbo.ProdottiAcquistati", "IdProdotti", "dbo.Prodotti");
            DropForeignKey("dbo.ProdottiAcquistati", "IdClienti", "dbo.Clienti");
            DropForeignKey("dbo.Visite", "IdPaziente", "dbo.Paziente");
            DropForeignKey("dbo.Paziente", "IdTipo", "dbo.TipoPaziente");
            DropForeignKey("dbo.Paziente", "IdClienti", "dbo.Clienti");
            DropForeignKey("dbo.Prodotti", "IdFornitori", "dbo.Fornitori");
            DropForeignKey("dbo.Prodotti", "IdCategoria", "dbo.Categoria");
            DropIndex("dbo.Utente", new[] { "IdRuoli" });
            DropIndex("dbo.Visite", new[] { "IdPaziente" });
            DropIndex("dbo.Paziente", new[] { "IdTipo" });
            DropIndex("dbo.Paziente", new[] { "IdClienti" });
            DropIndex("dbo.ProdottiAcquistati", new[] { "IdClienti" });
            DropIndex("dbo.ProdottiAcquistati", new[] { "IdProdotti" });
            DropIndex("dbo.Prodotti", new[] { "IdCassetti" });
            DropIndex("dbo.Prodotti", new[] { "IdCategoria" });
            DropIndex("dbo.Prodotti", new[] { "IdFornitori" });
            DropIndex("dbo.Cassetti", new[] { "IdArmadietti" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Utente");
            DropTable("dbo.Ruoli");
            DropTable("dbo.Visite");
            DropTable("dbo.TipoPaziente");
            DropTable("dbo.Paziente");
            DropTable("dbo.Clienti");
            DropTable("dbo.ProdottiAcquistati");
            DropTable("dbo.Fornitori");
            DropTable("dbo.Categoria");
            DropTable("dbo.Prodotti");
            DropTable("dbo.Cassetti");
            DropTable("dbo.Armadietti");
        }
    }
}
