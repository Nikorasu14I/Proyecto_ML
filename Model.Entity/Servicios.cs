using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Servicios
    {
        private int id_servicio;
        private string descripcion;
        private float precio;
        private string categoria;
        private int cantidad;

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

        public int ID_Servicio
        {
            get
            {
                return id_servicio;
            }

            set
            {
                id_servicio = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public float Precio
        {
            get
            {
                return precio;
            }

            set
            {
                precio = value;
            }
        }

        public string Categoria
        {
            get
            {
                return categoria;
            }

            set
            {
                categoria = value;
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

        public Servicios()
        {

        }
        public Servicios(int idservicio)
        {
            this.id_servicio = idservicio;
        }

        public Servicios(int id_servicio, string descripcion, int precio, string categoria, int cantidad)
        {
            this.ID_Servicio = id_servicio;
            this.Descripcion = descripcion;
            this.Precio = precio;
            this.Categoria = categoria;
            this.Cantidad = cantidad;
        }

    }
}
