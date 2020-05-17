namespace DemoASP2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        category_id = c.Int(nullable: false, identity: true),
                        category_name = c.String(nullable: false, maxLength: 20),
                        description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.category_id);
            
            CreateTable(
                "dbo.products",
                c => new
                    {
                        product_id = c.Int(nullable: false, identity: true),
                        supplier_id = c.Int(),
                        category_id = c.Int(),
                        product_name = c.String(nullable: false, maxLength: 65),
                        quantity_per_unit = c.Int(nullable: false),
                        unit_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        units_in_stock = c.Int(nullable: false),
                        units_on_order = c.Int(nullable: false),
                        reorder_level = c.Int(nullable: false),
                        discontinued = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.product_id)
                .ForeignKey("dbo.categories", t => t.category_id)
                .ForeignKey("dbo.suppliers", t => t.supplier_id)
                .Index(t => t.supplier_id)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.suppliers",
                c => new
                    {
                        supplier_id = c.Int(nullable: false, identity: true),
                        company_name = c.String(nullable: false, maxLength: 25),
                        contact_name = c.String(maxLength: 20),
                        contact_title = c.String(maxLength: 20),
                        address = c.String(nullable: false, maxLength: 100),
                        city = c.String(maxLength: 20),
                        region = c.String(maxLength: 15),
                        postal_code = c.Int(nullable: false),
                        country = c.String(maxLength: 20),
                        phon = c.String(maxLength: 9),
                        fax = c.String(maxLength: 10),
                        homepage = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.supplier_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.products", "supplier_id", "dbo.suppliers");
            DropForeignKey("dbo.products", "category_id", "dbo.categories");
            DropIndex("dbo.products", new[] { "category_id" });
            DropIndex("dbo.products", new[] { "supplier_id" });
            DropTable("dbo.suppliers");
            DropTable("dbo.products");
            DropTable("dbo.categories");
        }
    }
}
