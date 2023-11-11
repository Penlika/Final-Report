namespace Final_Report.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdadadas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ADMIN",
                c => new
                    {
                        USERNAME = c.String(nullable: false, maxLength: 20),
                        ID = c.Int(nullable: false),
                        EMAIL = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.USERNAME)
                .ForeignKey("dbo.USERS", t => t.ID, cascadeDelete: true)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.USERS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PICTURES = c.String(),
                        USERNAME = c.String(nullable: false, maxLength: 20),
                        PASSWORD = c.String(nullable: false, maxLength: 30),
                        EMAIL = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BOOKING_FORM",
                c => new
                    {
                        IDPACKAGE = c.Int(nullable: false),
                        IDCUSTOMER = c.Int(),
                        BOOKING_DETAIL = c.String(nullable: false),
                        TOTALPRICE = c.Double(nullable: false),
                        NUMOFPERSON = c.Int(nullable: false),
                        STATUS = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.IDPACKAGE)
                .ForeignKey("dbo.USERS", t => t.IDCUSTOMER)
                .Index(t => t.IDCUSTOMER);
            
            CreateTable(
                "dbo.COMMENTANDRATING",
                c => new
                    {
                        COMMENT = c.String(nullable: false, maxLength: 400),
                        RATING = c.Int(nullable: false),
                        IDCUSTOMER = c.Int(),
                        IDPACKAGES = c.Int(),
                    })
                .PrimaryKey(t => new { t.COMMENT, t.RATING })
                .ForeignKey("dbo.PACKAGES", t => t.IDPACKAGES)
                .ForeignKey("dbo.USERS", t => t.IDCUSTOMER)
                .Index(t => t.IDCUSTOMER)
                .Index(t => t.IDPACKAGES);
            
            CreateTable(
                "dbo.PACKAGES",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PACKAGENAME = c.String(nullable: false, maxLength: 50),
                        PICTURES = c.String(),
                        IDFLIGHT = c.Int(),
                        IDHOTEL = c.Int(),
                        DESTINATION = c.String(maxLength: 200),
                        INFORMATION = c.String(nullable: false, maxLength: 250),
                        PACKAGEPRICE_PER_PERSON = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FLIGHTS", t => t.IDFLIGHT)
                .ForeignKey("dbo.HOTELS", t => t.IDHOTEL)
                .Index(t => t.IDFLIGHT)
                .Index(t => t.IDHOTEL);
            
            CreateTable(
                "dbo.CUSTOMER",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        USERNAME = c.String(nullable: false, maxLength: 20),
                        PHONENUMBER = c.Int(nullable: false),
                        ADDRESS = c.String(nullable: false, maxLength: 100),
                        EMAIL = c.String(maxLength: 50),
                        BOOKINGLISTS = c.Int(),
                    })
                .PrimaryKey(t => new { t.ID, t.USERNAME, t.PHONENUMBER, t.ADDRESS })
                .ForeignKey("dbo.PACKAGES", t => t.BOOKINGLISTS)
                .ForeignKey("dbo.USERS", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.BOOKINGLISTS);
            
            CreateTable(
                "dbo.FLIGHTS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        COMPANY = c.String(nullable: false, maxLength: 30),
                        DEPARTURE = c.DateTime(nullable: false),
                        ARRIVAL = c.DateTime(nullable: false),
                        FROM = c.String(nullable: false, maxLength: 100),
                        TO = c.String(nullable: false, maxLength: 100),
                        PRICE_PER_PERSON = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HOTELS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NAME = c.String(nullable: false, maxLength: 50),
                        ADDRESS = c.String(nullable: false, maxLength: 100),
                        PICTURES = c.String(),
                        INFORMATION = c.String(nullable: false, maxLength: 250),
                        PRICE_PER_PERSON = c.Double(nullable: false),
                        ROOM_AVAILABLE = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CUSTOMER", "ID", "dbo.USERS");
            DropForeignKey("dbo.COMMENTANDRATING", "IDCUSTOMER", "dbo.USERS");
            DropForeignKey("dbo.PACKAGES", "IDHOTEL", "dbo.HOTELS");
            DropForeignKey("dbo.PACKAGES", "IDFLIGHT", "dbo.FLIGHTS");
            DropForeignKey("dbo.CUSTOMER", "BOOKINGLISTS", "dbo.PACKAGES");
            DropForeignKey("dbo.COMMENTANDRATING", "IDPACKAGES", "dbo.PACKAGES");
            DropForeignKey("dbo.BOOKING_FORM", "IDCUSTOMER", "dbo.USERS");
            DropForeignKey("dbo.ADMIN", "ID", "dbo.USERS");
            DropIndex("dbo.CUSTOMER", new[] { "BOOKINGLISTS" });
            DropIndex("dbo.CUSTOMER", new[] { "ID" });
            DropIndex("dbo.PACKAGES", new[] { "IDHOTEL" });
            DropIndex("dbo.PACKAGES", new[] { "IDFLIGHT" });
            DropIndex("dbo.COMMENTANDRATING", new[] { "IDPACKAGES" });
            DropIndex("dbo.COMMENTANDRATING", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKING_FORM", new[] { "IDCUSTOMER" });
            DropIndex("dbo.ADMIN", new[] { "ID" });
            DropTable("dbo.HOTELS");
            DropTable("dbo.FLIGHTS");
            DropTable("dbo.CUSTOMER");
            DropTable("dbo.PACKAGES");
            DropTable("dbo.COMMENTANDRATING");
            DropTable("dbo.BOOKING_FORM");
            DropTable("dbo.USERS");
            DropTable("dbo.ADMIN");
        }
    }
}
