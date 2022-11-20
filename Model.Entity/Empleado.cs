using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Empleado
    {
        private int id_empleado;
        private int id_cargo;
        private string nombre;
        private string apellido;
        private string cedula;
        private string direccion;
        private string telefono;
        private string cargo;

        public int ID_Empleado
        {
            get
            {
                return id_empleado;
            }

            set
            {
                id_empleado = value;
            }
        }

        public int ID_Cargo
        {
            get
            {
                return id_cargo;
            }

            set
            {
                id_cargo = value;
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

        public string Apellido
        {
            get
            {
                return apellido;
            }

            set
            {
                apellido = value;
            }
        }

        public string Cedula
        {
            get
            {
                return cedula;
            }

            set
            {
                cedula = value;
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

        public string Cargo
        {
            get
            {
                return cargo;
            }

            set
            {
                cargo = value;
            }
        }

        public Empleado ()
        {

        }
        public Empleado(int id_Empleado)
        {
            this.id_empleado = id_Empleado;
        }

        public Empleado(int id_empleado, string nombre, string apellido, string cedula, string direccion, string telefono)
        {
            this.id_empleado = id_empleado;
            this.nombre = nombre;
            this.apellido = apellido;
            this.cedula = cedula;
            this.direccion = direccion;
            this.telefono = telefono;
        }


    }
}
