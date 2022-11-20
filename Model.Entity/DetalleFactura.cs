using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class DetalleFactura
    {
        int id_detalleventa;
        private int id_Factura;
        private int id_Evento;
        private int id_Servicio;
        private int cantidad;
        private double subTotal;
        private int estado;

        public int ID_Factura
        {
            get
            {
                return id_Factura;
            }

            set
            {
                id_Factura = value;
            }
        }

        public int ID_Evento
        {
            get
            {
                return id_Evento;
            }

            set
            {
                id_Evento = value;
            }
        }
        public int ID_Servicio
        {
            get
            {
                return id_Servicio;
            }

            set
            {
                id_Servicio = value;
            }
        }
        public double SubTotal
        {
            get
            {
                return subTotal;
            }

            set
            {
                subTotal = value;
            }
        }

        public int Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }

        public int Cantidad
        {
            get
            {
                return cantidad;
            }

            set
            {
                cantidad = value;
            }
        }


        public DetalleFactura()
        {

        }
        public DetalleFactura(int idDetalleVenta)
        {
            this.id_detalleventa = idDetalleVenta;

        }
        public DetalleFactura(int numFacura, int idVenta, int idProducto, int cantidad, double subTotal)
        {
            this.id_Factura = numFacura;
            this.id_Evento = idVenta;
            this.id_Servicio = idProducto;
            this.subTotal = subTotal;
            this.cantidad = cantidad;
        }
    }
}
