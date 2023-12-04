using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CocktailCompass.Models;
using Microsoft.AspNet.Identity;

namespace CocktailCompass.Controllers
{
    [Authorize]
    public class CocktailFavoritesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CocktailFavorites
        public ActionResult Index()
        {
            var cocktailFavorites = db.CocktailFavorites.Include(c => c.Cocktail).Include(c => c.User);
            return View(cocktailFavorites.ToList());
        }

        [HttpPost]
        public JsonResult AddToFavorites(int cocktailId)
        {
            var userId = User.Identity.GetUserId();

            bool isInFavorites = db.CocktailFavorites.Any(cf => cf.UserId == userId && cf.CocktailId == cocktailId);

            if (!isInFavorites)
            {
                var newFavorite = new CocktailFavorite
                {
                    UserId = userId,
                    CocktailId = cocktailId
                };

                db.CocktailFavorites.Add(newFavorite);
                db.SaveChanges();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult RemoveFromFavorites(int cocktailId)
        {
            var userId = User.Identity.GetUserId();

            var favoriteToRemove = db.CocktailFavorites.FirstOrDefault(cf => cf.UserId == userId && cf.CocktailId == cocktailId);

            if (favoriteToRemove != null)
            {
                db.CocktailFavorites.Remove(favoriteToRemove);
                db.SaveChanges();
            }

            return RedirectToAction("MyFavorites");
        }


        public ActionResult MyFavorites() {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            if(user != null)
            {
                var favoriteCocktails = user.Favorites.Select(f => f.Cocktail).ToList();
                return View(favoriteCocktails);
            }

            return View(new List<Cocktail>());
        }

        public JsonResult IsInFavorites(int cocktailId)
        {
            var userId = User.Identity.GetUserId();
            bool isInFavorites = db.CocktailFavorites.Any(cf => cf.UserId == userId && cf.CocktailId == cocktailId);

            return Json(new { isInFavorites }, JsonRequestBehavior.AllowGet);
        }

        // GET: CocktailFavorites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CocktailFavorite cocktailFavorite = db.CocktailFavorites.Find(id);
            if (cocktailFavorite == null)
            {
                return HttpNotFound();
            }
            return View(cocktailFavorite);
        }

        // GET: CocktailFavorites/Create
        public ActionResult Create()
        {
            ViewBag.CocktailId = new SelectList(db.Cocktails, "CocktailId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: CocktailFavorites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CocktailFavoriteId,UserId,CocktailId")] CocktailFavorite cocktailFavorite)
        {
            if (ModelState.IsValid)
            {
                db.CocktailFavorites.Add(cocktailFavorite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CocktailId = new SelectList(db.Cocktails, "CocktailId", "Name", cocktailFavorite.CocktailId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", cocktailFavorite.UserId);
            return View(cocktailFavorite);
        }

        // GET: CocktailFavorites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CocktailFavorite cocktailFavorite = db.CocktailFavorites.Find(id);
            if (cocktailFavorite == null)
            {
                return HttpNotFound();
            }
            ViewBag.CocktailId = new SelectList(db.Cocktails, "CocktailId", "Name", cocktailFavorite.CocktailId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", cocktailFavorite.UserId);
            return View(cocktailFavorite);
        }

        // POST: CocktailFavorites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CocktailFavoriteId,UserId,CocktailId")] CocktailFavorite cocktailFavorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cocktailFavorite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CocktailId = new SelectList(db.Cocktails, "CocktailId", "Name", cocktailFavorite.CocktailId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", cocktailFavorite.UserId);
            return View(cocktailFavorite);
        }

        // GET: CocktailFavorites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CocktailFavorite cocktailFavorite = db.CocktailFavorites.Find(id);
            if (cocktailFavorite == null)
            {
                return HttpNotFound();
            }
            return View(cocktailFavorite);
        }

        // POST: CocktailFavorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CocktailFavorite cocktailFavorite = db.CocktailFavorites.Find(id);
            db.CocktailFavorites.Remove(cocktailFavorite);
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
