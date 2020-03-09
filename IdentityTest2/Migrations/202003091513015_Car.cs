namespace IdentityTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Car : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Brand_BrandID = c.Int(),
                        Type_TypeId = c.Int(),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.CarBrands", t => t.Brand_BrandID)
                .ForeignKey("dbo.CarTypes", t => t.Type_TypeId)
                .Index(t => t.Brand_BrandID)
                .Index(t => t.Type_TypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "Type_TypeId", "dbo.CarTypes");
            DropForeignKey("dbo.Cars", "Brand_BrandID", "dbo.CarBrands");
            DropIndex("dbo.Cars", new[] { "Type_TypeId" });
            DropIndex("dbo.Cars", new[] { "Brand_BrandID" });
            DropTable("dbo.Cars");
        }
    }
}
