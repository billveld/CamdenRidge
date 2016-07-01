namespace CamdenRidge.CamdenRidgeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                        Category = c.String(),
                        Display = c.Boolean(nullable: false),
                        Public = c.Boolean(nullable: false),
                        Sequence = c.Int(nullable: false),
                        FileContents = c.Binary(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        Display = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        Text = c.String(),
                        Display = c.Boolean(nullable: false),
                        ThreadID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Thread", t => t.ThreadID, cascadeDelete: true)
                .Index(t => t.ThreadID);
            
            CreateTable(
                "dbo.Thread",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        LastPost = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "ThreadID", "dbo.Thread");
            DropIndex("dbo.Message", new[] { "ThreadID" });
            DropTable("dbo.Thread");
            DropTable("dbo.Message");
            DropTable("dbo.Event");
            DropTable("dbo.Document");
        }
    }
}
