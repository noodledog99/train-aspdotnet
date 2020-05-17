namespace DemoASP2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.customers", "company_name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.customers", "address", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.customers", "phon", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.customers", "phon", c => c.String(maxLength: 10));
            AlterColumn("dbo.customers", "address", c => c.String(maxLength: 60));
            AlterColumn("dbo.customers", "company_name", c => c.String(maxLength: 20));
        }
    }
}
