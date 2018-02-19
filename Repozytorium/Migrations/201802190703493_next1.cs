namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class next1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsedNumber",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Used = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Fuel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fuel");
            DropTable("dbo.UsedNumber");
        }
    }
}
