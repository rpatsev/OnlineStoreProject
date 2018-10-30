namespace OnlineStoreProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intUserIdPropRemove : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BirthDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        RegisteredAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        City = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Feedbacks", "ClientProfile_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "ClientProfile_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Feedbacks", "ClientProfile_Id");
            CreateIndex("dbo.Orders", "ClientProfile_Id");
            AddForeignKey("dbo.Feedbacks", "ClientProfile_Id", "dbo.ClientProfiles", "Id");
            AddForeignKey("dbo.Orders", "ClientProfile_Id", "dbo.ClientProfiles", "Id");
            DropColumn("dbo.AspNetUsers", "UserId");
            DropColumn("dbo.AspNetUsers", "Password");
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "RegisteredAt");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "RegisteredAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orders", "ClientProfile_Id", "dbo.ClientProfiles");
            DropForeignKey("dbo.Feedbacks", "ClientProfile_Id", "dbo.ClientProfiles");
            DropForeignKey("dbo.ClientProfiles", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClientProfiles", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "ClientProfile_Id" });
            DropIndex("dbo.Feedbacks", new[] { "ClientProfile_Id" });
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.Orders", "ClientProfile_Id");
            DropColumn("dbo.Feedbacks", "ClientProfile_Id");
            DropTable("dbo.ClientProfiles");
        }
    }
}
