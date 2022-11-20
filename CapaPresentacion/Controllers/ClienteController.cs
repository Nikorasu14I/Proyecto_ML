using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaPresentacion.Models;
using Model.Entity;

namespace CapaPresentacion.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult listar()
        {
            var data = new Model.Neg.ClienteNeg().ReadC();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult listarD()
        {
            var data = new Model.Neg.ClienteNeg().Read();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarC()
        {
            var data = new Model.Neg.ClienteNeg().ReadC();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult InsertC(Cliente_Venta OCliente)
        {
            bool respuesta = true;
            Model.Neg.ClienteNeg.Agregar(OCliente);          

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertR(Cliente_Venta OCliente)
        {
            
            int ID = Model.Neg.ClienteNeg.AgregarR(OCliente);

            return Json(ID);
        }

        [HttpPost]
        public JsonResult Buscar(Cliente_Venta OCliente)
        {
            Cliente_Venta Cliente = new Model.Neg.ClienteNeg().Buscar(OCliente);

            return Json(Cliente);
        }

        [HttpPost]        
        public JsonResult Guardar(Cliente_Venta OCliente)
        {
            bool respuesta = true;
            try
            {
                if (OCliente.ID_Cliente == 0)
                {
                    Model.Neg.ClienteNeg.Agregar(OCliente);
                }
                else
                {
                    Model.Neg.ClienteNeg.Actualizar(OCliente);
                }
            }
            catch
            {
                respuesta = false;
            }

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult Elimina(int IdCliente)
        {
            bool respuesta = true;
            try
            {
                Model.Neg.ClienteNeg.Eliminar(IdCliente);
            }
            catch
            {
                respuesta = false;
            }
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}