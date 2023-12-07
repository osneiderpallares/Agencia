using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agencia.Models;
using Agencia.Permisos;

namespace Agencia.Controllers
{
    [ValidateSessionAtribute]
    public class ReservasController : Controller
    {
        private AGENCIAModelado db = new AGENCIAModelado();

        // GET: Reservas
        public ActionResult Index(int? id_habitacion, int? id_hotel)
        {
            var reservas = db.Reservas.Include(r => r.Estados);

            var usuario = db.Usuarios.Find(Session["usuario"]);
            ViewBag.nombre_usuario = usuario.nombres + " " + usuario.apellidos;

            var hotel = db.Hoteles.Find(id_hotel);
            ViewBag.nombre_hotel = hotel.nombre;

            var habitacion = db.Habitaciones.Find(id_habitacion);
            ViewBag.nombre_habitacion = habitacion.nombre;

            return View(reservas.ToList());
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservas reservas = db.Reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            var usuario = db.Usuarios.Find(reservas.usuario);
            var hotel = db.Hoteles.Find(reservas.hotel);
            var habitacion = db.Habitaciones.Find(reservas.habitacion);
            ViewBag.nombre_usuario = usuario.nombres + " " + usuario.apellidos;
            ViewBag.nombre_hotel = hotel.nombre;
            ViewBag.nombre_habitacion = habitacion.nombre;
            return View(reservas);
        }

        // GET: Reservas/Create
        public ActionResult Create(int id_hotel, int id_habitacion)
        {
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre");
            //ViewBag.id_hotel = new SelectList(db.Hoteles, "id", "nombre");
            //ViewBag.id_habitacion = new SelectList(db.Habitaciones, "id", "nombre");
            ViewBag.id_hotel = id_hotel;
            ViewBag.id_habitacion = id_habitacion;
            return View();
        }

        // POST: Reservas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,usuario,hotel,habitacion,fechaEntrada,fechaSalida,ciudadDestino,cantidadPersonas,nombreEmergencia,apellidoEmergencia,telefomoEmergencia,estado")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                db.Reservas.Add(reservas);
                db.SaveChanges();
                return RedirectToAction("../Habitaciones/Index");
            }

            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", reservas.estado);
            ViewBag.id_hotel = reservas.hotel;
            ViewBag.id_habitacion = reservas.habitacion;          

            return View(reservas);
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservas reservas = db.Reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", reservas.estado);
            return View(reservas);
        }

        // POST: Reservas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,usuario,hotel,habitacion,fechaEntrada,fechaSalida,ciudadDestino,cantidadPersonas,nombreEmergencia,apellidoEmergencia,telefomoEmergencia,estado")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", reservas.estado);
            return View(reservas);
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservas reservas = db.Reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            return View(reservas);            
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservas reservas = db.Reservas.Find(id);
            db.Reservas.Remove(reservas);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("../Habitaciones/index");
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
