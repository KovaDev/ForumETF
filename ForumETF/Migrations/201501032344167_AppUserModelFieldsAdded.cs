namespace ForumETF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserModelFieldsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "About", c => c.String());
            AddColumn("dbo.AspNetUsers", "FacebookUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "TwitterUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "LinkedinUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "InstagramUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "GithubUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "MobilePhone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MobilePhone");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "GithubUrl");
            DropColumn("dbo.AspNetUsers", "InstagramUrl");
            DropColumn("dbo.AspNetUsers", "LinkedinUrl");
            DropColumn("dbo.AspNetUsers", "TwitterUrl");
            DropColumn("dbo.AspNetUsers", "FacebookUrl");
            DropColumn("dbo.AspNetUsers", "About");
        }
    }
}
