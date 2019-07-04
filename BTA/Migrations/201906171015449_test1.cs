namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LifeInCItyViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category_CategoryId = c.Long(),
                        City_CityId = c.Long(),
                        Line_LineId = c.Long(),
                        POI_PoiId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.Category_CategoryId)
                .ForeignKey("dbo.City", t => t.City_CityId)
                .ForeignKey("dbo.Line", t => t.Line_LineId)
                .ForeignKey("dbo.POI", t => t.POI_PoiId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.City_CityId)
                .Index(t => t.Line_LineId)
                .Index(t => t.POI_PoiId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LifeInCItyViewModels", "POI_PoiId", "dbo.POI");
            DropForeignKey("dbo.LifeInCItyViewModels", "Line_LineId", "dbo.Line");
            DropForeignKey("dbo.LifeInCItyViewModels", "City_CityId", "dbo.City");
            DropForeignKey("dbo.LifeInCItyViewModels", "Category_CategoryId", "dbo.Category");
            DropIndex("dbo.LifeInCItyViewModels", new[] { "POI_PoiId" });
            DropIndex("dbo.LifeInCItyViewModels", new[] { "Line_LineId" });
            DropIndex("dbo.LifeInCItyViewModels", new[] { "City_CityId" });
            DropIndex("dbo.LifeInCItyViewModels", new[] { "Category_CategoryId" });
            DropTable("dbo.LifeInCItyViewModels");
        }
    }
}
