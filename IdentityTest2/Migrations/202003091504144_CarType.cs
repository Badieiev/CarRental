namespace IdentityTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.TypeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarTypes");
        }
    }
}
