namespace BW_Team_5_ClinicaVeterinaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataRicovero : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paziente", "DataRicovero", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paziente", "DataRicovero");
        }
    }
}
