namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDescriptionColumnFromExtra : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Extras", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Extras", "Description", c => c.String(maxLength: 500));
        }
    }
}
