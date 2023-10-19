namespace BW_Team_5_ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FotoProdotto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prodotti", "FotoProdotto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prodotti", "FotoProdotto");
        }
    }
}
