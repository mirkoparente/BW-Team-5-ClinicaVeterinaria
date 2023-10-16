namespace BW_Team_5_ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBoolpaziente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paziente", "IsHospitalized", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paziente", "IsHospitalized");
        }
    }
}
