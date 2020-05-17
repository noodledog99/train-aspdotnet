namespace DemoASP2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.order_details",
                c => new
                    {
                        order_id = c.Int(nullable: false),
                        product_id = c.Int(nullable: false),
                        unit_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quantity = c.Int(nullable: false),
                        discount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.order_id, t.product_id })
                .ForeignKey("dbo.orders", t => t.order_id, cascadeDelete: true)
                .ForeignKey("dbo.products", t => t.product_id, cascadeDelete: true)
                .Index(t => t.order_id)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        order_id = c.Int(nullable: false, identity: true),
                        customer_id = c.String(maxLength: 128),
                        employee_id = c.Int(),
                        shipper_id = c.Int(),
                        order_date = c.DateTime(nullable: false),
                        required_date = c.DateTime(nullable: false),
                        shipped_date = c.DateTime(nullable: false),
                        ship_via = c.Int(nullable: false),
                        freight = c.Int(nullable: false),
                        ship_name = c.String(nullable: false, maxLength: 20),
                        ship_address = c.String(maxLength: 100),
                        ship_city = c.String(maxLength: 20),
                        ship_region = c.String(maxLength: 15),
                        ship_postal_code = c.Int(nullable: false),
                        ship_country = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.customers", t => t.customer_id)
                .ForeignKey("dbo.employees", t => t.employee_id)
                .ForeignKey("dbo.shippers", t => t.shipper_id)
                .Index(t => t.customer_id)
                .Index(t => t.employee_id)
                .Index(t => t.shipper_id);
            
            CreateTable(
                "dbo.shippers",
                c => new
                    {
                        shipper_id = c.Int(nullable: false, identity: true),
                        company_name = c.String(nullable: false, maxLength: 25),
                        phon = c.String(maxLength: 9),
                    })
                .PrimaryKey(t => t.shipper_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.order_details", "product_id", "dbo.products");
            DropForeignKey("dbo.order_details", "order_id", "dbo.orders");
            DropForeignKey("dbo.orders", "shipper_id", "dbo.shippers");
            DropForeignKey("dbo.orders", "employee_id", "dbo.employees");
            DropForeignKey("dbo.orders", "customer_id", "dbo.customers");
            DropIndex("dbo.orders", new[] { "shipper_id" });
            DropIndex("dbo.orders", new[] { "employee_id" });
            DropIndex("dbo.orders", new[] { "customer_id" });
            DropIndex("dbo.order_details", new[] { "product_id" });
            DropIndex("dbo.order_details", new[] { "order_id" });
            DropTable("dbo.shippers");
            DropTable("dbo.orders");
            DropTable("dbo.order_details");
        }
    }
}
