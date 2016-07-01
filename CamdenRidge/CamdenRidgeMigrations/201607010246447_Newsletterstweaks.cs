namespace CamdenRidge.CamdenRidgeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newsletterstweaks : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Event", "Body", c => c.String(nullable: false));
            AlterColumn("dbo.Newsletter", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Newsletter", "Body", c => c.String(nullable: false));
            AlterColumn("dbo.Newsletter", "ShortDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Newsletter", "ShortDescription", c => c.String());
            AlterColumn("dbo.Newsletter", "Body", c => c.String());
            AlterColumn("dbo.Newsletter", "Title", c => c.String());
            AlterColumn("dbo.Event", "Body", c => c.String());
            AlterColumn("dbo.Event", "Description", c => c.String());
        }
    }
}
