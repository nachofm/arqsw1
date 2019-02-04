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
    public class listadoController : Controller
    {
        public static int idEvento;
        private mydbEntities db = new mydbEntities();

        // GET: listado
        public ActionResult Index(int? id)
        {
            List<listado> listadoes = new List<listado> { };
            if (id == 0)
            { listadoes = db.listadoes.Include(l => l.evento).Include(l => l.ubicacion).ToList(); }
            else { listadoes = db.listadoes.Where(a => a.evento_id_evento == id).ToList(); }
            return View(listadoes);
        }

        [HttpPost]
        public ActionResult Index(listado lista, string filtroApellido, string filtroNombre, string filtroUbicacion)
        {
            mydbEntities db = new mydbEntities();
            if (!string.IsNullOrEmpty(filtroApellido))
            {
                return View(db.listadoes.Where(i => i.apellido.ToUpper().StartsWith(filtroApellido.ToUpper())).ToList());
            }
            if (!string.IsNullOrEmpty(filtroNombre))
            {
                return View(db.listadoes.Where(i => i.nombre.ToUpper().StartsWith(filtroNombre.ToUpper())).ToList());
            }
            if (!string.IsNullOrEmpty(filtroUbicacion))
            {
                return View(db.listadoes.Where(i => i.ubicacion.descripcion.ToUpper().Equals(filtroUbicacion.ToUpper())).ToList());
            }
            else
            {
                return View(db.listadoes.ToList());
            }
        }

        public ActionResult Presente(int id, int id2)
        {
            listado listado = db.listadoes.Find(id);
            if (listado == null)
            {
                return HttpNotFound();
            }
            listado.presente = "SI";
            db.SaveChanges();
            return RedirectToAction("Index", new { id = id2 });
        }


        public ActionResult Listar()
        {
            ViewBag.evento_id_evento = new SelectList(db.eventoes, "id_evento", "nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Listar([Bind(Include = "evento_id_evento")] listado listado)
        {
            string id_obtenido = Request.Form["evento_id_evento"].ToString();
            return RedirectToAction("Index", new { id = id_obtenido });
        }

        public ActionResult Reporte()
        {
            ViewBag.evento_id_evento = new SelectList(db.eventoes, "id_evento", "nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Reporte([Bind(Include = "evento_id_evento")] listado listado)
        {
            int id_obtenido = Convert.ToInt32(Request.Form["evento_id_evento"]);
            Reporte report = new Reporte();
            mydbEntities db = new mydbEntities();
            report.ListaAusentes = (db.listadoes.Where(i => i.presente == "NO").Where(i=> i.evento_id_evento == id_obtenido)).ToList();
            report.ListaPresentes = (db.listadoes.Where(i => i.presente == "SI").Where(i => i.evento_id_evento == id_obtenido)).ToList();
            report.Presentes = report.ListaPresentes.Count();
            report.Ausentes = report.ListaAusentes.Count();

            return View("MostrarReporte", report);
        }



        // GET: listado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            listado listado = db.listadoes.Find(id);
            if (listado == null)
            {
                return HttpNotFound();
            }
            return View(listado);
        }

        // GET: listado/Create
        public ActionResult Create()
        {
            ViewBag.evento_id_evento = new SelectList(db.eventoes, "id_evento", "nombre");
            ViewBag.ubicacion_id_mesa = new SelectList(db.ubicacions, "id_mesa", "descripcion");
            return View();
        }



        // POST: listado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_listado,nombre,apellido,evento_id_evento,ubicacion_id_mesa,presente")] listado listado)
        {
            if (ModelState.IsValid)
            {
                db.listadoes.Add(listado);
                db.SaveChanges();
                
                //var identificador = (from e in db.eventoes
                //                    where e.nombre == nombreEvento
                //                    select e.id_evento);
                //string id_obtenido = identificador.ToString();
                string id_obtenido = Request.Form["evento_id_evento"].ToString();
                return RedirectToAction("Index", new { id = id_obtenido });
            }

            ViewBag.evento_id_evento = new SelectList(db.eventoes, "id_evento", "nombre", listado.evento_id_evento);
            ViewBag.ubicacion_id_mesa = new SelectList(db.ubicacions, "id_mesa", "descripcion", listado.ubicacion_id_mesa);
            return View(listado);
        }

        // GET: listado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            listado listado = db.listadoes.Find(id);
            if (listado == null)
            {
                return HttpNotFound();
            }
            ViewBag.evento_id_evento = new SelectList(db.eventoes, "id_evento", "nombre", listado.evento_id_evento);
            ViewBag.ubicacion_id_mesa = new SelectList(db.ubicacions, "id_mesa", "descripcion", listado.ubicacion_id_mesa);
            return View(listado);
        }

        // POST: listado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_listado,nombre,apellido,evento_id_evento,ubicacion_id_mesa,presente")] listado listado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listado).State = EntityState.Modified;
                db.SaveChanges();
                string id_obtenido = Request.Form["evento_id_evento"].ToString();
                return RedirectToAction("Index", new { id = id_obtenido });
            }
            ViewBag.evento_id_evento = new SelectList(db.eventoes, "id_evento", "nombre", listado.evento_id_evento);
            ViewBag.ubicacion_id_mesa = new SelectList(db.ubicacions, "id_mesa", "descripcion", listado.ubicacion_id_mesa);
            return View(listado);
        }

        // GET: listado/Delete/5
        public ActionResult Delete(int? id, int id2)
        {
            idEvento = (int)id2;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            listado listado = db.listadoes.Find(id);
            if (listado == null)
            {
                return HttpNotFound();
            }
            return View(listado);
            
        }

        // POST: listado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int idE = (int)idEvento;
            listado listado = db.listadoes.Find(id);
            db.listadoes.Remove(listado);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = idE });
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
