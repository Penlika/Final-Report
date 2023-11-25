namespace Final_Report.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsdadada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ACCOUNT",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        USERNAME = c.String(nullable: false, maxLength: 50),
                        EMAIL = c.String(nullable: false),
                        PASSWORD = c.String(nullable: false, maxLength: 30),
                        ROLE = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ADMIN",
                c => new
                    {
                        USERNAME = c.String(nullable: false, maxLength: 20),
                        ID = c.Int(),
                        PICTURES = c.String(),
                    })
                .PrimaryKey(t => t.USERNAME)
                .ForeignKey("dbo.ACCOUNT", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.APPROVE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDADMIN = c.Int(),
                        IDHOTELBOOK = c.Int(),
                        IDFLIGHTBOOK = c.Int(),
                        IDPACKAGEBOOK = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BOOKINGFLIGHT", t => t.IDFLIGHTBOOK)
                .ForeignKey("dbo.BOOKINGPACKAGE", t => t.IDPACKAGEBOOK)
                .ForeignKey("dbo.BOOKINGHOTEL", t => t.IDHOTELBOOK)
                .ForeignKey("dbo.ACCOUNT", t => t.IDADMIN)
                .Index(t => t.IDADMIN)
                .Index(t => t.IDHOTELBOOK)
                .Index(t => t.IDFLIGHTBOOK)
                .Index(t => t.IDPACKAGEBOOK);
            
            CreateTable(
                "dbo.BOOKINGFLIGHT",
                c => new
                    {
                        IDRECEIPT = c.Int(nullable: false, identity: true),
                        IDFLIGHT = c.Int(),
                        IDCUSTOMER = c.Int(),
                        BOOKING_DETAIL = c.String(),
                        BOOKINGDATE = c.DateTime(),
                        TOTALPRICE = c.Double(nullable: false),
                        NUMOFPERSON = c.Int(nullable: false),
                        STATUS = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.IDRECEIPT)
                .ForeignKey("dbo.FLIGHT", t => t.IDFLIGHT)
                .ForeignKey("dbo.ACCOUNT", t => t.IDCUSTOMER)
                .Index(t => t.IDFLIGHT)
                .Index(t => t.IDCUSTOMER);
            
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
            
            CreateTable(
                "dbo.BOOKINGPACKAGE",
                c => new
                    {
                        IDRECEIPT = c.Int(nullable: false, identity: true),
                        IDCUSTOMER = c.Int(),
                        IDHOTEL = c.Int(),
                        IDFLIGHT = c.Int(),
                        BOOKING_DETAIL = c.String(),
                        BOOKINGDATE = c.DateTime(),
                        TOTALPRICE = c.Double(nullable: false),
                        NUMOFPERSON = c.Int(nullable: false),
                        STATUS = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.IDRECEIPT)
                .ForeignKey("dbo.HOTEL", t => t.IDHOTEL)
                .ForeignKey("dbo.FLIGHT", t => t.IDFLIGHT)
                .ForeignKey("dbo.ACCOUNT", t => t.IDCUSTOMER)
                .Index(t => t.IDCUSTOMER)
                .Index(t => t.IDHOTEL)
                .Index(t => t.IDFLIGHT);
            
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
                "dbo.BOOKINGHOTEL",
                c => new
                    {
                        IDRECEIPT = c.Int(nullable: false, identity: true),
                        IDHOTEL = c.Int(),
                        IDCUSTOMER = c.Int(),
                        BOOKING_DETAIL = c.String(),
                        BOOKINGDATE = c.DateTime(),
                        TOTALPRICE = c.Double(nullable: false),
                        NUMOFPERSON = c.Int(nullable: false),
                        STATUS = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.IDRECEIPT)
                .ForeignKey("dbo.HOTEL", t => t.IDHOTEL)
                .ForeignKey("dbo.ACCOUNT", t => t.IDCUSTOMER)
                .Index(t => t.IDHOTEL)
                .Index(t => t.IDCUSTOMER);
            
            CreateTable(
                "dbo.PAYING",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDCUSTOMER = c.Int(),
                        IDHOTELBOOK = c.Int(),
                        IDFLIGHTBOOK = c.Int(),
                        IDPACKAGEBOOK = c.Int(),
                        CARDNAME = c.String(maxLength: 50),
                        CARDNUM = c.Int(),
                        EXPDATE = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BOOKINGHOTEL", t => t.IDHOTELBOOK)
                .ForeignKey("dbo.BOOKINGPACKAGE", t => t.IDPACKAGEBOOK)
                .ForeignKey("dbo.BOOKINGFLIGHT", t => t.IDFLIGHTBOOK)
                .ForeignKey("dbo.ACCOUNT", t => t.IDCUSTOMER)
                .Index(t => t.IDCUSTOMER)
                .Index(t => t.IDHOTELBOOK)
                .Index(t => t.IDFLIGHTBOOK)
                .Index(t => t.IDPACKAGEBOOK);
            
            CreateTable(
                "dbo.COMMENTANDRATING",
                c => new
                    {
                        COMMENT = c.String(nullable: false, maxLength: 400),
                        RATING = c.Double(nullable: false),
                        IDCUSTOMER = c.Int(),
                        IDHOTEL = c.Int(),
                    })
                .PrimaryKey(t => new { t.COMMENT, t.RATING })
                .ForeignKey("dbo.HOTEL", t => t.IDHOTEL)
                .ForeignKey("dbo.ACCOUNT", t => t.IDCUSTOMER)
                .Index(t => t.IDCUSTOMER)
                .Index(t => t.IDHOTEL);
            
            CreateTable(
                "dbo.MANAGING",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDADMIN = c.Int(),
                        IDHOTEL = c.Int(),
                        IDFLIGHT = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HOTEL", t => t.IDHOTEL)
                .ForeignKey("dbo.FLIGHT", t => t.IDFLIGHT)
                .ForeignKey("dbo.ACCOUNT", t => t.IDADMIN)
                .Index(t => t.IDADMIN)
                .Index(t => t.IDHOTEL)
                .Index(t => t.IDFLIGHT);
            
            CreateTable(
                "dbo.CUSTOMER",
                c => new
                    {
                        USERNAME = c.String(nullable: false, maxLength: 20),
                        ID = c.Int(),
                        NAME = c.String(maxLength: 40),
                        DATEOFBIRTH = c.DateTime(),
                        PICTURES = c.String(),
                        EMAIL = c.String(maxLength: 50),
                        PHONENUMBER = c.String(maxLength: 10, unicode: false),
                        ADDRESS = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.USERNAME)
                .ForeignKey("dbo.ACCOUNT", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PAYING", "IDCUSTOMER", "dbo.ACCOUNT");
            DropForeignKey("dbo.MANAGING", "IDADMIN", "dbo.ACCOUNT");
            DropForeignKey("dbo.CUSTOMER", "ID", "dbo.ACCOUNT");
            DropForeignKey("dbo.COMMENTANDRATING", "IDCUSTOMER", "dbo.ACCOUNT");
            DropForeignKey("dbo.BOOKINGPACKAGE", "IDCUSTOMER", "dbo.ACCOUNT");
            DropForeignKey("dbo.BOOKINGHOTEL", "IDCUSTOMER", "dbo.ACCOUNT");
            DropForeignKey("dbo.BOOKINGFLIGHT", "IDCUSTOMER", "dbo.ACCOUNT");
            DropForeignKey("dbo.APPROVE", "IDADMIN", "dbo.ACCOUNT");
            DropForeignKey("dbo.PAYING", "IDFLIGHTBOOK", "dbo.BOOKINGFLIGHT");
            DropForeignKey("dbo.MANAGING", "IDFLIGHT", "dbo.FLIGHT");
            DropForeignKey("dbo.BOOKINGPACKAGE", "IDFLIGHT", "dbo.FLIGHT");
            DropForeignKey("dbo.PAYING", "IDPACKAGEBOOK", "dbo.BOOKINGPACKAGE");
            DropForeignKey("dbo.MANAGING", "IDHOTEL", "dbo.HOTEL");
            DropForeignKey("dbo.COMMENTANDRATING", "IDHOTEL", "dbo.HOTEL");
            DropForeignKey("dbo.BOOKINGPACKAGE", "IDHOTEL", "dbo.HOTEL");
            DropForeignKey("dbo.BOOKINGHOTEL", "IDHOTEL", "dbo.HOTEL");
            DropForeignKey("dbo.PAYING", "IDHOTELBOOK", "dbo.BOOKINGHOTEL");
            DropForeignKey("dbo.APPROVE", "IDHOTELBOOK", "dbo.BOOKINGHOTEL");
            DropForeignKey("dbo.APPROVE", "IDPACKAGEBOOK", "dbo.BOOKINGPACKAGE");
            DropForeignKey("dbo.BOOKINGFLIGHT", "IDFLIGHT", "dbo.FLIGHT");
            DropForeignKey("dbo.APPROVE", "IDFLIGHTBOOK", "dbo.BOOKINGFLIGHT");
            DropForeignKey("dbo.ADMIN", "ID", "dbo.ACCOUNT");
            DropIndex("dbo.CUSTOMER", new[] { "ID" });
            DropIndex("dbo.MANAGING", new[] { "IDFLIGHT" });
            DropIndex("dbo.MANAGING", new[] { "IDHOTEL" });
            DropIndex("dbo.MANAGING", new[] { "IDADMIN" });
            DropIndex("dbo.COMMENTANDRATING", new[] { "IDHOTEL" });
            DropIndex("dbo.COMMENTANDRATING", new[] { "IDCUSTOMER" });
            DropIndex("dbo.PAYING", new[] { "IDPACKAGEBOOK" });
            DropIndex("dbo.PAYING", new[] { "IDFLIGHTBOOK" });
            DropIndex("dbo.PAYING", new[] { "IDHOTELBOOK" });
            DropIndex("dbo.PAYING", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGHOTEL", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGHOTEL", new[] { "IDHOTEL" });
            DropIndex("dbo.BOOKINGPACKAGE", new[] { "IDFLIGHT" });
            DropIndex("dbo.BOOKINGPACKAGE", new[] { "IDHOTEL" });
            DropIndex("dbo.BOOKINGPACKAGE", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGFLIGHT", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGFLIGHT", new[] { "IDFLIGHT" });
            DropIndex("dbo.APPROVE", new[] { "IDPACKAGEBOOK" });
            DropIndex("dbo.APPROVE", new[] { "IDFLIGHTBOOK" });
            DropIndex("dbo.APPROVE", new[] { "IDHOTELBOOK" });
            DropIndex("dbo.APPROVE", new[] { "IDADMIN" });
            DropIndex("dbo.ADMIN", new[] { "ID" });
            DropTable("dbo.CUSTOMER");
            DropTable("dbo.MANAGING");
            DropTable("dbo.COMMENTANDRATING");
            DropTable("dbo.PAYING");
            DropTable("dbo.BOOKINGHOTEL");
            DropTable("dbo.HOTEL");
            DropTable("dbo.BOOKINGPACKAGE");
            DropTable("dbo.FLIGHT");
            DropTable("dbo.BOOKINGFLIGHT");
            DropTable("dbo.APPROVE");
            DropTable("dbo.ADMIN");
            DropTable("dbo.ACCOUNT");
        }
    }
}
