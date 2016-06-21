namespace UCRMS_Version2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 50),
                        ContactNo = c.String(nullable: false, maxLength: 15),
                        Date = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 200),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.DepartmentId);
            
            AddColumn("dbo.Courses", "Student_Id", c => c.Int());
            CreateIndex("dbo.Courses", "Student_Id");
            AddForeignKey("dbo.Courses", "Student_Id", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Courses", "Student_Id", "dbo.Students");
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            DropIndex("dbo.Students", new[] { "Email" });
            DropIndex("dbo.Courses", new[] { "Student_Id" });
            DropColumn("dbo.Courses", "Student_Id");
            DropTable("dbo.Students");
        }
    }
}
