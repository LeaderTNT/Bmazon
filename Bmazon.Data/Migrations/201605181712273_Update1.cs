namespace Bmazon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "SellerEmail", c => c.String());
            DropColumn("dbo.Review", "SellerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Review", "SellerID", c => c.String());
            DropColumn("dbo.Review", "SellerEmail");
        }
    }
}
