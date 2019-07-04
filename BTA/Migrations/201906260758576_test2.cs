namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POI", "Transport", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.POI", "Transport");
        }
    }
}
