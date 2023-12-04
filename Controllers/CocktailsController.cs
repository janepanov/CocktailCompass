using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CocktailCompass.Models;

namespace CocktailCompass.Controllers
{
    [Authorize(Roles="Admin,User")]
    public class CocktailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cocktails
        [Authorize(Roles = "Admin,User")]
        public ActionResult Index()
        {
            return View(db.Cocktails.ToList());
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult RandomCocktail()
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        // GET: Cocktails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cocktail cocktail = db.Cocktails.Find(id);
            if (cocktail == null)
            {
                return HttpNotFound();
            }
            return View(cocktail);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cocktails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cocktails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "CocktailId,Name,Instructions,ImageURL")] Cocktail cocktail)
        {
            if (ModelState.IsValid)
            {
                db.Cocktails.Add(cocktail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cocktail);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cocktails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cocktail cocktail = db.Cocktails.Find(id);
            if (cocktail == null)
            {
                return HttpNotFound();
            }
            return View(cocktail);
        }

        // POST: Cocktails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "CocktailId,Name,Instructions,ImageURL")] Cocktail cocktail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cocktail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cocktail);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Cocktail cocktail = db.Cocktails.Find(id);
            db.Cocktails.Remove(cocktail);
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
