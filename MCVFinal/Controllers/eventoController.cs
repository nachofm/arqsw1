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
    public class eventoController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: evento
        public ActionResult Index()
        {
            var eventoes = db.eventoes.Include(e => e.salon);
            return View(eventoes.ToList());
        }

        public ActionResult VerInvitados(int id)
        {
            int id_obtenido = id;
            return RedirectToAction("Index", "listado", new { id = id_obtenido });
           
        }

        // GET: evento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento evento = db.eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: evento/Create
        public ActionResult Create()
        {
            ViewBag.salon_id_salon = new SelectList(db.salons, "id_salon", "nombre");
            return View();
        }

        // POST: evento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_evento,nombre,organizador,salon_id_salon")] evento evento)
        {
            if (ModelState.IsValid)
            {
                db.eventoes.Add(evento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.salon_id_salon = new SelectList(db.salons, "id_salon", "nombre", evento.salon_id_salon);
            return View(evento);
        }

        // GET: evento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento evento = db.eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            ViewBag.salon_id_salon = new SelectList(db.salons, "id_salon", "nombre", evento.salon_id_salon);
            return View(evento);
        }

        // POST: evento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_evento,nombre,organizador,salon_id_salon")] evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.salon_id_salon = new SelectList(db.salons, "id_salon", "nombre", evento.salon_id_salon);
            return View(evento);
        }

        // GET: evento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento evento = db.eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            evento evento = db.eventoes.Find(id);
            db.eventoes.Remove(evento);
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
