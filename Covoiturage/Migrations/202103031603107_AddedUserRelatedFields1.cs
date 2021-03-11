namespace Covoiturage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserRelatedFields1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DateNaiss", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Adresse", c => c.String());
            AddColumn("dbo.AspNetUsers", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "DateInscription", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DateInscription");
            DropColumn("dbo.AspNetUsers", "isActive");
            DropColumn("dbo.AspNetUsers", "Adresse");
            DropColumn("dbo.AspNetUsers", "DateNaiss");
        }
    }
}
