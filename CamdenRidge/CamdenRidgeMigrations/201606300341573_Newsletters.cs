namespace CamdenRidge.CamdenRidgeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newsletters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Newsletter",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        PublishDate = c.DateTime(nullable: false),
                        ShortDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Event", "BoardAndCommmitteeOnly", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "BoardAndCommmitteeOnly");
            DropTable("dbo.Newsletter");
        }
    }
}
