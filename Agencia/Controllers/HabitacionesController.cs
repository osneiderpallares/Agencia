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
    public class HabitacionesController : Controller
    {
        private AGENCIAModelado db = new AGENCIAModelado();

        // GET: Habitaciones
        public ActionResult Index(string searchString = "", string filtro = "")
        {
            var habitaciones = db.Habitaciones.Include(h => h.Estados).Include(h => h.Hoteles).Include(h => h.tiposHabitaciones).Include(h => h.Ubicacion1)
                .Where(h => h.Hoteles.estado == 1).Where(ha => ha.estado == 1);


            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(filtro))
            {
                switch (filtro)
                {
                    case "hotel":
                        habitaciones = habitaciones.Where(s => s.Hoteles.nombre.Contains(searchString));
                        break;

                    case "fechaEntrada":                        
                        habitaciones = habitaciones.Where(s => Convert.ToDateTime(s.fechaEntrada).ToShortDateString() == (Convert.ToDateTime(searchString).ToShortDateString()));
                        break;

                    case "fechaSalida":
                        habitaciones = habitaciones.Where(s => Convert.ToDateTime(s.fechaSalida).ToShortDateString() == (Convert.ToDateTime(searchString).ToShortDateString()));
                        break;

                    case "totalPersonas":
                        habitaciones = habitaciones.Where(s => s.totalPersonas == int.Parse(searchString));
                        break;

                    case "ciudad":
                        habitaciones = habitaciones.Where(s => s.Hoteles.ciudad.Contains(searchString));
                        break;
                }                                    
            }

            var usuario = db.Usuarios.Find(Session["usuario"]);

            ViewBag.reservas = (from hoteles in db.Hoteles
                                join reservas in db.Reservas on hoteles.id equals reservas.hotel
                                join _habitaciones in db.Habitaciones on reservas.habitacion equals _habitaciones.id
                                select new ModelNew
                                {
                                    id = reservas.id,
                                    fechaEntrada = reservas.fechaEntrada,
                                    fechaSalida = reservas.fechaSalida,
                                    cantidadPersonas = reservas.cantidadPersonas,
                                    ciudadDestino = reservas.ciudadDestino,
                                    nombreEmergencia = reservas.nombreEmergencia,
                                    apellidoEmergencia = reservas.apellidoEmergencia,
                                    telefomoEmergencia = reservas.telefomoEmergencia,
                                    nombre_usuario = usuario.nombres + " " + usuario.apellidos,
                                    nombre_hotel = hoteles.nombre,
                                    nombre_habitacion = _habitaciones.nombre,
                                }).ToArray();
              

            return View(habitaciones.ToList());
        }

        public ActionResult Lista()
        {
            var habitaciones = db.Habitaciones.Include(h => h.Estados).Include(h => h.Hoteles).Include(h => h.tiposHabitaciones).Include(h => h.Ubicacion1)
                .Where(h => h.Hoteles.estado == 1).Where(ha => ha.estado == 1);
            return View(habitaciones.ToList());

        }

        // GET: Habitaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            if (habitaciones == null)
            {
                return HttpNotFound();
            }
            return View(habitaciones);
        }

        // GET: Habitaciones/Create
        public ActionResult Create()
        {
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre");
            ViewBag.hotel = new SelectList(db.Hoteles, "id", "nombre");
            ViewBag.tipoHabitacion = new SelectList(db.tiposHabitaciones, "id", "nombre");
            ViewBag.ubicacion = new SelectList(db.Ubicacion, "id", "nombre");
            return View();
        }

        // POST: Habitaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,hotel,tipoHabitacion,nombre,descripcion,costo,impuesto,numeroAdultos,numeroNiños,totalPersonas,ubicacion,estado")] Habitaciones habitaciones)
        {
            if (ModelState.IsValid)
            {
                db.Habitaciones.Add(habitaciones);
                db.SaveChanges();
                return RedirectToAction("Lista");
            }

            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", habitaciones.estado);
            ViewBag.hotel = new SelectList(db.Hoteles, "id", "nombre", habitaciones.hotel);
            ViewBag.tipoHabitacion = new SelectList(db.tiposHabitaciones, "id", "nombre", habitaciones.tipoHabitacion);
            ViewBag.ubicacion = new SelectList(db.Ubicacion, "id", "nombre", habitaciones.ubicacion);
            return View(habitaciones);
        }

        // GET: Habitaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            if (habitaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", habitaciones.estado);
            ViewBag.hotel = new SelectList(db.Hoteles, "id", "nombre", habitaciones.hotel);
            ViewBag.tipoHabitacion = new SelectList(db.tiposHabitaciones, "id", "nombre", habitaciones.tipoHabitacion);
            ViewBag.ubicacion = new SelectList(db.Ubicacion, "id", "nombre", habitaciones.ubicacion);
            return View(habitaciones);
        }

        // POST: Habitaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,hotel,tipoHabitacion,nombre,descripcion,costo,impuesto,numeroAdultos,numeroNiños,totalPersonas,ubicacion,estado")] Habitaciones habitaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(habitaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Lista");
            }
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", habitaciones.estado);
            ViewBag.hotel = new SelectList(db.Hoteles, "id", "nombre", habitaciones.hotel);
            ViewBag.tipoHabitacion = new SelectList(db.tiposHabitaciones, "id", "nombre", habitaciones.tipoHabitacion);
            ViewBag.ubicacion = new SelectList(db.Ubicacion, "id", "nombre", habitaciones.ubicacion);
            return View(habitaciones);
        }

        // GET: Habitaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            if (habitaciones == null)
            {
                return HttpNotFound();
            }
            return View(habitaciones);
        }

        // POST: Habitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Habitaciones habitaciones = db.Habitaciones.Find(id);
            db.Habitaciones.Remove(habitaciones);
            db.SaveChanges();
            return RedirectToAction("Lista");
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
