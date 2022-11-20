using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Evento
    {
        private int id_Evento;

        private int id_Cliente;
        private int id_Empleado;
        private string direccion;
        private string fecha;  
        private string observaciones;
        private float adelanto;
        private string horai;
        private string horaf;
        private string nombre;
        private string fechaf;
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

        public int ID_Cliente
        {
            get
            {
                return id_Cliente;
            }

            set
            {
                id_Cliente = value;
            }
        }

        public int ID_Empleado
        {
            get
            {
                return id_Empleado;
            }

            set
            {
                id_Empleado = value;
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

        public string FechaF
        {
            get
            {
                return fechaf;
            }

            set
            {
                fechaf = value;
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


        public string HoraI
        {
            get
            {
                return horai;
            }

            set
            {
                horai = value;
            }
        }

        public string HoraF
        {
            get
            {
                return horaf;
            }

            set
            {
                horaf = value;
            }
        }

        public float Adelanto
        {
            get
            {
                return adelanto;
            }

            set
            {
                adelanto = value;
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

        public string Observaciones
        {
            get
            {
                return observaciones;
            }

            set
            {
                observaciones = value;
            }
        }

        public Evento()
        {

        }


        public Evento(int ID_Cliente, int ID_Empleado, string Fecha, string Direccion, string Observacion, float Adelanto, string HoraI, string HoraF)
        {          
            this.id_Cliente = ID_Cliente;
            this.id_Empleado = ID_Empleado;
            this.fecha = Fecha;
            this.direccion = Direccion;
            this.observaciones = Observacion;
            this.adelanto = Adelanto;
            this.horai = HoraI;
            this.horaf = HoraF;
        }
    }
}
