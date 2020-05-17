using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoASP2.Data;
using DemoASP2.Models;

namespace DemoASP2.Controllers
{
    public class OrdersController : Controller
    {
        private AppDb db = new AppDb();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.customer).Include(o => o.employee).Include(o => o.shipper).Include(o => o.OrderDetails);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(o => o.customer).Include(o => o.employee).Include(o => o.shipper).Include(o => o.OrderDetails.Select(c => c.product)).FirstOrDefault(it => it.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Products = new SelectList(db.Products, "ProductId", "ProductName", "-- Please select --");
            return View(order);
        }

        // GET: 
        public ActionResult ViewOrderDetail(int? id)
        {
            var orderDetail = db.Orders.Include(o => o.OrderDetails.Select(it => it.product)).FirstOrDefault(it => it.OrderId == id);
            if (orderDetail == null) return HttpNotFound();
            return PartialView(orderDetail);
        }

        [HttpPost]
        public ActionResult AddOrderDetail(int? id, OrderDetail orderDetail)
        {
            var order = db.Orders
                .Include(it => it.OrderDetails
                .Select(p => p.product))
                .FirstOrDefault(it => it.OrderId == id);
            var product = db.Products.Find(orderDetail.ProductId);
            var productIsExist = order.OrderDetails
                .Any(it => it.product.ProductId == orderDetail.ProductId);

            var od = order.OrderDetails.FirstOrDefault(it => it.ProductId == orderDetail.ProductId);

            if (productIsExist)
            {
                od.Quantity += orderDetail.Quantity;
                od.UnitPrice = product.UnitPrice;
            }
            else
            {
                od = new OrderDetail();
                od.ProductId = orderDetail.ProductId;
                od.Quantity = orderDetail.Quantity;
                od.UnitPrice = product.UnitPrice;
            }

            order.OrderDetails.Add(od);
            db.SaveChanges();

            var res = new
            {
                success = "success"
            };

            return Json(res);
        }

        // GET: Orders/Create
        public ActionResult Create(string id)
        {
            ViewBag.CustomerId = db.Customers.Find(id);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", "-- Select Employee --");
            ViewBag.ShipperId = new SelectList(db.Shippers, "ShipperId", "CompanyName", "-- Select Employee --");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var xx = DateTime.Parse(order.ShippedDate.ToString()).ToString("hh:mm tt");
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CompanyName", order.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", order.EmployeeId);
            ViewBag.ShipperId = new SelectList(db.Shippers, "ShipperId", "CompanyName", order.ShipperId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CompanyName", order.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", order.EmployeeId);
            ViewBag.ShipperId = new SelectList(db.Shippers, "ShipperId", "CompanyName", order.ShipperId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,EmployeeId,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipperId,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CompanyName", order.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", order.EmployeeId);
            ViewBag.ShipperId = new SelectList(db.Shippers, "ShipperId", "CompanyName", order.ShipperId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
