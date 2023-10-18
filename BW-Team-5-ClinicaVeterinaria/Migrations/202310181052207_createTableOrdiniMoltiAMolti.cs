namespace BW_Team_5_ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTableOrdiniMoltiAMolti : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProdottiAcquistati", "IdClienti", "dbo.Clienti");
            DropIndex("dbo.ProdottiAcquistati", new[] { "IdClienti" });
            CreateTable(
                "dbo.Ordinis",
                c => new
                    {
                        IdOrdini = c.Int(nullable: false, identity: true),
                        DataOrdine = c.DateTime(nullable: false),
                        IdClienti = c.Int(nullable: false),
                        TotaleOrdine = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.IdOrdini)
                .ForeignKey("dbo.Clienti", t => t.IdClienti)
                .Index(t => t.IdClienti);
            
            AddColumn("dbo.ProdottiAcquistati", "IdOrdini", c => c.Int(nullable: false));
            CreateIndex("dbo.ProdottiAcquistati", "IdOrdini");
            AddForeignKey("dbo.ProdottiAcquistati", "IdOrdini", "dbo.Ordinis", "IdOrdini", cascadeDelete: true);
            DropColumn("dbo.ProdottiAcquistati", "IdClienti");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProdottiAcquistati", "IdClienti", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProdottiAcquistati", "IdOrdini", "dbo.Ordinis");
            DropForeignKey("dbo.Ordinis", "IdClienti", "dbo.Clienti");
            DropIndex("dbo.Ordinis", new[] { "IdClienti" });
            DropIndex("dbo.ProdottiAcquistati", new[] { "IdOrdini" });
            DropColumn("dbo.ProdottiAcquistati", "IdOrdini");
            DropTable("dbo.Ordinis");
            CreateIndex("dbo.ProdottiAcquistati", "IdClienti");
            AddForeignKey("dbo.ProdottiAcquistati", "IdClienti", "dbo.Clienti", "IdClienti");
        }
    }
}
