using UCRMS_Version2.Models;

namespace UCRMS_Version2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UCRMS_Version2.Models.UniversityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UCRMS_Version2.Models.UniversityDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Semesters.AddOrUpdate(
            //      new Semester {Name = "First"},
            //      new Semester { Name = "Second" },
            //      new Semester { Name = "Third" },
            //      new Semester { Name = "Fourth" },
            //      new Semester { Name = "Fifth" },
            //      new Semester { Name = "Sixth" },
            //      new Semester { Name = "Seventh" },
            //      new Semester { Name = "Eighth" }
            //    );

            //context.Designations.AddOrUpdate(
            //    new Designation { Name = "Lecturer"},
            //    new Designation { Name = "Assistant Professor"},
            //    new Designation { Name = "Associate Professor"},
            //    new Designation { Name = "Professor" }
            //    );

            //context.Rooms.AddOrUpdate(
            //    new Room { Name = "A-101" },
            //    new Room { Name = "A-102" },
            //    new Room { Name = "B-101" },
            //    new Room { Name = "B-102" },
            //    new Room { Name = "A-201" },
            //    new Room { Name = "A-202" },
            //    new Room { Name = "B-201" },
            //    new Room { Name = "B-202" }
            //    );

            //context.Grades.AddOrUpdate(
            //    new Grade { Name="A+"}, new Grade { Name="A"}, new Grade { Name = "A-"},
            //    new Grade { Name="B+"}, new Grade { Name="B"}, new Grade { Name = "B-"},
            //    new Grade { Name="C+"}, new Grade { Name="C"}, new Grade { Name = "C-"},
            //    new Grade { Name="D+"}, new Grade { Name="D"}, new Grade { Name = "D-"},
            //    new Grade { Name="F"}
            //    );
        }
    }
}
