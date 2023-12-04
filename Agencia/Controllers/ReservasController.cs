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
        private AGENCIAModel db = new AGENCIAModel();

        // GET: Reservas
        public ActionResult Index()
        {
            var reservas = db.Reservas.Include(r => r.Alojamiento1).Include(r => r.contactoEmergencia1).Include(r => r.Estados);
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
            return View(reservas);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            ViewBag.alojamiento = new SelectList(db.Alojamiento, "id", "ciudadDestino");
            ViewBag.contactoEmergencia = new SelectList(db.contactoEmergencia, "id", "nombres");
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre");
            return View();
        }

        // POST: Reservas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,alojamiento,contactoEmergencia,fecha,estado")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                db.Reservas.Add(reservas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.alojamiento = new SelectList(db.Alojamiento, "id", "ciudadDestino", reservas.alojamiento);
            ViewBag.contactoEmergencia = new SelectList(db.contactoEmergencia, "id", "nombres", reservas.contactoEmergencia);
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", reservas.estado);
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
            ViewBag.alojamiento = new SelectList(db.Alojamiento, "id", "ciudadDestino", reservas.alojamiento);
            ViewBag.contactoEmergencia = new SelectList(db.contactoEmergencia, "id", "nombres", reservas.contactoEmergencia);
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", reservas.estado);
            return View(reservas);
        }

        // POST: Reservas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,alojamiento,contactoEmergencia,fecha,estado")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.alojamiento = new SelectList(db.Alojamiento, "id", "ciudadDestino", reservas.alojamiento);
            ViewBag.contactoEmergencia = new SelectList(db.contactoEmergencia, "id", "nombres", reservas.contactoEmergencia);
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
