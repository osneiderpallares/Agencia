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
        private AGENCIAModel db = new AGENCIAModel();        
        // GET: Login
        public ActionResult Index()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuarios usuario)
        {
            System.Console.Write(usuario);
            var usuarios = db.Usuarios.Where(c => c.email == usuario.email).Where(d => d.clave == usuario.clave).Count();            

            if (usuarios > 0)
            {
                Session["usuario"] = usuarios;
                return RedirectToAction("Index", "Home");
                //return View(usuarios);
            }

            ViewData["mensaje"] = "Usuario no encontrado";
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Index", "Login");

        }
    }
}