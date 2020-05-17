namespace DemoASP2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.customers", "phon", c => c.String(maxLength: 10));
            AlterColumn("dbo.shippers", "phon", c => c.String(maxLength: 10));
            AlterColumn("dbo.suppliers", "phon", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.suppliers", "phon", c => c.String(maxLength: 9));
            AlterColumn("dbo.shippers", "phon", c => c.String(maxLength: 9));
            AlterColumn("dbo.customers", "phon", c => c.String(maxLength: 9));
        }
    }
}
