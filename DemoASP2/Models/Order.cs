using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace DemoASP2.Models
{
    [Table("orders")]
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Order Id")]
        [Key, Column("order_id", Order = 0)]
        public int OrderId { get; set; }

        [Display(Name = "Customer Id")]
        [Column("customer_id", Order = 1)]
        public string CustomerId { get; set; }
        public Customer customer { get; set; }

        [Display(Name = "Employee Id")]
        [Column("employee_id", Order = 2)]
        public int? EmployeeId { get; set; }
        public Employee employee { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Required Date")]
        [DataType(DataType.Date)]
        [Column("required_date")]
        public DateTime RequiredDate { get; set; }

        [Display(Name = "Shipped Date")]
        [DataType(DataType.Date)]
        [Column("shipped_date")]
        public DateTime ShippedDate { get; set; }

        [Display(Name = "Ship Via")]
        [Column("ship_via")]
        public int ShipVia { get; set; }

        [Display(Name = "Freight")]
        [Column("freight")]
        public int Freight { get; set; }

        [Display(Name = "Shipper Id")]
        [Column("shipper_id", Order = 3)]
        public int? ShipperId { get; set; }
        public Shipper shipper { get; set; }

        [Required]
        [Display(Name = "Ship Name")]
        [StringLength(20)]
        [Column("ship_name")]
        public string ShipName { get; set; }

        [Display(Name = "Ship Address")]
        [StringLength(100)]
        [Column("ship_address")]
        public string ShipAddress { get; set; }

        [Display(Name = "Ship City")]
        [StringLength(20)]
        [Column("ship_city")]
        public string ShipCity { get; set; }

        [Display(Name = "Ship Region")]
        [StringLength(15)]
        [Column("ship_region")]
        public string ShipRegion { get; set; }

        [Display(Name = "Ship Posttal Code")]
        [Column("ship_postal_code")]
        public int ShipPostalCode { get; set; }

        [Display(Name = "Ship Country")]
        [StringLength(20)]
        [Column("ship_country")]
        public string ShipCountry { get; set; }

        [Required]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
         = new HashSet<OrderDetail>();
        //anonymus function
        public decimal SubTotal => OrderDetails.Sum(x => x.Total);
        public decimal VatAmount => Math.Round(SubTotal * 0.07m, 2, MidpointRounding.AwayFromZero);
        public decimal NetTotal => SubTotal + VatAmount;

    }
}