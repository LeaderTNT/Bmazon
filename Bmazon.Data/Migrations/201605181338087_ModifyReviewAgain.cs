namespace Bmazon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyReviewAgain : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Review", name: "Reviewer_Email", newName: "CustomerEmail");
            RenameIndex(table: "dbo.Review", name: "IX_Reviewer_Email", newName: "IX_CustomerEmail");
            AddColumn("dbo.Review", "Company", c => c.String());
            AddColumn("dbo.Review", "ProductID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Review", "ProductID");
            DropColumn("dbo.Review", "Company");
            RenameIndex(table: "dbo.Review", name: "IX_CustomerEmail", newName: "IX_Reviewer_Email");
            RenameColumn(table: "dbo.Review", name: "CustomerEmail", newName: "Reviewer_Email");
        }
    }
}
