namespace UCRMS_Version2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version3 : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CourseViewModels");
        }
    }
}
