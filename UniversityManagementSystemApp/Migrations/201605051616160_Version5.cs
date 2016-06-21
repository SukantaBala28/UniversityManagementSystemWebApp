namespace UCRMS_Version2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version5 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CourseStatistics");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Semester = c.String(),
                        AssignedTo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
