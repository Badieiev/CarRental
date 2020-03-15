namespace CarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrandIdCarId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "Brand_BrandID", "dbo.CarBrands");
            DropForeignKey("dbo.Cars", "Type_TypeId", "dbo.CarTypes");
            DropIndex("dbo.Cars", new[] { "Brand_BrandID" });
            DropIndex("dbo.Cars", new[] { "Type_TypeId" });
            RenameColumn(table: "dbo.Cars", name: "Brand_BrandID", newName: "BrandId");
            RenameColumn(table: "dbo.Cars", name: "Type_TypeId", newName: "TypeId");
            AlterColumn("dbo.Cars", "BrandId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "TypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "BrandId");
            CreateIndex("dbo.Cars", "TypeId");
            AddForeignKey("dbo.Cars", "BrandId", "dbo.CarBrands", "BrandID", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "TypeId", "dbo.CarTypes", "TypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "TypeId", "dbo.CarTypes");
            DropForeignKey("dbo.Cars", "BrandId", "dbo.CarBrands");
            DropIndex("dbo.Cars", new[] { "TypeId" });
            DropIndex("dbo.Cars", new[] { "BrandId" });
            AlterColumn("dbo.Cars", "TypeId", c => c.Int());
            AlterColumn("dbo.Cars", "BrandId", c => c.Int());
            RenameColumn(table: "dbo.Cars", name: "TypeId", newName: "Type_TypeId");
            RenameColumn(table: "dbo.Cars", name: "BrandId", newName: "Brand_BrandID");
            CreateIndex("dbo.Cars", "Type_TypeId");
            CreateIndex("dbo.Cars", "Brand_BrandID");
            AddForeignKey("dbo.Cars", "Type_TypeId", "dbo.CarTypes", "TypeId");
            AddForeignKey("dbo.Cars", "Brand_BrandID", "dbo.CarBrands", "BrandID");
        }
    }
}
