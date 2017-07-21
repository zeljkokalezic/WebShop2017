using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class NarudzbeniceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Narudzbenice
        public ActionResult Index()
        {
            return View(db.Narudzbenice.ToList());
        }

        // GET: Narudzbenice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzbenica narudzbenica = db.Narudzbenice.Find(id);
            if (narudzbenica == null)
            {
                return HttpNotFound();
            }
            return View(narudzbenica);
        }

        // GET: Narudzbenice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Narudzbenice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status")] Narudzbenica narudzbenica)
        {
            if (ModelState.IsValid)
            {
                db.Narudzbenice.Add(narudzbenica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(narudzbenica);
        }

        // GET: Narudzbenice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzbenica narudzbenica = db.Narudzbenice.Find(id);
            if (narudzbenica == null)
            {
                return HttpNotFound();
            }
            return View(narudzbenica);
        }

        // POST: Narudzbenice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status")] Narudzbenica narudzbenica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(narudzbenica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(narudzbenica);
        }

        // GET: Narudzbenice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narudzbenica narudzbenica = db.Narudzbenice.Find(id);
            if (narudzbenica == null)
            {
                return HttpNotFound();
            }
            return View(narudzbenica);
        }

        // POST: Narudzbenice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Narudzbenica narudzbenica = db.Narudzbenice.Find(id);
            db.Narudzbenice.Remove(narudzbenica);
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
