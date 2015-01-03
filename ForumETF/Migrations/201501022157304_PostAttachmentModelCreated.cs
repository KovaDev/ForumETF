namespace ForumETF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostAttachmentModelCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostAttachments",
                c => new
                    {
                        PostAttachmentId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(),
                        Post_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.PostAttachmentId)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .Index(t => t.Post_PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostAttachments", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.PostAttachments", new[] { "Post_PostId" });
            DropTable("dbo.PostAttachments");
        }
    }
}
