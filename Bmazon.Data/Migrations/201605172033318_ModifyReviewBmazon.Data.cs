namespace Bmazon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyReviewBmazonData : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Review", name: "Customer_Email", newName: "Reviewer_Email");
            RenameIndex(table: "dbo.Review", name: "IX_Customer_Email", newName: "IX_Reviewer_Email");
            AddColumn("dbo.Review", "Product_ProductID", c => c.Int());
            AddColumn("dbo.Review", "Seller_Email", c => c.String(maxLength: 128));
            CreateIndex("dbo.Review", "Product_ProductID");
            CreateIndex("dbo.Review", "Seller_Email");
            AddForeignKey("dbo.Review", "Product_ProductID", "dbo.Product", "ProductID");
            AddForeignKey("dbo.Review", "Seller_Email", "dbo.Seller", "Email");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "Seller_Email", "dbo.Seller");
            DropForeignKey("dbo.Review", "Product_ProductID", "dbo.Product");
            DropIndex("dbo.Review", new[] { "Seller_Email" });
            DropIndex("dbo.Review", new[] { "Product_ProductID" });
            DropColumn("dbo.Review", "Seller_Email");
            DropColumn("dbo.Review", "Product_ProductID");
            RenameIndex(table: "dbo.Review", name: "IX_Reviewer_Email", newName: "IX_Customer_Email");
            RenameColumn(table: "dbo.Review", name: "Reviewer_Email", newName: "Customer_Email");
        }
    }
}
