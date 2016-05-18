namespace Bmazon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyOrder : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Order", name: "Customer_Email", newName: "CustomerEmail");
            RenameIndex(table: "dbo.Order", name: "IX_Customer_Email", newName: "IX_CustomerEmail");
            AddColumn("dbo.Order", "SellerEmail", c => c.String(maxLength: 128));
            CreateIndex("dbo.Order", "SellerEmail");
            AddForeignKey("dbo.Order", "SellerEmail", "dbo.Seller", "Email");
            DropColumn("dbo.Order", "Buyer");
            DropColumn("dbo.Order", "Seller");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "Seller", c => c.String());
            AddColumn("dbo.Order", "Buyer", c => c.String());
            DropForeignKey("dbo.Order", "SellerEmail", "dbo.Seller");
            DropIndex("dbo.Order", new[] { "SellerEmail" });
            DropColumn("dbo.Order", "SellerEmail");
            RenameIndex(table: "dbo.Order", name: "IX_CustomerEmail", newName: "IX_Customer_Email");
            RenameColumn(table: "dbo.Order", name: "CustomerEmail", newName: "Customer_Email");
        }
    }
}
