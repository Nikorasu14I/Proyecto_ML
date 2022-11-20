using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Proforma
    {
        private int id_proforma;
        private string fecha;
        private float impuesto;
        private float total;
        private string nombre;
        private int id_cliente;
        private float subtotal;


        public int ID_Cliente
        {
            get
            {
                return id_cliente;
            }

            set
            {
                id_cliente = value;
            }
        }

        public int ID_Proforma
        {
            get
            {
                return id_proforma;
            }

            set
            {
                id_proforma = value;
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

        public float Impuesto
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

       
        public Proforma()
        {

        }

        public Proforma(string Fecha, float Subtotal,  float Impuesto, float Total)
        {
            this.fecha = Fecha;
            this.subtotal = Subtotal;
            this.impuesto = Impuesto;
            this.total = Total;
        }


    }
}
