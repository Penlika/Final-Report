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

        protected override void Seed(Final_Report.Models.Final context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var lstPackage = new List<PACKAGE>();
            lstPackage.Add(new PACKAGE
            {

                Anhbia = "data:image/jpeg;base64," + Utility.ConvertImageToBase64("C:\\Users\\ACER\\source\\repos\\WebApplication1\\SachOnline\\Images\\gtO1o2E.jpeg")
            });
            lstSach.ForEach(s => context.SACHes.AddOrUpdate(s));
            base.Seed(context);
        }
    }
}
