namespace DemoASP2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.products", "category_id", "dbo.categories");
            DropForeignKey("dbo.products", "supplier_id", "dbo.suppliers");
            DropIndex("dbo.products", new[] { "supplier_id" });
            DropIndex("dbo.products", new[] { "category_id" });
            DropTable("dbo.categories");
            DropTable("dbo.products");
            DropTable("dbo.suppliers");
        }
        
        public override void Down()
        {
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
            
            CreateTable(
                "dbo.products",
                c => new
                    {
                        product_id = c.Int(nullable: false, identity: true),
                        supplier_id = c.Int(nullable: false),
                        category_id = c.Int(nullable: false),
                        product_name = c.String(nullable: false, maxLength: 65),
                        quantity_per_unit = c.Int(nullable: false),
                        unit_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        units_in_stock = c.Int(nullable: false),
                        units_on_order = c.Int(nullable: false),
                        reorder_level = c.Int(nullable: false),
                        discontinued = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.product_id, t.supplier_id, t.category_id });
            
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        category_id = c.Int(nullable: false, identity: true),
                        category_name = c.String(nullable: false, maxLength: 20),
                        description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.category_id);
            
            CreateIndex("dbo.products", "category_id");
            CreateIndex("dbo.products", "supplier_id");
            AddForeignKey("dbo.products", "supplier_id", "dbo.suppliers", "supplier_id", cascadeDelete: true);
            AddForeignKey("dbo.products", "category_id", "dbo.categories", "category_id", cascadeDelete: true);
        }
    }
}
