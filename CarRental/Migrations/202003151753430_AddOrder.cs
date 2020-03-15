namespace IdentityTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        PassportId = c.String(),
                        ReturnDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Driver = c.Boolean(nullable: false),
                        DamageDescription = c.String(),
                        DamageCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
