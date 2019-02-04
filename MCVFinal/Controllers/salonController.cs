using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCVFinal;

namespace MCVFinal.Controllers
{
    public class salonController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: salon
        public ActionResult Index()
        {
            return View(db.salons.ToList());
        }

        // GET: salon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salon salon = db.salons.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        // GET: salon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: salon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_salon,nombre,capacidad")] salon salon)
        {
            if (ModelState.IsValid)
            {
                db.salons.Add(salon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salon);
        }

        // GET: salon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salon salon = db.salons.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        // POST: salon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_salon,nombre,capacidad")] salon salon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salon);
        }

        // GET: salon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salon salon = db.salons.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        // POST: salon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            salon salon = db.salons.Find(id);
            db.salons.Remove(salon);
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
