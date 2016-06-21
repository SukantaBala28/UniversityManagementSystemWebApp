namespace UCRMS_Version2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAllocateClassroomModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AllocateClassrooms", "ClassStartFrom", c => c.String(nullable: false));
            AlterColumn("dbo.AllocateClassrooms", "ClassEndAt", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AllocateClassrooms", "ClassEndAt", c => c.String());
            AlterColumn("dbo.AllocateClassrooms", "ClassStartFrom", c => c.String());
        }
    }
}
