namespace AutoDrive.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Honesty : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Honesties", newName: "Honesty");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Honesty", newName: "Honesties");
        }
    }
}
