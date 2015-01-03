namespace ForumETF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostAttachmentFilenameAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostAttachments", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostAttachments", "FileName");
        }
    }
}
