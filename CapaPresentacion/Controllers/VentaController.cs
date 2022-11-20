using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaPresentacion.Models;
using Model.Entity;
using Model.Neg;

namespace CapaPresentacion.Controllers
{
    public class VentaController : Controller
    {
        // GET: Venta

        private EventoNeg objEventoNeg;
        
        private ClienteNeg objClienteNeg;
        private ProductoNeg objProductoNeg;
        private FacturaNeg objFacturaNeg;
        private DetalleFacturaNeg objDetalleFacturaNeg;
        List<Servicios> Lista;

        public VentaController()
        {
            objEventoNeg = new EventoNeg();
            objClienteNeg = new ClienteNeg();
            objProductoNeg = new ProductoNeg();
            objFacturaNeg = new FacturaNeg();
            objDetalleFacturaNeg = new DetalleFacturaNeg();
        }

        public ActionResult Historial()
        {
            return View();
        }


        public ActionResult Registro()
        {
            Lista = objProductoNeg.ReadVista();
            return View(Lista);
        }

        public ActionResult Contrato(int ID)
        {
            Evento Cabeza= new EventoNeg().VistaC(ID);
            ViewData["Fecha"] = Cabeza.Fecha;
            ViewData["FechaC"] = Cabeza.FechaF;
            ViewData["Observacion"] = Cabeza.Observaciones;
            ViewData["Direccion"] = Cabeza.Direccion;
            ViewData["Adelanto"] = Cabeza.Adelanto;
            ViewData["Nombre"] = Cabeza.Nombre;
            ViewData["Total"] = Cabeza.ID_Cliente;
            ViewData["Horai"] = Cabeza.HoraI;
            ViewData["Horaf"] = Cabeza.HoraF;

            return View();
        }

        public ActionResult Recibo(int ID)
        {
            Recibo Cabeza = new FacturaNeg().VistaR(ID);
            ViewData["Fecha"] = Cabeza.Fecha;
            ViewData["Nombre"] = Cabeza.Nombre;
            ViewData["Metodo"] = Cabeza.Forma;
            ViewData["Monto"] = Cabeza.Monto;
            return View();
        }

        public ActionResult Factura(int ID)
        {
            Factura Cabeza = new FacturaNeg().VistaF(ID);
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
            ViewData["Direccion"] = Cabeza.Direccion;
            return View(ListadoDetalle);
        }

        public ActionResult Proforma(int ID)
        {
            Proforma Cabeza = new FacturaNeg().VistaP(ID);
            List<Servicios> ListadoDetalle = new FacturaNeg().VistaD(ID);
            ViewData["Fecha"] = Cabeza.Fecha;
            ViewData["Subtotal"] = Cabeza.Total - Cabeza.Impuesto;
            ViewData["Iva"] = Cabeza.Impuesto;
            ViewData["Total"] = Cabeza.Total;
            return View(ListadoDetalle);
        }

        public ActionResult Servicio()
        {
            var data = new Model.Neg.ProductoNeg().ReadVista();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MostrarClientes()
        {
            List<Cliente_Venta> lista = objClienteNeg.findAll();
            return View(lista);
        }

        [HttpPost]//para buscar clientes
        public ActionResult MostrarClientes(string txtnombre, string txtappaterno, string txtdni, long txtcliente = -1)
        {
            if (txtnombre == "")
            {
                txtnombre = "-1";
            }
            if (txtappaterno == "")
            {
                txtappaterno = "-1";
            }
            if (txtdni == "")
            {
                txtdni = "-1";
            }
            Cliente_Venta objCliente = new Cliente_Venta();
            objCliente.Nombre = txtnombre;
            objCliente.ID_Cliente = (int)txtcliente;
            objCliente.Apellido = txtappaterno;
            objCliente.Cedula = txtdni;

            List<Cliente_Venta> cliente = objClienteNeg.findAllClientes(objCliente);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Seleccionar(int ID_Servicio)
        {
            Servicios objProducto = new Servicios(ID_Servicio);
            objProductoNeg.find(objProducto);
            return Json(objProducto, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PruebaJson()
        {  // escribir la url directa  para ver el formato      
            List<Producto> lista = objProductoNeg.findAll();
            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult Read()
        {
            var data = new Model.Neg.ProductoNeg().Read();
            var json = new { data };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GuardarVenta(string Fecha, int Descuento, string FechaF, 
            int Adelanto, string Hora_I, string Hora_F , string Observacion, 
            string Direccion, int ID_Empleado, int Id_Cliente, string Metodo, 
            float Efectivo, float Cheque, string Banco, string Concepto,
            string Total, List<DetalleFactura> ListadoDetalle)
        {
            
            //Evento(ID_Evento, ID_Cliente, ID_Empleado, Fecha_Evento) , List<DetalleVenta> ListadoDetalle
            //Factura(ID_Factura,Impuesto,Total)
            //Detalle_Factura(ID_Detalle,ID_Factura,ID_Evento,ID_Servicio,Cantidad,subtotal)                    
            int ID_Factura = 0;
            int ID_Evento = 0;
            int ID_Recibo = 0;
            int Subtotal = (int)float.Parse(Total);
            int Impuesto = ((int.Parse(Total) - Descuento) * 15) / 100;
            float total = (int.Parse(Total) - Descuento) + Impuesto;

            if (Fecha == "" || Id_Cliente == 0 || Total == "")
            {

            }
            else
            {
                //   total = Convert.ToDouble(Total);

                //REGISTRO DE VENTA
                Evento objEvento = new Evento(Id_Cliente, ID_Empleado, Fecha, Direccion, Observacion, Adelanto, Hora_I, Hora_F);
                ID_Evento = objEventoNeg.create(objEvento);
                if (ID_Evento == 0)
                {

                }
                else
                {
                    Session["id_Evento"] = ID_Evento;

                    Recibo objRecibo = new Recibo(FechaF, ID_Evento, Adelanto, Metodo, Efectivo, Cheque, Banco, Concepto);
                    ID_Recibo = objFacturaNeg.createR(objRecibo);
                   

                    //REGISTRO DE FACTURA
                    Factura objFactura = new Factura(FechaF, Subtotal, Impuesto, total, Descuento);
                    ID_Factura = objFacturaNeg.create(objFactura);

                    if (ID_Factura == 0)
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
                            new DetalleFactura(ID_Factura, ID_Evento, IdProducto, cantidad, subtotal);
                            objDetalleFacturaNeg.create(objDetalleVenta);
                        }
                    }
                }
            }
            Factura objFin = new Factura("h", 100,ID_Evento, ID_Factura, ID_Recibo);

            return Json(objFin);
        }      
    }
}