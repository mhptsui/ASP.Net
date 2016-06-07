using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers {
    public class StoreManagerController : Controller {
        private MusicStoreDB db = new MusicStoreDB();

        //
        // GET: /StoreManager/

        public ActionResult Index() {
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(albums.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ActionResult Details(int id = 0) {
            Album album = db.Albums.Find(id);
            if (album == null) {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create() {
            ViewBag.ArtistId = new SelectList(db.Artists.OrderBy(a => a.Name), "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(db.Genres.OrderBy(g => g.Name), "GenreId", "Name");
            return View();
        }

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Album album) {
            if (ModelState.IsValid) {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists.OrderBy(a => a.Name), "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres.OrderBy(g => g.Name), "GenreId", "Name", album.GenreId);
            return View(album);
        }

        //
        // GET: /StoreManager/Edit/5

        public ActionResult Edit(int id = 0) {
            Album album = db.Albums.Find(id);
            if (album == null) {
                return HttpNotFound();
            }
            ViewBag.ArtistId = db.Artists.OrderBy(a => a.Name).AsEnumerable().Select(a => new SelectListItem { Text = a.Name, Value = a.ArtistId.ToString(), Selected = (a.ArtistId == album.ArtistId) });
            ViewBag.GenreId = db.Genres.OrderBy(g => g.Name).ToList().Select(g => new SelectListItem { Text = g.Name, Value = g.GenreId.ToString(), Selected = (g.GenreId == album.GenreId) });
            return View(album);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album) {
            if (ModelState.IsValid) {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists.OrderBy(a => a.Name), "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres.OrderBy(g => g.Name), "GenreId", "Name", album.GenreId);
            return View(album);
        }

        //
        // GET: /StoreManager/Delete/5

        public ActionResult Delete(int id = 0) {
            Album album = db.Albums.Find(id);
            if (album == null) {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}