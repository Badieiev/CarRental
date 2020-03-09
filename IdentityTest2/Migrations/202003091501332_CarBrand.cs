namespace IdentityTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarBrand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarBrands",
                c => new
                    {
                        BrandID = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                    })
                .PrimaryKey(t => t.BrandID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarBrands");
        }
    }
}
