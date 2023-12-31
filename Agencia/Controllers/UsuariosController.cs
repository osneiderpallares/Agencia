﻿using System;
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
    public class UsuariosController : Controller
    {
        private AGENCIAModelado db = new AGENCIAModelado();

        // GET: Usuarios
        public ActionResult Index()
        {
            int usuario =Convert.ToInt32(Session["usuario"]);
            var usuarios = db.Usuarios.Include(u => u.Generos).Include(u => u.tiposDocumentos).Where(d => d.id != usuario);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.genero = new SelectList(db.Generos, "id", "nombre");
            ViewBag.tipoDocumento = new SelectList(db.tiposDocumentos, "id", "nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tipoDocumento,numeroDocumento,nombres,apellidos,email,genero,telefono,fechaNacimiento,nombreUsuario,clave")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.estado = 1;
                usuarios.rol = 2;
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.genero = new SelectList(db.Generos, "id", "nombre", usuarios.genero);
            ViewBag.tipoDocumento = new SelectList(db.tiposDocumentos, "id", "nombre", usuarios.tipoDocumento);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.genero = new SelectList(db.Generos, "id", "nombre", usuarios.genero);
            ViewBag.tipoDocumento = new SelectList(db.tiposDocumentos, "id", "nombre", usuarios.tipoDocumento);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tipoDocumento,numeroDocumento,nombres,apellidos,email,genero,telefono,fechaNacimiento,nombreUsuario,clave")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.genero = new SelectList(db.Generos, "id", "nombre", usuarios.genero);
            ViewBag.tipoDocumento = new SelectList(db.tiposDocumentos, "id", "nombre", usuarios.tipoDocumento);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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
