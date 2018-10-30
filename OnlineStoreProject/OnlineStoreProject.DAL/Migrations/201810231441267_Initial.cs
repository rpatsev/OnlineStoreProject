namespace OnlineStoreProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedbacks", "Points", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "Points", c => c.Single(nullable: false));
        }
    }
}
