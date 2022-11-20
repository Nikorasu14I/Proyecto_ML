using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Entity;
using Model.Neg;

namespace CapaPresentacion.Controllers
{
    public class AdminController : Controller
    {
        private UsuarioNeg ObjUsuarioNeg;
        // GET: Acceso
        public AdminController()
        {
            ObjUsuarioNeg = new UsuarioNeg();
        }

        public ActionResult Inicio()
        {
            ViewData["Cliente"] = ObjUsuarioNeg.ReadC();
            ViewData["Evento"] = ObjUsuarioNeg.ReadE();
            ViewData["Almacen"] = ObjUsuarioNeg.ReadA();
            ViewData["Transporte"] = ObjUsuarioNeg.ReadT();
            return View();
        }

        public ActionResult Empleado()
        {
            return View();
        }

        public ActionResult Almacen()
        {
            return View();
        }

        public ActionResult Proforma()
        {
            return View();
        }

        public ActionResult Transporte()
        {
            return View();
        }
        public ActionResult Historial()
        {
            return View();
        }

        public ActionResult Cliente()
        {
            return View();
        }

        // GET: Admin
        public ActionResult Usuario()
        {
            return View();
        }

        [HttpGet]
        public JsonResult listarE()
        {
            var data = new Model.Neg.UsuarioNeg().listarE();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarER()
        {
            var data = new Model.Neg.UsuarioNeg().listarER();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listar()
        {
            var data = new Model.Neg.UsuarioNeg().listar();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Usuario OUsuario)
        {
            OUsuario.Pass = "123456789";
            bool respuesta = true;
            try
            {
                if (OUsuario.ID_Empleado != 0)
                {                 
                    Model.Neg.UsuarioNeg.Agregar(OUsuario);
                }
                else
                {
                   
                }
            }
            catch
            {
                respuesta = false;
            }

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarE(Empleado OCliente)
        {
            bool respuesta = true;
            try
            {
                if (OCliente.ID_Empleado == 0)
                {
                    Model.Neg.UsuarioNeg.AgregarE(OCliente);
                }
                else
                {
                    Model.Neg.UsuarioNeg.ActualizarE(OCliente);
                }
            }
            catch
            {
                respuesta = false;
            }

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarU(int IdCliente)
        {
            bool respuesta = true;
            try
            {
                UsuarioNeg.EliminarU(IdCliente);
            }
            catch
            {
                respuesta = false;
            }
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

    }
}