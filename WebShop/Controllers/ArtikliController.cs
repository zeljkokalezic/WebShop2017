using Microsoft.AspNet.Identity;
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
    public class ArtikliController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Artikli
        public ActionResult Index()
        {
            var korisnikId = User.Identity.GetUserId();
            var korisnik = db.Users.Find(korisnikId);
            var narudzbenica = korisnik.Narudzbenice.Where(x => x.Status == StatusNarudzbenice.Otvorena).FirstOrDefault();

            if (narudzbenica != null)
            {
                ViewBag.TotalNarudzbenice = narudzbenica.Stavke.Sum(x => x.Cena * x.Kolicina);
            }
            else
            {
                ViewBag.TotalNarudzbenice = "Nema artikala";
            }

            return View(db.Artikli.ToList());
        }

        // GET: Artikli/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikal artikal = db.Artikli.Find(id);
            if (artikal == null)
            {
                return HttpNotFound();
            }
            return View(artikal);
        }

        // GET: Artikli/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artikli/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ime,Opis,Cena,RaspolozivaKolicina")] Artikal artikal,
            HttpPostedFileBase upload)
        {
            ModelState.Clear();

            if (upload != null)
            {
                artikal.Slika = upload.FileName;
            }
            
            if (ModelState.IsValid)
            {
                db.Artikli.Add(artikal);
                db.SaveChanges();

                string path = Server.MapPath("~/Content/Images/Artikli/") + artikal.Slika;
                upload.SaveAs(path);

                return RedirectToAction("Index");
            }

            return View(artikal);
        }

        // GET: Artikli/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikal artikal = db.Artikli.Find(id);
            if (artikal == null)
            {
                return HttpNotFound();
            }
            return View(artikal);
        }

        // POST: Artikli/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ime,Opis,Cena,RaspolozivaKolicina")] Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artikal);
        }

        // GET: Artikli/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikal artikal = db.Artikli.Find(id);
            if (artikal == null)
            {
                return HttpNotFound();
            }
            return View(artikal);
        }

        // POST: Artikli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artikal artikal = db.Artikli.Find(id);
            db.Artikli.Remove(artikal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult OrderItem(int id)
        {
            //nalazimo trenutno ulogovanog korisnika
            var korisnikId = User.Identity.GetUserId();
            var korisnik = db.Users.Find(korisnikId);

            //nalazimo otvorenu narudzbenicu za korisnika
            var narudzbenica = db.Narudzbenice.Where(x => x.User.Id == korisnikId && x.Status == StatusNarudzbenice.Otvorena)
                                              .FirstOrDefault();//prvi element ako postoji ili null

            if (narudzbenica == null)
            {
                //ako ne postoji narudzbenica kreiramo novu
                narudzbenica = new Narudzbenica();
                narudzbenica.User = korisnik;
                narudzbenica.Status = StatusNarudzbenice.Otvorena;

                //kazemo db kontekstu da je ovo nova narudzbenica (treba da se snimi u bazu)
                db.Narudzbenice.Add(narudzbenica);
            }

            //nalazimo artikal
            var artikal = db.Artikli.Find(id);

            Stavka stavka = null;

            if (narudzbenica.Stavke != null)
            {
                stavka = narudzbenica.Stavke.Where(x => x.Artikal.Id == artikal.Id).FirstOrDefault();
            }

            if (stavka == null)
            {
                stavka = new Stavka();
                stavka.Artikal = artikal;
                stavka.Narudzbenica = narudzbenica;
                stavka.Kolicina = 1;
                stavka.Cena = artikal.Cena;
                db.Stavke.Add(stavka);
            }
            else
            {
                stavka.Kolicina++;
                if (stavka.Cena != artikal.Cena)
                {
                    stavka.Cena = artikal.Cena;
                }
            }

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
