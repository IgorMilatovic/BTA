namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Category",
            //    c => new
            //        {
            //            CategoryId = c.Long(nullable: false, identity: true),
            //            CategoryName = c.String(nullable: false, maxLength: 50),
            //            Module = c.Long(),
            //            Active = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CategoryId)
            //    .ForeignKey("dbo.Module", t => t.Module)
            //    .Index(t => t.Module);
            
            //CreateTable(
            //    "dbo.Module",
            //    c => new
            //        {
            //            ModuleId = c.Long(nullable: false, identity: true),
            //            ModuleName = c.String(nullable: false, maxLength: 50),
            //            Active = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ModuleId);
            
            //CreateTable(
            //    "dbo.POI",
            //    c => new
            //        {
            //            PoiId = c.Long(nullable: false),
            //            CityId = c.Long(),
            //            Name = c.String(maxLength: 50),
            //            Address = c.String(maxLength: 150),
            //            Website = c.String(maxLength: 2083),
            //            PoiImg = c.String(maxLength: 2083),
            //            Rating = c.Double(nullable: false),
            //            Lon = c.Double(),
            //            Lat = c.Double(),
            //            Phone = c.String(maxLength: 50),
            //            Email = c.String(maxLength: 50),
            //            CategoryId = c.Long(),
            //            POIDescription = c.String(),
            //            Active = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.PoiId)
            //    .ForeignKey("dbo.Category", t => t.CategoryId)
            //    .ForeignKey("dbo.City", t => t.CityId)
            //    .Index(t => t.CityId)
            //    .Index(t => t.CategoryId);
            
            //CreateTable(
            //    "dbo.City",
            //    c => new
            //        {
            //            CityId = c.Long(nullable: false),
            //            CityName = c.String(nullable: false, maxLength: 20),
            //            Country = c.String(maxLength: 20),
            //            Lon = c.Double(),
            //            Lat = c.Double(),
            //            Population = c.Int(),
            //            ImgUrl = c.String(maxLength: 2083),
            //            Active = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CityId);
            
            //CreateTable(
            //    "dbo.Line",
            //    c => new
            //        {
            //            LineId = c.Long(nullable: false, identity: true),
            //            PoiId = c.Long(),
            //            SourceCity = c.Long(),
            //            DestCity = c.Long(),
            //            Active = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.LineId)
            //    .ForeignKey("dbo.POI", t => t.PoiId)
            //    .ForeignKey("dbo.City", t => t.SourceCity)
            //    .ForeignKey("dbo.City", t => t.DestCity)
            //    .Index(t => t.PoiId)
            //    .Index(t => t.SourceCity)
            //    .Index(t => t.DestCity);
            
            //CreateTable(
            //    "dbo.Comment",
            //    c => new
            //        {
            //            CommentId = c.Long(nullable: false),
            //            ParentId = c.Long(),
            //            Traveler = c.Long(),
            //            Title = c.String(maxLength: 50),
            //            Date = c.DateTime(nullable: false, storeType: "date"),
            //            Text = c.String(maxLength: 500),
            //            Rating = c.Int(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            TableName = c.String(maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.CommentId)
            //    .ForeignKey("dbo.Traveler", t => t.Traveler)
            //    .Index(t => t.Traveler);
            
            //CreateTable(
            //    "dbo.Traveler",
            //    c => new
            //        {
            //            UserId = c.Long(nullable: false, identity: true),
            //            IdentityId = c.String(maxLength: 128),
            //            Active = c.Boolean(nullable: false),
            //            ImgUrl = c.String(maxLength: 150),
            //            FullName = c.String(maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Comment", "Traveler", "dbo.Traveler");
            DropForeignKey("dbo.POI", "CityId", "dbo.City");
            DropForeignKey("dbo.Line", "DestCity", "dbo.City");
            DropForeignKey("dbo.Line", "SourceCity", "dbo.City");
            DropForeignKey("dbo.Line", "PoiId", "dbo.POI");
            DropForeignKey("dbo.POI", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Category", "Module", "dbo.Module");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Comment", new[] { "Traveler" });
            DropIndex("dbo.Line", new[] { "DestCity" });
            DropIndex("dbo.Line", new[] { "SourceCity" });
            DropIndex("dbo.Line", new[] { "PoiId" });
            DropIndex("dbo.POI", new[] { "CategoryId" });
            DropIndex("dbo.POI", new[] { "CityId" });
            DropIndex("dbo.Category", new[] { "Module" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Traveler");
            DropTable("dbo.Comment");
            DropTable("dbo.Line");
            DropTable("dbo.City");
            DropTable("dbo.POI");
            DropTable("dbo.Module");
            DropTable("dbo.Category");
        }
    }
}
