namespace AutoDrive.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeArchive : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeArchive",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ArchiveSettingHRsId = c.Int(nullable: false),
                        Number = c.String(),
                        Notes = c.String(),
                        Date = c.DateTime(),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ArchiveSettingHRs", t => t.ArchiveSettingHRsId, cascadeDelete: false)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.ArchiveSettingHRsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeArchive", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeArchive", "ArchiveSettingHRsId", "dbo.ArchiveSettingHRs");
            DropIndex("dbo.EmployeeArchive", new[] { "ArchiveSettingHRsId" });
            DropIndex("dbo.EmployeeArchive", new[] { "EmployeeId" });
            DropTable("dbo.EmployeeArchive");
        }
    }
}
