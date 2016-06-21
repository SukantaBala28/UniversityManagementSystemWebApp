namespace UCRMS_Version2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version6MadeRemainigCreditFromTeacherNotMapped : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Teachers", "RemainingCredit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "RemainingCredit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
