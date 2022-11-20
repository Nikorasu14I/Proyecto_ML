 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Entity;
using Model.Neg;

namespace CapaPresentacion.Controllers
{
    public class ServicioController : Controller
    {
        // GET: Servicio

        private FacturaNeg objFacturaNeg;
        private ProductoNeg objProductoNeg;
        private DetalleFacturaNeg objDetalleFacturaNeg;
        public ServicioController()
        {           
            objFacturaNeg = new FacturaNeg();
            objProductoNeg = new ProductoNeg();
            objDetalleFacturaNeg = new DetalleFacturaNeg();
        }

        public ActionResult Factura(int ID)
        {
            Factura Cabeza = new FacturaNeg().VistaF(ID);
            int ID_Contrato = objFacturaNeg.BuscarC(ID);
            List<Servicios> ListadoDetalle = new FacturaNeg().VistaDF(ID);
            ViewData["Fecha"] = Cabeza.Fecha;
            ViewData["Nombre"] = Cabeza.Nombre;
            ViewData["Cel"] = Cabeza.Telefono;
            ViewData["ID"] = Cabeza.ID_Factura;
            ViewData["Suma"] = Cabeza.Subtotal;
            ViewData["Descuento"] = Cabeza.Descuento;
            ViewData["Subtotal"] = Cabeza.Total - Cabeza.Impuesto;
            ViewData["Iva"] = Cabeza.Impuesto;
            ViewData["Total"] = Cabeza.Total;
            ViewData["ID_C"] = ID_Contrato;
            ViewData["Direccion"] = Cabeza.Direccion;
            return View(ListadoDetalle);
        }

        public ActionResult Recibo(int ID)
        {
            Recibo Cabeza = new FacturaNeg().VistaR(ID);
            ViewData["ID"] = ID;
            ViewData["Fecha"] = Cabeza.Fecha;
            ViewData["Nombre"] = Cabeza.Nombre;
            ViewData["Metodo"] = Cabeza.Forma;
            ViewData["Monto"] = Cabeza.Monto;
            ViewData["Efectivo"] = Cabeza.Efectivo;
            ViewData["Cheque"] = Cabeza.Cheque;
            ViewData["Concepto"] = Cabeza.Concepto;
            ViewData["Banco"] = Cabeza.Banco;
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

        [HttpPost]
        public JsonResult InsertS(Servicios OServicio)
        {
            bool respuesta = true;
            try
            {
                if (OServicio.ID_Servicio == 0)
                {
                    ProductoNeg.Agregar(OServicio);
                }
                else
                {
                    ProductoNeg.Actualizar(OServicio);
                }
            }
            catch
            {
                respuesta = false;
            }
            
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int GuardarRecibo(Recibo ORecibo)
        {            
            Recibo objRecibo = new Recibo(ORecibo.Fecha, ORecibo.ID_Evento, 
            ORecibo.Monto, ORecibo.Forma , ORecibo.Efectivo, ORecibo.Cheque, ORecibo.Banco, ORecibo.Concepto);
            int ID = objProductoNeg.AgregarR(objRecibo);
            return ID;
        }

        public JsonResult Elimina(int IdAlmacen)
        {
            bool respuesta = true;
            try
            {
                Model.Neg.ProductoNeg.Eliminar(IdAlmacen);
            }
            catch
            {
                respuesta = false;
            }
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Transporte()
        {
            return View();
        }
        public ActionResult Historial()
        {
            return View();
        }
        public JsonResult Obtener(int IdAlmacen)
        {
            Servicios ocliente = new Servicios();
            return Json(ocliente, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarDisponible(string FechaE)
        {
            var data = new Model.Neg.ProductoNeg().ReadDisponible(FechaE);
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listardetalle (int id)
        {
            var data = new Model.Neg.ProductoNeg().ReadDetalle(id);
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarFactura()
        {
            var data = new Model.Neg.ProductoNeg().ReadVistaF();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarDeuda()
        {
            var data = new Model.Neg.ProductoNeg().ReadVistaD();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarRecibo(int ID)
        {
            var data = new Model.Neg.ProductoNeg().ReadVistaRecibo(ID);
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listar()
        {
            var data = new Model.Neg.ProductoNeg().ReadVista();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarT()
        {
            var data = new Model.Neg.ProductoNeg().ReadVistaT();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GuardarP(float Impuesto, float Subtotal, float Total, List<DetalleFactura> ListadoDetalle)
        {
            int ID_Proforma = 0;
            string Fecha = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            if (Total == 0)
            {

            }
            else
            {
                if (ID_Proforma != 0)
                {

                }
                else
                {

                    //REGISTRO DE PROFORMA
                    Proforma objFactura = new Proforma(Fecha, Subtotal, Impuesto, Total);
                    ID_Proforma = objFacturaNeg.createP(objFactura);
                    
                    if (ID_Proforma == 0)
                    {

                    }
                    else
                    {
                        //Registro del Detalle
                        foreach (var data in ListadoDetalle)
                        {
                            int IdProducto = Convert.ToInt32(data.ID_Servicio.ToString());
                            int cantidad = Convert.ToInt32(data.Cantidad.ToString());
                            double subtotal = Convert.ToDouble(data.SubTotal.ToString());
                            DetalleFactura objDetalleVenta =
                            new DetalleFactura(ID_Proforma, 1,IdProducto, cantidad, subtotal);
                            objDetalleFacturaNeg.createP(objDetalleVenta);
                        }
                    }
                }
            }
            return Json(ID_Proforma);
        }

    }
}