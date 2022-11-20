using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Factura
    {
        private int id_Factura;
        private string fecha;
        private int impuesto;
        private float total;
        private float subtotal;
        private string nombre;
        private string telefono;
        private string direccion;
        private int descuento;
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

        public string Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public string Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }

        public int Impuesto
        {
            get
            {
                return impuesto;
            }

            set
            {
                impuesto = value;
            }
        }

        public float Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
            }
        }

        public float Subtotal
        {
            get
            {
                return subtotal;
            }

            set
            {
                subtotal = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }

        public int Descuento
        {
            get
            {
                return descuento;
            }

            set
            {
                descuento = value;
            }
        }

        public Factura()
        {

        }

        public Factura(string Fecha_Factura, float Subtotal, int Impuesto, float Total, int Descuento)
        {
            this.impuesto = Impuesto;
            this.fecha = Fecha_Factura;
            this.total = Total;
            this.descuento = Descuento;
            this.subtotal = Subtotal;

        }


    }
}
