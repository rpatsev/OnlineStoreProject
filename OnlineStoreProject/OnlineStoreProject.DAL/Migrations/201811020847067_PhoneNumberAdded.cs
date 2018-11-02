namespace OnlineStoreProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneNumberAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientProfiles", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientProfiles", "PhoneNumber");
        }
    }
}
