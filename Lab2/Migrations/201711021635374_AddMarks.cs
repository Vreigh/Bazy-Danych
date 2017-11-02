namespace Lab2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMarks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogMarks",
                c => new
                    {
                        BlogMarkId = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        Value = c.Int(nullable: false),
                        AddDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BlogMarkId)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.PostMarks",
                c => new
                    {
                        PostMarkId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        Value = c.Int(nullable: false),
                        AddDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostMarkId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostMarks", "PostId", "dbo.Posts");
            DropForeignKey("dbo.BlogMarks", "BlogId", "dbo.Blogs");
            DropIndex("dbo.PostMarks", new[] { "PostId" });
            DropIndex("dbo.BlogMarks", new[] { "BlogId" });
            DropTable("dbo.PostMarks");
            DropTable("dbo.BlogMarks");
        }
    }
}
