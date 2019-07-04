namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identityAddedToComments : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Comment");
            AlterColumn("dbo.Comment", "CommentId", c => c.Long(nullable: false, identity: true));
            //AddPrimaryKey("dbo.Comment", "CommentId");
        }
        
        public override void Down()
        {
            //DropPrimaryKey("dbo.Comment");
            AlterColumn("dbo.Comment", "CommentId", c => c.Long(nullable: false));
            //AddPrimaryKey("dbo.Comment", "CommentId");
        }
    }
}
