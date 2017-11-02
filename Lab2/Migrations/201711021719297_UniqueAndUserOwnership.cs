namespace Lab2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueAndUserOwnership : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Blogs", "UserName", "dbo.Users");
            DropIndex("dbo.Blogs", new[] { "UserName" });
            DropPrimaryKey("dbo.Users");
            AddColumn("dbo.BlogComments", "UserName", c => c.String(maxLength: 30));
            AddColumn("dbo.BlogMarks", "UserName", c => c.String(maxLength: 30));
            AddColumn("dbo.PostComments", "UserName", c => c.String(maxLength: 30));
            AddColumn("dbo.PostMarks", "UserName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Blogs", "Name", c => c.String(maxLength: 30));
            AlterColumn("dbo.Blogs", "UserName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Posts", "Title", c => c.String(maxLength: 30));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "display_name", c => c.String(maxLength: 30));
            AddPrimaryKey("dbo.Users", "UserName");
            CreateIndex("dbo.BlogComments", "UserName");
            CreateIndex("dbo.Blogs", "Name", unique: true);
            CreateIndex("dbo.Blogs", "UserName");
            CreateIndex("dbo.BlogMarks", "UserName");
            CreateIndex("dbo.Users", "UserName", unique: true);
            CreateIndex("dbo.Users", "display_name", unique: true);
            CreateIndex("dbo.PostComments", "UserName");
            CreateIndex("dbo.Posts", "Title", unique: true);
            CreateIndex("dbo.PostMarks", "UserName");
            AddForeignKey("dbo.BlogComments", "UserName", "dbo.Users", "UserName");
            AddForeignKey("dbo.BlogMarks", "UserName", "dbo.Users", "UserName");
            AddForeignKey("dbo.PostMarks", "UserName", "dbo.Users", "UserName");
            AddForeignKey("dbo.PostComments", "UserName", "dbo.Users", "UserName");
            AddForeignKey("dbo.Blogs", "UserName", "dbo.Users", "UserName");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "UserName", "dbo.Users");
            DropForeignKey("dbo.PostComments", "UserName", "dbo.Users");
            DropForeignKey("dbo.PostMarks", "UserName", "dbo.Users");
            DropForeignKey("dbo.BlogMarks", "UserName", "dbo.Users");
            DropForeignKey("dbo.BlogComments", "UserName", "dbo.Users");
            DropIndex("dbo.PostMarks", new[] { "UserName" });
            DropIndex("dbo.Posts", new[] { "Title" });
            DropIndex("dbo.PostComments", new[] { "UserName" });
            DropIndex("dbo.Users", new[] { "display_name" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.BlogMarks", new[] { "UserName" });
            DropIndex("dbo.Blogs", new[] { "UserName" });
            DropIndex("dbo.Blogs", new[] { "Name" });
            DropIndex("dbo.BlogComments", new[] { "UserName" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "display_name", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Posts", "Title", c => c.String());
            AlterColumn("dbo.Blogs", "UserName", c => c.String(maxLength: 128));
            AlterColumn("dbo.Blogs", "Name", c => c.String());
            DropColumn("dbo.PostMarks", "UserName");
            DropColumn("dbo.PostComments", "UserName");
            DropColumn("dbo.BlogMarks", "UserName");
            DropColumn("dbo.BlogComments", "UserName");
            AddPrimaryKey("dbo.Users", "UserName");
            CreateIndex("dbo.Blogs", "UserName");
            AddForeignKey("dbo.Blogs", "UserName", "dbo.Users", "UserName");
        }
    }
}
