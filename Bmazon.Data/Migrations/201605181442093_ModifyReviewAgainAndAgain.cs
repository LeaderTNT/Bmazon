namespace Bmazon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyReviewAgainAndAgain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Review", "Product_ProductID", "dbo.Product");
            DropIndex("dbo.Review", new[] { "Product_ProductID" });
            DropColumn("dbo.Review", "ProductID");
            RenameColumn(table: "dbo.Review", name: "Product_ProductID", newName: "ProductID");
            AlterColumn("dbo.Review", "ProductID", c => c.Int(nullable: false));
            AlterColumn("dbo.Review", "ProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.Review", "ProductID");
            AddForeignKey("dbo.Review", "ProductID", "dbo.Product", "ProductID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "ProductID", "dbo.Product");
            DropIndex("dbo.Review", new[] { "ProductID" });
            AlterColumn("dbo.Review", "ProductID", c => c.Int());
            AlterColumn("dbo.Review", "ProductID", c => c.String());
            RenameColumn(table: "dbo.Review", name: "ProductID", newName: "Product_ProductID");
            AddColumn("dbo.Review", "ProductID", c => c.String());
            CreateIndex("dbo.Review", "Product_ProductID");
            AddForeignKey("dbo.Review", "Product_ProductID", "dbo.Product", "ProductID");
        }
    }
}
