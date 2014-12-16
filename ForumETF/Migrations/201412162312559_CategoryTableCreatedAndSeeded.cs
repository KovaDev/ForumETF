namespace ForumETF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryTableCreatedAndSeeded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Posts", "Category_CategoryId", c => c.Int());
            CreateIndex("dbo.Posts", "Category_CategoryId");
            AddForeignKey("dbo.Posts", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Posts", new[] { "Category_CategoryId" });
            DropColumn("dbo.Posts", "Category_CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
