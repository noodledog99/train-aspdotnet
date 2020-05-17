namespace DemoASP2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        customer_id = c.String(nullable: false, maxLength: 128),
                        company_name = c.String(maxLength: 20),
                        contact_name = c.String(maxLength: 20),
                        contact_title = c.String(maxLength: 35),
                        address = c.String(maxLength: 60),
                        city = c.String(maxLength: 20),
                        region = c.String(maxLength: 15),
                        postal_code = c.String(maxLength: 5),
                        country = c.String(maxLength: 20),
                        phon = c.String(maxLength: 9),
                        fax = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.customer_id);
            
            CreateTable(
                "dbo.employees",
                c => new
                    {
                        employee_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(maxLength: 15),
                        last_name = c.String(maxLength: 15),
                        title = c.String(maxLength: 15),
                        title_of_courtesy = c.String(maxLength: 20),
                        birth_date = c.DateTime(nullable: false),
                        hire_date = c.DateTime(nullable: false),
                        address = c.String(maxLength: 60),
                        city = c.String(maxLength: 20),
                        region = c.String(maxLength: 15),
                        postal_code = c.Int(nullable: false),
                        country = c.String(maxLength: 20),
                        home_phon = c.Int(nullable: false),
                        extension = c.String(maxLength: 20),
                        note = c.String(maxLength: 20),
                        reports_to = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.employee_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.employees");
            DropTable("dbo.customers");
        }
    }
}
