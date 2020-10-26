namespace AutoDrive.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeFamily",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ArName = c.String(),
                        EnName = c.String(),
                        Gender = c.Int(nullable: false),
                        Relation = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeFamily", "EmployeeId", "dbo.Employee");
            DropIndex("dbo.EmployeeFamily", new[] { "EmployeeId" });
            DropTable("dbo.EmployeeFamily");
        }
    }
}
