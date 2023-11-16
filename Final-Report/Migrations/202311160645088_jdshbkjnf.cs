namespace Final_Report.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jdshbkjnf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ADMIN",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        USERNAME = c.String(nullable: false, maxLength: 20),
                        PASSWORD = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BOOKINGFLIGHT",
                c => new
                    {
                        TOTALPRICE = c.Double(nullable: false),
                        NUMOFPERSON = c.Int(nullable: false),
                        STATUS = c.String(nullable: false, maxLength: 20),
                        IDFLIGHT = c.Int(),
                        IDCUSTOMER = c.Int(),
                        BOOKING_DETAIL = c.String(),
                    })
                .PrimaryKey(t => new { t.TOTALPRICE, t.NUMOFPERSON, t.STATUS })
                .ForeignKey("dbo.CUSTOMER", t => t.IDCUSTOMER)
                .ForeignKey("dbo.FLIGHT", t => t.IDFLIGHT)
                .Index(t => t.IDFLIGHT)
                .Index(t => t.IDCUSTOMER);
            
            CreateTable(
                "dbo.CUSTOMER",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        USERNAME = c.String(nullable: false, maxLength: 20),
                        PASSWORD = c.String(nullable: false, maxLength: 16, unicode: false),
                        PICTURES = c.String(),
                        EMAIL = c.String(nullable: false, maxLength: 50),
                        PHONENUMBER = c.String(nullable: false, maxLength: 10, unicode: false),
                        ADDRESS = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BOOKINGHOTEL",
                c => new
                    {
                        TOTALPRICE = c.Double(nullable: false),
                        NUMOFPERSON = c.Int(nullable: false),
                        STATUS = c.String(nullable: false, maxLength: 20),
                        IDHOTEL = c.Int(),
                        IDCUSTOMER = c.Int(),
                        BOOKING_DETAIL = c.String(),
                    })
                .PrimaryKey(t => new { t.TOTALPRICE, t.NUMOFPERSON, t.STATUS })
                .ForeignKey("dbo.HOTEL", t => t.IDHOTEL)
                .ForeignKey("dbo.CUSTOMER", t => t.IDCUSTOMER)
                .Index(t => t.IDHOTEL)
                .Index(t => t.IDCUSTOMER);
            
            CreateTable(
                "dbo.HOTEL",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NAME = c.String(nullable: false, maxLength: 50),
                        ADDRESS = c.String(nullable: false, maxLength: 100),
                        PICTURES = c.String(),
                        INFORMATION = c.String(maxLength: 250),
                        PRICE_PER_PERSON = c.Double(nullable: false),
                        ROOM_AVAILABLE = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PACKAGE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PACKAGENAME = c.String(nullable: false, maxLength: 50),
                        PICTURES = c.String(),
                        IDFLIGHT = c.Int(),
                        IDHOTEL = c.Int(),
                        DESTINATION = c.String(nullable: false, maxLength: 200),
                        INFORMATION = c.String(nullable: false, maxLength: 250),
                        PROLONG = c.String(nullable: false, maxLength: 50),
                        PACKAGEPRICE_PER_PERSON = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FLIGHT", t => t.IDFLIGHT)
                .ForeignKey("dbo.HOTEL", t => t.IDHOTEL)
                .Index(t => t.IDFLIGHT)
                .Index(t => t.IDHOTEL);
            
            CreateTable(
                "dbo.BOOKINGPACKAGE",
                c => new
                    {
                        TOTALPRICE = c.Double(nullable: false),
                        NUMOFPERSON = c.Int(nullable: false),
                        STATUS = c.String(nullable: false, maxLength: 20),
                        IDPACKAGE = c.Int(),
                        IDCUSTOMER = c.Int(),
                        BOOKING_DETAIL = c.String(),
                    })
                .PrimaryKey(t => new { t.TOTALPRICE, t.NUMOFPERSON, t.STATUS })
                .ForeignKey("dbo.PACKAGE", t => t.IDPACKAGE)
                .ForeignKey("dbo.CUSTOMER", t => t.IDCUSTOMER)
                .Index(t => t.IDPACKAGE)
                .Index(t => t.IDCUSTOMER);
            
            CreateTable(
                "dbo.COMMENTANDRATING",
                c => new
                    {
                        COMMENT = c.String(nullable: false, maxLength: 400),
                        RATING = c.Double(nullable: false),
                        IDCUSTOMER = c.Int(),
                        IDPACKAGES = c.Int(),
                    })
                .PrimaryKey(t => new { t.COMMENT, t.RATING })
                .ForeignKey("dbo.PACKAGE", t => t.IDPACKAGES)
                .ForeignKey("dbo.CUSTOMER", t => t.IDCUSTOMER)
                .Index(t => t.IDCUSTOMER)
                .Index(t => t.IDPACKAGES);
            
            CreateTable(
                "dbo.FLIGHT",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        COMPANY = c.String(nullable: false, maxLength: 30),
                        DEPARTURE = c.DateTime(nullable: false),
                        ARRIVAL = c.DateTime(nullable: false),
                        FROM = c.String(nullable: false, maxLength: 100),
                        TO = c.String(nullable: false, maxLength: 100),
                        PRICE_PER_PERSON = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.COMMENTANDRATING", "IDCUSTOMER", "dbo.CUSTOMER");
            DropForeignKey("dbo.BOOKINGPACKAGE", "IDCUSTOMER", "dbo.CUSTOMER");
            DropForeignKey("dbo.BOOKINGHOTEL", "IDCUSTOMER", "dbo.CUSTOMER");
            DropForeignKey("dbo.PACKAGE", "IDHOTEL", "dbo.HOTEL");
            DropForeignKey("dbo.PACKAGE", "IDFLIGHT", "dbo.FLIGHT");
            DropForeignKey("dbo.BOOKINGFLIGHT", "IDFLIGHT", "dbo.FLIGHT");
            DropForeignKey("dbo.COMMENTANDRATING", "IDPACKAGES", "dbo.PACKAGE");
            DropForeignKey("dbo.BOOKINGPACKAGE", "IDPACKAGE", "dbo.PACKAGE");
            DropForeignKey("dbo.BOOKINGHOTEL", "IDHOTEL", "dbo.HOTEL");
            DropForeignKey("dbo.BOOKINGFLIGHT", "IDCUSTOMER", "dbo.CUSTOMER");
            DropIndex("dbo.COMMENTANDRATING", new[] { "IDPACKAGES" });
            DropIndex("dbo.COMMENTANDRATING", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGPACKAGE", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGPACKAGE", new[] { "IDPACKAGE" });
            DropIndex("dbo.PACKAGE", new[] { "IDHOTEL" });
            DropIndex("dbo.PACKAGE", new[] { "IDFLIGHT" });
            DropIndex("dbo.BOOKINGHOTEL", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGHOTEL", new[] { "IDHOTEL" });
            DropIndex("dbo.BOOKINGFLIGHT", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGFLIGHT", new[] { "IDFLIGHT" });
            DropTable("dbo.FLIGHT");
            DropTable("dbo.COMMENTANDRATING");
            DropTable("dbo.BOOKINGPACKAGE");
            DropTable("dbo.PACKAGE");
            DropTable("dbo.HOTEL");
            DropTable("dbo.BOOKINGHOTEL");
            DropTable("dbo.CUSTOMER");
            DropTable("dbo.BOOKINGFLIGHT");
            DropTable("dbo.ADMIN");
        }
    }
}
