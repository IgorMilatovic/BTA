namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectUserToAspUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TABLE Traveler WITH NOCHECK ADD FOREIGN KEY(IdentityId) REFERENCES AspNetUsers(Id);");
        }
        
        public override void Down()
        {
        }
    }
}
