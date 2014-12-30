namespace ForumETF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnswerAndPostModelsUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Votes", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "UpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "UpdatedAt");
            DropColumn("dbo.Answers", "Votes");
        }
    }
}
