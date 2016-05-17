namespace Bmazon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetCompanyUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Seller", "Company", unique: true, name: "CompanyIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Seller", "CompanyIndex");
        }
    }
}
