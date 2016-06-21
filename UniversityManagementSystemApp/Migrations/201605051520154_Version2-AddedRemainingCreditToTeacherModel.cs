namespace UCRMS_Version2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version2AddedRemainingCreditToTeacherModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "RemainingCredit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.CourseViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        CreditToBeTaken = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemainingCredit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                        CourseCredit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Teachers", "RemainingCredit");
        }
    }
}
