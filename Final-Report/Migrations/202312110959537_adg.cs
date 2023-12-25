namespace Final_Report.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HOTEL", "MODIFIEDDAY", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HOTEL", "MODIFIEDDAY");
        }
    }
}
