namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToManyRelToSpecialsAndFoods : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foods", "Special_Id", "dbo.Specials");
            DropIndex("dbo.Foods", new[] { "Special_Id" });
            CreateTable(
                "dbo.SpecialFoods",
                c => new
                    {
                        SpecialId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SpecialId, t.FoodId })
                .ForeignKey("dbo.Specials", t => t.SpecialId, cascadeDelete: true)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .Index(t => t.SpecialId)
                .Index(t => t.FoodId);
            
            DropColumn("dbo.Foods", "Special_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "Special_Id", c => c.Int());
            DropForeignKey("dbo.SpecialFoods", "FoodId", "dbo.Foods");
            DropForeignKey("dbo.SpecialFoods", "SpecialId", "dbo.Specials");
            DropIndex("dbo.SpecialFoods", new[] { "FoodId" });
            DropIndex("dbo.SpecialFoods", new[] { "SpecialId" });
            DropTable("dbo.SpecialFoods");
            CreateIndex("dbo.Foods", "Special_Id");
            AddForeignKey("dbo.Foods", "Special_Id", "dbo.Specials", "Id");
        }
    }
}
