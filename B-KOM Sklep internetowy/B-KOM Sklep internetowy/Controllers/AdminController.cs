using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        InternetShopContext db = new InternetShopContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrdersList(int? idZamowienia)
        {
            var orders = db.Orders.Where(c => c.OrderId.ToString().Contains(idZamowienia.ToString())).Include("OrderItems").OrderByDescending(c => c.OrderId).ToList();

            return View(orders);
        }

        public ActionResult OrderDetails(int id)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            var order = db.Orders.Where(c => c.OrderId == id).Include("OrderItems").SingleOrDefault();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeOrderStatus(Order order)
        {
            Order orderToChange = db.Orders.Find(order.OrderId);
            orderToChange.OrderStatus = order.OrderStatus;
            db.SaveChanges();

            return RedirectToAction("OrderDetails", new { id = order.OrderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeOrderDetails(Order order)
        {
            if(!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("OrderDetails", new { id = order.OrderId });
            }

            Order orderToChange = db.Orders.Find(order.OrderId);

            orderToChange.FirstName = order.FirstName;
            orderToChange.LastName = order.LastName;
            orderToChange.Address = order.Address;
            orderToChange.City = order.City;
            orderToChange.PostCode = order.PostCode;
            orderToChange.Email = order.Email;
            orderToChange.Phone = order.Phone;
            orderToChange.Comment = order.Comment;

            db.SaveChanges();

            return RedirectToAction("OrderDetails", new { id = order.OrderId });
        }
    }
}