namespace Bmazon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyReview : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Review", "CustomerID");
            DropColumn("dbo.Review", "ProductID");
            DropColumn("dbo.Review", "SellerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Review", "SellerID", c => c.String());
            AddColumn("dbo.Review", "ProductID", c => c.String());
            AddColumn("dbo.Review", "CustomerID", c => c.String());
        }
    }
}
