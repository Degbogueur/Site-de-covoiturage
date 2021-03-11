namespace Covoiturage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserRelatedFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nom", c => c.String());
            AddColumn("dbo.AspNetUsers", "Prenoms", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Prenoms");
            DropColumn("dbo.AspNetUsers", "Nom");
        }
    }
}
