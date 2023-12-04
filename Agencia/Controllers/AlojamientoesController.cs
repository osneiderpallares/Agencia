using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agencia.Models;

namespace Agencia.Controllers
{
    public class AlojamientoesController : Controller
    {
        private AGENCIAModel db = new AGENCIAModel();

        // GET: Alojamientoes
        public ActionResult Index()
        {
            var alojamiento = db.Alojamiento.Include(a => a.Habitaciones).Include(a => a.Hoteles).Include(a => a.Usuarios);
            return View(alojamiento.ToList());
        }

        // GET: Alojamientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alojamiento alojamiento = db.Alojamiento.Find(id);
            if (alojamiento == null)
            {
                return HttpNotFound();
            }
            return View(alojamiento);
        }

        // GET: Alojamientoes/Create
        public ActionResult Create()
        {
            ViewBag.habitacion = new SelectList(db.Habitaciones, "id", "nombre");
            ViewBag.hotel = new SelectList(db.Hoteles, "id", "nombre");
            ViewBag.usuario = new SelectList(db.Usuarios, "id", "numeroDocumento");
            return View();
        }

        // POST: Alojamientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,usuario,hotel,habitacion,fechaEntrada,fechaSalida,cantidadPersonas,ciudadDestino")] Alojamiento alojamiento)
        {
            if (ModelState.IsValid)
            {
                db.Alojamiento.Add(alojamiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.habitacion = new SelectList(db.Habitaciones, "id", "nombre", alojamiento.habitacion);
            ViewBag.hotel = new SelectList(db.Hoteles, "id", "nombre", alojamiento.hotel);
            ViewBag.usuario = new SelectList(db.Usuarios, "id", "numeroDocumento", alojamiento.usuario);
            return View(alojamiento);
        }

        // GET: Alojamientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alojamiento alojamiento = db.Alojamiento.Find(id);
            if (alojamiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.habitacion = new SelectList(db.Habitaciones, "id", "nombre", alojamiento.habitacion);
            ViewBag.hotel = new SelectList(db.Hoteles, "id", "nombre", alojamiento.hotel);
            ViewBag.usuario = new SelectList(db.Usuarios, "id", "numeroDocumento", alojamiento.usuario);
            return View(alojamiento);
        }

        // POST: Alojamientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,usuario,hotel,habitacion,fechaEntrada,fechaSalida,cantidadPersonas,ciudadDestino")] Alojamiento alojamiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alojamiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.habitacion = new SelectList(db.Habitaciones, "id", "nombre", alojamiento.habitacion);
            ViewBag.hotel = new SelectList(db.Hoteles, "id", "nombre", alojamiento.hotel);
            ViewBag.usuario = new SelectList(db.Usuarios, "id", "numeroDocumento", alojamiento.usuario);
            return View(alojamiento);
        }

        // GET: Alojamientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alojamiento alojamiento = db.Alojamiento.Find(id);
            if (alojamiento == null)
            {
                return HttpNotFound();
            }
            return View(alojamiento);
        }

        // POST: Alojamientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alojamiento alojamiento = db.Alojamiento.Find(id);
            db.Alojamiento.Remove(alojamiento);
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
