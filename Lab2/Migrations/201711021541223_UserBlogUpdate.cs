namespace Lab2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserBlogUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "UserName", c => c.String(maxLength: 128));
            CreateIndex("dbo.Blogs", "UserName");
            AddForeignKey("dbo.Blogs", "UserName", "dbo.Users", "UserName");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "UserName", "dbo.Users");
            DropIndex("dbo.Blogs", new[] { "UserName" });
            DropColumn("dbo.Blogs", "UserName");
        }
    }
}
