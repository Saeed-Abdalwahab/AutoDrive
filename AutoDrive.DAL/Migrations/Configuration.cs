namespace AutoDrive.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutoDrive.DAL.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            //automatic migration 
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AutoDrive.DAL.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
