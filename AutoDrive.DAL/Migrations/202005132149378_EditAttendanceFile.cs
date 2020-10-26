namespace AutoDrive.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditAttendanceFile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AttendanceFile", "UploadDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AttendanceFile", "UploadDate", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
