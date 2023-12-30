namespace Final_Report.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adhv : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ADMIN", new[] { "ID" });
            DropIndex("dbo.CUSTOMER", new[] { "ID" });
            DropPrimaryKey("dbo.ADMIN");
            DropPrimaryKey("dbo.COMMENTANDRATING");
            DropPrimaryKey("dbo.CUSTOMER");
            CreateTable(
                "dbo.MANAGER",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        PICTURES = c.String(),
                        USERNAME = c.String(maxLength: 20),
                        NAME = c.String(maxLength: 30),
                        EMAIL = c.String(maxLength: 40),
                        PHONENUMBER = c.String(maxLength: 10),
                        ADDRESS = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ACCOUNT", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.LOGOIMG",
                c => new
                    {
                        COMPANY = c.String(nullable: false, maxLength: 40),
                        LOGO = c.String(),
                    })
                .PrimaryKey(t => t.COMPANY);
            
            AddColumn("dbo.FLIGHT", "LOGO", c => c.String());
            AddColumn("dbo.HOTEL", "PICTURE1", c => c.String());
            AddColumn("dbo.HOTEL", "PICTURE2", c => c.String());
            AddColumn("dbo.HOTEL", "PICTURE3", c => c.String());
            AddColumn("dbo.HOTEL", "PICTURE4", c => c.String());
            AddColumn("dbo.HOTEL", "PICTURE5", c => c.String());
            AddColumn("dbo.HOTEL", "PICTURE6", c => c.String());
            AddColumn("dbo.HOTEL", "RATING", c => c.Int());
            AddColumn("dbo.COMMENTANDRATING", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.COMMENTANDRATING", "STAR_RATING", c => c.Int(nullable: false));
            AddColumn("dbo.COMMENTANDRATING", "DATECREATE", c => c.DateTime());
            AlterColumn("dbo.ACCOUNT", "EMAIL", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ADMIN", "USERNAME", c => c.String(maxLength: 20));
            AlterColumn("dbo.ADMIN", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.FLIGHT", "COMPANY", c => c.String(maxLength: 40));
            AlterColumn("dbo.HOTEL", "ADDRESS", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.HOTEL", "INFORMATION", c => c.String());
            AlterColumn("dbo.CUSTOMER", "USERNAME", c => c.String(maxLength: 20));
            AlterColumn("dbo.CUSTOMER", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.CUSTOMER", "DATEOFBIRTH", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.CUSTOMER", "EMAIL", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CUSTOMER", "PHONENUMBER", c => c.String(maxLength: 10));
            AlterColumn("dbo.CUSTOMER", "CARDNUM", c => c.String(maxLength: 16));
            AlterColumn("dbo.CUSTOMER", "SECURNUM", c => c.String(maxLength: 3));
            AddPrimaryKey("dbo.ADMIN", "ID");
            AddPrimaryKey("dbo.COMMENTANDRATING", "ID");
            AddPrimaryKey("dbo.CUSTOMER", new[] { "ID", "EMAIL" });
            CreateIndex("dbo.ADMIN", "ID");
            CreateIndex("dbo.CUSTOMER", "ID");
            DropColumn("dbo.FLIGHT", "PICTURES");
            DropColumn("dbo.HOTEL", "PICTURES");
            DropColumn("dbo.COMMENTANDRATING", "RATING");
        }
        
        public override void Down()
        {
            AddColumn("dbo.COMMENTANDRATING", "RATING", c => c.Double(nullable: false));
            AddColumn("dbo.HOTEL", "PICTURES", c => c.String());
            AddColumn("dbo.FLIGHT", "PICTURES", c => c.String());
            DropForeignKey("dbo.MANAGER", "ID", "dbo.ACCOUNT");
            DropIndex("dbo.MANAGER", new[] { "ID" });
            DropIndex("dbo.CUSTOMER", new[] { "ID" });
            DropIndex("dbo.ADMIN", new[] { "ID" });
            DropPrimaryKey("dbo.CUSTOMER");
            DropPrimaryKey("dbo.COMMENTANDRATING");
            DropPrimaryKey("dbo.ADMIN");
            AlterColumn("dbo.CUSTOMER", "SECURNUM", c => c.Int());
            AlterColumn("dbo.CUSTOMER", "CARDNUM", c => c.Int());
            AlterColumn("dbo.CUSTOMER", "PHONENUMBER", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.CUSTOMER", "EMAIL", c => c.String(maxLength: 50));
            AlterColumn("dbo.CUSTOMER", "DATEOFBIRTH", c => c.DateTime());
            AlterColumn("dbo.CUSTOMER", "ID", c => c.Int());
            AlterColumn("dbo.CUSTOMER", "USERNAME", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.HOTEL", "INFORMATION", c => c.String(maxLength: 250));
            AlterColumn("dbo.HOTEL", "ADDRESS", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.FLIGHT", "COMPANY", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.ADMIN", "ID", c => c.Int());
            AlterColumn("dbo.ADMIN", "USERNAME", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ACCOUNT", "EMAIL", c => c.String(nullable: false));
            DropColumn("dbo.COMMENTANDRATING", "DATECREATE");
            DropColumn("dbo.COMMENTANDRATING", "STAR_RATING");
            DropColumn("dbo.COMMENTANDRATING", "ID");
            DropColumn("dbo.HOTEL", "RATING");
            DropColumn("dbo.HOTEL", "PICTURE6");
            DropColumn("dbo.HOTEL", "PICTURE5");
            DropColumn("dbo.HOTEL", "PICTURE4");
            DropColumn("dbo.HOTEL", "PICTURE3");
            DropColumn("dbo.HOTEL", "PICTURE2");
            DropColumn("dbo.HOTEL", "PICTURE1");
            DropColumn("dbo.FLIGHT", "LOGO");
            DropTable("dbo.LOGOIMG");
            DropTable("dbo.MANAGER");
            AddPrimaryKey("dbo.CUSTOMER", "USERNAME");
            AddPrimaryKey("dbo.COMMENTANDRATING", new[] { "COMMENT", "RATING" });
            AddPrimaryKey("dbo.ADMIN", "USERNAME");
            CreateIndex("dbo.CUSTOMER", "ID");
            CreateIndex("dbo.ADMIN", "ID");
        }
    }
}
