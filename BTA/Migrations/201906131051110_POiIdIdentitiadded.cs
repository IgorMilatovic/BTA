namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POiIdIdentitiadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Line", "PoiId", "dbo.POI");
            //DropPrimaryKey("dbo.POI");
            AlterColumn("dbo.POI", "PoiId", c => c.Long(nullable: false, identity: true));
            //AddPrimaryKey("dbo.POI", "PoiId");
            AddForeignKey("dbo.Line", "PoiId", "dbo.POI", "PoiId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Line", "PoiId", "dbo.POI");
            DropPrimaryKey("dbo.POI");
            AlterColumn("dbo.POI", "PoiId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.POI", "PoiId");
            AddForeignKey("dbo.Line", "PoiId", "dbo.POI", "PoiId");
        }
    }
}
