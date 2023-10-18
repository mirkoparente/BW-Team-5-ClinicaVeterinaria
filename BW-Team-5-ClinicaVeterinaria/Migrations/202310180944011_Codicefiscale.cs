namespace BW_Team_5_ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Codicefiscale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clienti", "CodiceFiscale", c => c.String(nullable: false, maxLength: 16));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clienti", "CodiceFiscale");
        }
    }
}
