namespace AutoDrive.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttendanceFile_editEmployeeAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttendanceFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        UploadDate = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.EmployeeAttendance", "AttendanceFileId", c => c.Int());
            CreateIndex("dbo.EmployeeAttendance", "AttendanceFileId");
            AddForeignKey("dbo.EmployeeAttendance", "AttendanceFileId", "dbo.AttendanceFile", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeAttendance", "AttendanceFileId", "dbo.AttendanceFile");
            DropIndex("dbo.EmployeeAttendance", new[] { "AttendanceFileId" });
            DropColumn("dbo.EmployeeAttendance", "AttendanceFileId");
            DropTable("dbo.AttendanceFile");
        }
    }
}
