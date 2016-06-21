namespace UCRMS_Version2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReshufflingInModelNames : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Classrooms", "DepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.Classrooms", "CourseId", c => c.Int(nullable: false));
            AddColumn("dbo.Classrooms", "ClassroomId", c => c.Int(nullable: false));
            AddColumn("dbo.Classrooms", "Day", c => c.String(nullable: false));
            AddColumn("dbo.Classrooms", "ClassStartFrom", c => c.String(nullable: false));
            AddColumn("dbo.Classrooms", "ClassEndAt", c => c.String(nullable: false));
            DropColumn("dbo.Classrooms", "Name");
            DropTable("dbo.AllocateClassrooms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AllocateClassrooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        ClassroomId = c.Int(nullable: false),
                        Day = c.String(nullable: false),
                        ClassStartFrom = c.String(nullable: false),
                        ClassEndAt = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Classrooms", "Name", c => c.String());
            DropColumn("dbo.Classrooms", "ClassEndAt");
            DropColumn("dbo.Classrooms", "ClassStartFrom");
            DropColumn("dbo.Classrooms", "Day");
            DropColumn("dbo.Classrooms", "ClassroomId");
            DropColumn("dbo.Classrooms", "CourseId");
            DropColumn("dbo.Classrooms", "DepartmentId");
            DropTable("dbo.Rooms");
        }
    }
}
