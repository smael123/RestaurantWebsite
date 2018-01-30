namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNavigationalPropertiesTooFoodAndExtra : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Extras", "Food_Id", "dbo.Foods");
            DropIndex("dbo.Extras", new[] { "Food_Id" });
            RenameColumn(table: "dbo.Extras", name: "Food_Id", newName: "FoodId");
            AlterColumn("dbo.Extras", "FoodId", c => c.Int(nullable: false));
            CreateIndex("dbo.Extras", "FoodId");
            AddForeignKey("dbo.Extras", "FoodId", "dbo.Foods", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Extras", "FoodId", "dbo.Foods");
            DropIndex("dbo.Extras", new[] { "FoodId" });
            AlterColumn("dbo.Extras", "FoodId", c => c.Int());
            RenameColumn(table: "dbo.Extras", name: "FoodId", newName: "Food_Id");
            CreateIndex("dbo.Extras", "Food_Id");
            AddForeignKey("dbo.Extras", "Food_Id", "dbo.Foods", "Id");
        }
    }
}
