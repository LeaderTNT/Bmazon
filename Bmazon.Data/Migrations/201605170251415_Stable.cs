namespace Bmazon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                        TotalPayment = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        Buyer = c.String(),
                        Seller = c.String(),
                        TotalPayment = c.Double(nullable: false),
                        PaymentOption_ID = c.Int(),
                        Customer_Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.PaymentOption", t => t.PaymentOption_ID)
                .ForeignKey("dbo.Customer", t => t.Customer_Email)
                .Index(t => t.PaymentOption_ID)
                .Index(t => t.Customer_Email);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.Customer_Email)
                .Index(t => t.Customer_Email);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.String(),
                        ProductID = c.String(),
                        SellerID = c.String(),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(),
                        Customer_Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.Customer_Email)
                .Index(t => t.Customer_Email);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Seller = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        AvailableNum = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Buyer = c.String(),
                        Quantity = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Seller_Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Seller", t => t.Seller_Email)
                .Index(t => t.Seller_Email);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        BmazonAccount_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.BmazonAccount", t => t.BmazonAccount_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.BmazonAccount_Id);
            
            CreateTable(
                "dbo.BmazonAccount",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        BmazonAccount_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BmazonAccount", t => t.BmazonAccount_Id)
                .Index(t => t.BmazonAccount_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        BmazonAccount_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.BmazonAccount", t => t.BmazonAccount_Id)
                .Index(t => t.BmazonAccount_Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Cart_CustomerID = c.String(maxLength: 128),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        PhoneNumber = c.String(),
                        BillingAddress = c.String(),
                        ShippingAddress = c.String(),
                    })
                .PrimaryKey(t => t.Email)
                .ForeignKey("dbo.Cart", t => t.Cart_CustomerID)
                .Index(t => t.Cart_CustomerID);
            
            CreateTable(
                "dbo.Seller",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        PhoneNumber = c.String(),
                        Company = c.String(),
                        Earning = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customer", "Cart_CustomerID", "dbo.Cart");
            DropForeignKey("dbo.IdentityUserRole", "BmazonAccount_Id", "dbo.BmazonAccount");
            DropForeignKey("dbo.IdentityUserLogin", "BmazonAccount_Id", "dbo.BmazonAccount");
            DropForeignKey("dbo.IdentityUserClaim", "BmazonAccount_Id", "dbo.BmazonAccount");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Product", "Seller_Email", "dbo.Seller");
            DropForeignKey("dbo.Review", "Customer_Email", "dbo.Customer");
            DropForeignKey("dbo.PaymentOption", "Customer_Email", "dbo.Customer");
            DropForeignKey("dbo.Order", "Customer_Email", "dbo.Customer");
            DropForeignKey("dbo.Order", "PaymentOption_ID", "dbo.PaymentOption");
            DropIndex("dbo.Customer", new[] { "Cart_CustomerID" });
            DropIndex("dbo.IdentityUserLogin", new[] { "BmazonAccount_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "BmazonAccount_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "BmazonAccount_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Product", new[] { "Seller_Email" });
            DropIndex("dbo.Review", new[] { "Customer_Email" });
            DropIndex("dbo.PaymentOption", new[] { "Customer_Email" });
            DropIndex("dbo.Order", new[] { "Customer_Email" });
            DropIndex("dbo.Order", new[] { "PaymentOption_ID" });
            DropTable("dbo.Seller");
            DropTable("dbo.Customer");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.BmazonAccount");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Product");
            DropTable("dbo.Review");
            DropTable("dbo.PaymentOption");
            DropTable("dbo.Order");
            DropTable("dbo.Cart");
        }
    }
}
