namespace Bmazon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Seller", "CompanyIndex");
            AlterColumn("dbo.Seller", "Company", c => c.String(maxLength: 128));
            CreateIndex("dbo.Seller", "Company", unique: true, name: "CompanyIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Seller", "CompanyIndex");
            AlterColumn("dbo.Seller", "Company", c => c.String());
            CreateIndex("dbo.Seller", "Company", unique: true, name: "CompanyIndex");
        }
    }
}
