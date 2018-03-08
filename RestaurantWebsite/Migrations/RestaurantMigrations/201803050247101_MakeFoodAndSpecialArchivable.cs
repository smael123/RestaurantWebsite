namespace RestaurantWebsite.Migrations.RestaurantMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeFoodAndSpecialArchivable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "IsArchived", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Specials", "IsArchived", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Specials", "IsArchived");
            DropColumn("dbo.Foods", "IsArchived");
        }
    }
}
