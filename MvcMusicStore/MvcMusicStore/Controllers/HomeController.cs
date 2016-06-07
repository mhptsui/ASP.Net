using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers {
    public class HomeController : Controller {
        private MusicStoreDB db = new MusicStoreDB();
        
        public ActionResult Index() {
            var albums = db.Albums.Include(a => a.Artist);
            return View(albums.ToList());
        }

        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string q) {
            var albums = db.Albums.Include("Artist").Where(a => a.Title.Contains(q));
            return View(albums);
        }

        public ActionResult Buy(int id) {
            var userName = User.Identity.Name;
            if (userName == null || userName == "") {
                userName = "Guest";
            }

            var album = db.Albums.Find(id);
            var cart = db.Carts.SingleOrDefault(c => c.UserName == userName);

            if (cart == null) {
                cart = new Cart { UserName = userName, 
                                  CartItems = new List<OrderDetail>() };
                db.Carts.Add(cart);
            } else {
                cart.CartItems = db.OrderDetails.Include(od => od.Album).Where(od => od.CartId == cart.CartId).ToList();
            }

            if (album != null) {
                var cartItem = cart.CartItems.Find(item => (item.AlbumId == album.AlbumId));

                if (cartItem == null) {
                    cartItem = new OrderDetail {
                        AlbumId = album.AlbumId,
                        Album = album,
                        Quantity = 1
                    };
                    cart.CartItems.Add(cartItem);
                    db.OrderDetails.Add(cartItem);
                } else {
                    cartItem.Quantity++;
                    db.Entry(cartItem).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            return PartialView("Cart", cart.CartItems);
        }
        
        [ChildActionOnly]
        public ActionResult Cart() {
            var userName = User.Identity.Name;
            if (userName == null || userName == "") {
                userName = "Guest";
            }
            var cart = db.Carts.SingleOrDefault(c => c.UserName == userName);
            if (cart == null) {
                cart = new Cart { UserName = User.Identity.Name, 
                                  CartItems = new List<OrderDetail>() };
                db.Carts.Add(cart);
                db.SaveChanges();
            } else {
                cart.CartItems = db.OrderDetails.Where(od => od.CartId == cart.CartId).Include(od => od.Album).ToList();
            }
            return PartialView(cart.CartItems);
        }

        [Authorize]
        public ActionResult Checkout() {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(Order order) {
            if (ModelState.IsValid) {
                order.OrderDate = DateTime.Now;
                order.Username = "Mike Tsui";
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }
    }
}
