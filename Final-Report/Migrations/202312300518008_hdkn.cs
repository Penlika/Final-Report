namespace Final_Report.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hdkn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ACCOUNT",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        USERNAME = c.String(nullable: false, maxLength: 50),
                        EMAIL = c.String(nullable: false, maxLength: 50),
                        PASSWORD = c.String(nullable: false, maxLength: 30),
                        ROLE = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ADMIN",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        PICTURES = c.String(),
                        USERNAME = c.String(maxLength: 20),
                        EMAIL = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ACCOUNT", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.BOOKINGFLIGHT",
                c => new
                    {
                        IDRECEIPT = c.Int(nullable: false, identity: true),
                        IDFLIGHT = c.Int(),
                        IDCUSTOMER = c.Int(),
                        BOOKINGDATE = c.DateTime(),
                        NUMOFPERSON = c.Int(nullable: false),
                        TOTALPRICE = c.Double(nullable: false),
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
                        COMPANY = c.String(maxLength: 40),
                        LOGO = c.String(),
                        DEPARTURE = c.DateTime(nullable: false),
                        ARRIVAL = c.DateTime(nullable: false),
                        FROM = c.String(nullable: false, maxLength: 100),
                        TO = c.String(nullable: false, maxLength: 100),
                        PRICE_PER_PERSON = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PACKAGE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DESTINATION = c.String(nullable: false, maxLength: 100),
                        IDFLIGHT = c.Int(),
                        IDHOTEL = c.Int(),
                        PICTURES = c.String(),
                        INFORMATION = c.String(maxLength: 250),
                        PROLONG = c.Int(),
                        PRICE_PER_PERSON = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HOTEL", t => t.IDHOTEL)
                .ForeignKey("dbo.FLIGHT", t => t.IDFLIGHT)
                .Index(t => t.IDFLIGHT)
                .Index(t => t.IDHOTEL);
            
            CreateTable(
                "dbo.BOOKINGPACKAGE",
                c => new
                    {
                        IDRECEIPT = c.Int(nullable: false, identity: true),
                        IDCUSTOMER = c.Int(),
                        IDPACKAGE = c.Int(),
                        BOOKINGDATE = c.DateTime(),
                        NUMOFPERSON = c.Int(nullable: false),
                        TOTALPRICE = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IDRECEIPT)
                .ForeignKey("dbo.PACKAGE", t => t.IDPACKAGE)
                .ForeignKey("dbo.ACCOUNT", t => t.IDCUSTOMER)
                .Index(t => t.IDCUSTOMER)
                .Index(t => t.IDPACKAGE);
            
            CreateTable(
                "dbo.HOTEL",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PICTURE1 = c.String(),
                        PICTURE2 = c.String(),
                        PICTURE3 = c.String(),
                        PICTURE4 = c.String(),
                        PICTURE5 = c.String(),
                        PICTURE6 = c.String(),
                        NAME = c.String(nullable: false, maxLength: 50),
                        ADDRESS = c.String(nullable: false, maxLength: 300),
                        INFORMATION = c.String(),
                        PRICE_PER_PERSON = c.Double(nullable: false),
                        ROOM_AVAILABLE = c.Int(nullable: false),
                        RATING = c.Int(),
                        MODIFIEDDAY = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BOOKINGHOTEL",
                c => new
                    {
                        IDRECEIPT = c.Int(nullable: false, identity: true),
                        IDHOTEL = c.Int(),
                        IDCUSTOMER = c.Int(),
                        CHECKINDATE = c.DateTime(),
                        CHECKOUTDATE = c.DateTime(),
                        NUMOFPERSON = c.Int(nullable: false),
                        ROOM = c.Int(),
                        TOTALPRICE = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IDRECEIPT)
                .ForeignKey("dbo.HOTEL", t => t.IDHOTEL)
                .ForeignKey("dbo.ACCOUNT", t => t.IDCUSTOMER)
                .Index(t => t.IDHOTEL)
                .Index(t => t.IDCUSTOMER);
            
            CreateTable(
                "dbo.COMMENTANDRATING",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDCUSTOMER = c.Int(),
                        IDHOTEL = c.Int(),
                        COMMENT = c.String(nullable: false, maxLength: 400),
                        STAR_RATING = c.Int(nullable: false),
                        DATECREATE = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HOTEL", t => t.IDHOTEL)
                .ForeignKey("dbo.ACCOUNT", t => t.IDCUSTOMER)
                .Index(t => t.IDCUSTOMER)
                .Index(t => t.IDHOTEL);
            
            CreateTable(
                "dbo.CUSTOMER",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        EMAIL = c.String(nullable: false, maxLength: 50),
                        NAME = c.String(maxLength: 40),
                        DATEOFBIRTH = c.DateTime(storeType: "date"),
                        USERNAME = c.String(maxLength: 20),
                        PICTURES = c.String(),
                        PHONENUMBER = c.String(maxLength: 10),
                        ADDRESS = c.String(maxLength: 100),
                        CARDNAME = c.String(maxLength: 50),
                        CARDNUM = c.String(maxLength: 16),
                        EXPDATE = c.DateTime(),
                        SECURNUM = c.String(maxLength: 3),
                    })
                .PrimaryKey(t => new { t.ID, t.EMAIL })
                .ForeignKey("dbo.ACCOUNT", t => t.ID)
                .Index(t => t.ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MANAGER", "ID", "dbo.ACCOUNT");
            DropForeignKey("dbo.CUSTOMER", "ID", "dbo.ACCOUNT");
            DropForeignKey("dbo.COMMENTANDRATING", "IDCUSTOMER", "dbo.ACCOUNT");
            DropForeignKey("dbo.BOOKINGPACKAGE", "IDCUSTOMER", "dbo.ACCOUNT");
            DropForeignKey("dbo.BOOKINGHOTEL", "IDCUSTOMER", "dbo.ACCOUNT");
            DropForeignKey("dbo.BOOKINGFLIGHT", "IDCUSTOMER", "dbo.ACCOUNT");
            DropForeignKey("dbo.PACKAGE", "IDFLIGHT", "dbo.FLIGHT");
            DropForeignKey("dbo.PACKAGE", "IDHOTEL", "dbo.HOTEL");
            DropForeignKey("dbo.COMMENTANDRATING", "IDHOTEL", "dbo.HOTEL");
            DropForeignKey("dbo.BOOKINGHOTEL", "IDHOTEL", "dbo.HOTEL");
            DropForeignKey("dbo.BOOKINGPACKAGE", "IDPACKAGE", "dbo.PACKAGE");
            DropForeignKey("dbo.BOOKINGFLIGHT", "IDFLIGHT", "dbo.FLIGHT");
            DropForeignKey("dbo.ADMIN", "ID", "dbo.ACCOUNT");
            DropIndex("dbo.MANAGER", new[] { "ID" });
            DropIndex("dbo.CUSTOMER", new[] { "ID" });
            DropIndex("dbo.COMMENTANDRATING", new[] { "IDHOTEL" });
            DropIndex("dbo.COMMENTANDRATING", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGHOTEL", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGHOTEL", new[] { "IDHOTEL" });
            DropIndex("dbo.BOOKINGPACKAGE", new[] { "IDPACKAGE" });
            DropIndex("dbo.BOOKINGPACKAGE", new[] { "IDCUSTOMER" });
            DropIndex("dbo.PACKAGE", new[] { "IDHOTEL" });
            DropIndex("dbo.PACKAGE", new[] { "IDFLIGHT" });
            DropIndex("dbo.BOOKINGFLIGHT", new[] { "IDCUSTOMER" });
            DropIndex("dbo.BOOKINGFLIGHT", new[] { "IDFLIGHT" });
            DropIndex("dbo.ADMIN", new[] { "ID" });
            DropTable("dbo.LOGOIMG");
            DropTable("dbo.MANAGER");
            DropTable("dbo.CUSTOMER");
            DropTable("dbo.COMMENTANDRATING");
            DropTable("dbo.BOOKINGHOTEL");
            DropTable("dbo.HOTEL");
            DropTable("dbo.BOOKINGPACKAGE");
            DropTable("dbo.PACKAGE");
            DropTable("dbo.FLIGHT");
            DropTable("dbo.BOOKINGFLIGHT");
            DropTable("dbo.ADMIN");
            DropTable("dbo.ACCOUNT");
        }
    }
}
