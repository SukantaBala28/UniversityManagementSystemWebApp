namespace UCRMS_Version2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationshipsBetweenClassroomAndOthers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classrooms", "Room_Id", c => c.Int());
            CreateIndex("dbo.Classrooms", "DepartmentId");
            CreateIndex("dbo.Classrooms", "CourseId");
            CreateIndex("dbo.Classrooms", "Room_Id");
            AddForeignKey("dbo.Classrooms", "CourseId", "dbo.Courses", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Classrooms", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Classrooms", "Room_Id", "dbo.Rooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classrooms", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Classrooms", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Classrooms", "CourseId", "dbo.Courses");
            DropIndex("dbo.Classrooms", new[] { "Room_Id" });
            DropIndex("dbo.Classrooms", new[] { "CourseId" });
            DropIndex("dbo.Classrooms", new[] { "DepartmentId" });
            DropColumn("dbo.Classrooms", "Room_Id");
        }
    }
}
