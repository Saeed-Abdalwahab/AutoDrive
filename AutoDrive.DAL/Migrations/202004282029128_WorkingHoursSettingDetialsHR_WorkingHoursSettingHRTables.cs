namespace AutoDrive.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkingHoursSettingDetialsHR_WorkingHoursSettingHRTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkingHoursSettingDetialsHR",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WorkingHoursSettingHRId = c.Int(nullable: false),
                        FromTime = c.Time(nullable: false, precision: 7),
                        ToTime = c.Time(nullable: false, precision: 7),
                        DayName = c.String(nullable: false),
                        DisplayArDayName = c.String(nullable: false),
                        DisplayEnDayName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.WorkingHoursSettingHR", t => t.WorkingHoursSettingHRId, cascadeDelete: true)
                .Index(t => t.WorkingHoursSettingHRId);
            
            CreateTable(
                "dbo.WorkingHoursSettingHR",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ArName = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                        FromDate = c.DateTime(storeType: "date"),
                        ToDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkingHoursSettingDetialsHR", "WorkingHoursSettingHRId", "dbo.WorkingHoursSettingHR");
            DropIndex("dbo.WorkingHoursSettingDetialsHR", new[] { "WorkingHoursSettingHRId" });
            DropTable("dbo.WorkingHoursSettingHR");
            DropTable("dbo.WorkingHoursSettingDetialsHR");
        }
    }
}
