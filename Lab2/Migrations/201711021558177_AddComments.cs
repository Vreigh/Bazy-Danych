namespace Lab2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        BlogCommentId = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        Content = c.String(),
                        AddDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BlogCommentId)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.PostComments",
                c => new
                    {
                        PostCommentId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        Content = c.String(),
                        AddDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostCommentId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostComments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.BlogComments", "BlogId", "dbo.Blogs");
            DropIndex("dbo.PostComments", new[] { "PostId" });
            DropIndex("dbo.BlogComments", new[] { "BlogId" });
            DropTable("dbo.PostComments");
            DropTable("dbo.BlogComments");
        }
    }
}
