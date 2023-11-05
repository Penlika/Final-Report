namespace Final_Report.Migrations
{
    using Final_Report.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Final_Report.Models.Final>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //protected override void Seed(Final_Report.Models.Final context)
        //{
        //    //  This method will be called after migrating to the latest version.

        //    //  You can use the DbSet<T>.AddOrUpdate() helper extension method
        //    //  to avoid creating duplicate seed data.
        //    var lstFlight = new List<FLIGHT>();
        //    lstFlight.Add(new FLIGHT
        //    {
        //        COMPANY="",
        //        DEPARTURE="",
        //        ARRIVAL="",
        //        FROM="",
        //        TO="",
        //        PRICE_PER_PERSON=,
        //    });
        //    lstFlight.ForEach(s => context.PACKAGES.AddOrUpdate(s));
        //    base.Seed(context);


        //    var lstHotel = new List<HOTEL>();
        //    lstHotel.Add(new HOTEL
        //    {
        //        NAME="",
        //        ADDRESS="",
        //        PICTURES = "data:image/jpeg;base64," + Utility.ConvertImageToBase64("C:\\Users\\ACER\\source\\repos\\WebApplication1\\SachOnline\\Images\\gtO1o2E.jpeg"),
        //        INFORMATION="",
        //        PRICE_PER_PERSON=,
        //        ROOM_AVAILABLE=,
        //    });
        //    lstHotel.ForEach(s => context.PACKAGES.AddOrUpdate(s));
        //    base.Seed(context);


        //    var lstPackage = new List<PACKAGE>();
        //    lstPackage.Add(new PACKAGE
        //    {
        //        PACKAGENAME="",
        //        IDFLIGHT=,
        //        IDHOTEL=,
        //        DESTINATION="",
        //        INFORMATION="",
        //        PACKAGEPRICE_PER_PERSON=,
        //        PICTURES = "data:image/jpeg;base64," + Utility.ConvertImageToBase64("C:\\Users\\ACER\\source\\repos\\WebApplication1\\SachOnline\\Images\\gtO1o2E.jpeg")
        //    });
        //    lstPackage.ForEach(s => context.PACKAGES.AddOrUpdate(s));
        //    base.Seed(context);
        
    }
}
