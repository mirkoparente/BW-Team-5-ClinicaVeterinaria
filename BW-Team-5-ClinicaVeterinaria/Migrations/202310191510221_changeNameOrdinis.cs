namespace BW_Team_5_ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeNameOrdinis : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Ordinis", newName: "Ordini");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Ordini", newName: "Ordinis");
        }
    }
}
