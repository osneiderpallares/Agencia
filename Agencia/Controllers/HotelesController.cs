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
    public class HotelesController : Controller
    {
        private AGENCIAModel db = new AGENCIAModel();

        // GET: Hoteles
        public ActionResult Index()
        {
            var hoteles = db.Hoteles.Include(h => h.Estados);
            return View(hoteles.ToList());
        }

        // GET: Hoteles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hoteles hoteles = db.Hoteles.Find(id);
            if (hoteles == null)
            {
                return HttpNotFound();
            }
            return View(hoteles);
        }

        // GET: Hoteles/Create
        public ActionResult Create()
        {
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre");
            return View();
        }

        // POST: Hoteles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,direccion,ciudad,telefono,email,estado")] Hoteles hoteles)
        {
            if (ModelState.IsValid)
            {
                db.Hoteles.Add(hoteles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", hoteles.estado);
            return View(hoteles);
        }

        // GET: Hoteles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hoteles hoteles = db.Hoteles.Find(id);
            if (hoteles == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", hoteles.estado);
            return View(hoteles);
        }

        // POST: Hoteles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,direccion,ciudad,telefono,email,estado")] Hoteles hoteles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoteles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", hoteles.estado);
            return View(hoteles);
        }

        // GET: Hoteles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hoteles hoteles = db.Hoteles.Find(id);
            if (hoteles == null)
            {
                return HttpNotFound();
            }
            return View(hoteles);
        }

        // POST: Hoteles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hoteles hoteles = db.Hoteles.Find(id);
            db.Hoteles.Remove(hoteles);
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
