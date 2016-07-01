namespace CamdenRidge.CamdenRidgeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreNewsletters : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "Body", c => c.String());
            AddColumn("dbo.Event", "Location", c => c.String());
            AddColumn("dbo.Newsletter", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Newsletter", "ImagePath");
            DropColumn("dbo.Event", "Location");
            DropColumn("dbo.Event", "Body");
        }
    }
}
