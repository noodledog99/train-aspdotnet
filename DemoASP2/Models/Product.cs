using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoASP2.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id", Order = 0)]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(65, ErrorMessage = "product name at least 3 charactor", MinimumLength = 3)]
        [Column("product_name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Column("supplier_id", Order = 1)]
        [Display(Name = "Supplier Id")]
        public int? SupplierId { get; set; }
        public Supplier supplier { get; set; }

        [Column("category_id", Order = 2)]
        [Display(Name = "Category Id")]
        public int? CategoryId { get; set; }
        public Category category { get; set; }

        [Required]
        [Column("quantity_per_unit")]
        [Display(Name = "Quantity Per Unit")]
        public int QuantityPerUnit { get; set; }

        [Required]
        [Display(Name = "Unit Price")]
        [Column("unit_price")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Units In Stock")]
        [Column("units_in_stock")]
        public int UnitsInStock { get; set; }

        [Display(Name = "Units On Order")]
        [Column("units_on_order")]
        public int UnitsOnOrder { get; set; }

        [Display(Name = "Re Order Level")]
        [Column("reorder_level")]
        public int ReOrderLevel { get; set; }

        [Display(Name = "Discontinued")]
        [Column("discontinued")]
        public int Discontinued { get; set; }
    }
}