namespace BW_Team_5_ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataVisite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visite", "Data", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visite", "Data");
        }
    }
}
