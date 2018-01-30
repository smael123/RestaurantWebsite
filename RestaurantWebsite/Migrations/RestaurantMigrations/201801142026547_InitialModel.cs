namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Extras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 500),
                        AddedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Food_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.Food_Id)
                .Index(t => t.Food_Id);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 500),
                        BasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Special_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specials", t => t.Special_Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Special_Id);
            
            CreateTable(
                "dbo.Specials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 500),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Foods", "Special_Id", "dbo.Specials");
            DropForeignKey("dbo.Extras", "Food_Id", "dbo.Foods");
            DropIndex("dbo.Foods", new[] { "Special_Id" });
            DropIndex("dbo.Foods", new[] { "Name" });
            DropIndex("dbo.Extras", new[] { "Food_Id" });
            DropTable("dbo.Specials");
            DropTable("dbo.Foods");
            DropTable("dbo.Extras");
        }
    }
}
