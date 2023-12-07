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
    public class tiposDocumentosController : Controller
    {
        private AGENCIAModelado db = new AGENCIAModelado();

        // GET: tiposDocumentos
        public ActionResult Index()
        {
            return View(db.tiposDocumentos.ToList());
        }

        // GET: tiposDocumentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tiposDocumentos tiposDocumentos = db.tiposDocumentos.Find(id);
            if (tiposDocumentos == null)
            {
                return HttpNotFound();
            }
            return View(tiposDocumentos);
        }

        // GET: tiposDocumentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tiposDocumentos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] tiposDocumentos tiposDocumentos)
        {
            if (ModelState.IsValid)
            {
                db.tiposDocumentos.Add(tiposDocumentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiposDocumentos);
        }

        // GET: tiposDocumentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tiposDocumentos tiposDocumentos = db.tiposDocumentos.Find(id);
            if (tiposDocumentos == null)
            {
                return HttpNotFound();
            }
            return View(tiposDocumentos);
        }

        // POST: tiposDocumentos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] tiposDocumentos tiposDocumentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposDocumentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiposDocumentos);
        }

        // GET: tiposDocumentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tiposDocumentos tiposDocumentos = db.tiposDocumentos.Find(id);
            if (tiposDocumentos == null)
            {
                return HttpNotFound();
            }
            return View(tiposDocumentos);
        }

        // POST: tiposDocumentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tiposDocumentos tiposDocumentos = db.tiposDocumentos.Find(id);
            db.tiposDocumentos.Remove(tiposDocumentos);
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
