namespace Bmazon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyBmazonData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "PaymentOption_ID", "dbo.PaymentOption");
            DropForeignKey("dbo.PaymentOption", "Customer_Email", "dbo.Customer");
            DropIndex("dbo.Order", new[] { "PaymentOption_ID" });
            DropIndex("dbo.PaymentOption", new[] { "Customer_Email" });
            AddColumn("dbo.Product", "Order_OrderID", c => c.Int());
            CreateIndex("dbo.Product", "Order_OrderID");
            AddForeignKey("dbo.Product", "Order_OrderID", "dbo.Order", "OrderID");
            DropColumn("dbo.Order", "PaymentOption_ID");
            DropTable("dbo.PaymentOption");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentOption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Owner = c.String(),
                        HolderName = c.String(),
                        CardNumber = c.String(),
                        ExpDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Customer_Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Order", "PaymentOption_ID", c => c.Int());
            DropForeignKey("dbo.Product", "Order_OrderID", "dbo.Order");
            DropIndex("dbo.Product", new[] { "Order_OrderID" });
            DropColumn("dbo.Product", "Order_OrderID");
            CreateIndex("dbo.PaymentOption", "Customer_Email");
            CreateIndex("dbo.Order", "PaymentOption_ID");
            AddForeignKey("dbo.PaymentOption", "Customer_Email", "dbo.Customer", "Email");
            AddForeignKey("dbo.Order", "PaymentOption_ID", "dbo.PaymentOption", "ID");
        }
    }
}
