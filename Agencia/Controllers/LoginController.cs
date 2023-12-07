using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Agencia.Models;

namespace Agencia.Controllers
{
    public class LoginController : Controller
    {
        private AGENCIAModelado db = new AGENCIAModelado();        
        // GET: Login
        public ActionResult Index()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuarios usuario)
        {
            if (ModelState.IsValid) {
                
                var usuarios = db.Usuarios.Where(c => c.email == usuario.email).Where(d => d.clave == usuario.clave).FirstOrDefault();            

                if (usuarios != null)
                {
                    Session["usuario"] = usuarios.id;
                    Session["rol"] = usuario.rol;
                    return RedirectToAction("Index", "Home");
                    //return View(usuarios);
                }
            }            
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Register([Bind(Include = "id,tipoDocumento,numeroDocumento,nombres,apellidos,email,genero,telefono,fechaNacimiento,nombreUsuario,clave,rol")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.rol = 2;
                usuarios.estado = 1;
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.roles = new SelectList(db.Roles, "id", "nombre", usuarios.rol);
            ViewBag.estado = new SelectList(db.Estados, "id", "nombre", usuarios.estado);
            ViewBag.genero = new SelectList(db.Generos, "id", "nombre", usuarios.genero);
            ViewBag.tipoDocumento = new SelectList(db.tiposDocumentos, "id", "nombre", usuarios.tipoDocumento);
            return RedirectToAction("Index", "Login");
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Index", "Login");

        }
    }
}