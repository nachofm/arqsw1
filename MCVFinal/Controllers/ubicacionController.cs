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
    public class ubicacionController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: ubicacion
        public ActionResult Index()
        {
            return View(db.ubicacions.ToList());
        }

        // GET: ubicacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ubicacion ubicacion = db.ubicacions.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            return View(ubicacion);
        }

        // GET: ubicacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ubicacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mesa,descripcion")] ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                db.ubicacions.Add(ubicacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ubicacion);
        }

        // GET: ubicacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ubicacion ubicacion = db.ubicacions.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            return View(ubicacion);
        }

        // POST: ubicacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mesa,descripcion")] ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ubicacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ubicacion);
        }

        // GET: ubicacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ubicacion ubicacion = db.ubicacions.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            return View(ubicacion);
        }

        // POST: ubicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ubicacion ubicacion = db.ubicacions.Find(id);
            db.ubicacions.Remove(ubicacion);
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
