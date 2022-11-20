using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Cliente_Venta
    {
        private int id_Cliente;
        private string nombre;
        private string apellido;
        private string cedula;
        private string direccion;
        private string telefono;
        /*private List<Venta> ventas;*/
        private int estado;

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
        [Display(Name = "Codigo")]
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
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Nombre")]
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
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Apellido")]
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
        [Required(ErrorMessage = "Este Campo es Requerido")]
       
       
        [Display(Name = "Cedula")]
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
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Direccion")]
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
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Telefono")]
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

        public Cliente_Venta()
        {

        }
        public Cliente_Venta(int id_Cliente)
        {
            this.id_Cliente = id_Cliente;
        }

        public Cliente_Venta(int id_Cliente, string nombre, string apellido, string cedula, string direccion, string telefono)
        {
            this.id_Cliente = id_Cliente;
            this.nombre = nombre;
            this.apellido = apellido;
            this.cedula = cedula;
            this.direccion = direccion;
            this.telefono = telefono;
        }


    }
}
