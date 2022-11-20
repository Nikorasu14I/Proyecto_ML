using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Producto
    {
        private int id_servicio;
        private string descripcion;
        private float precio;
        private int id_categoria;
        private int id_clase;
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

        public int ID_Categoria
        {
            get
            {
                return id_categoria;
            }

            set
            {
                id_categoria = value;
            }
        }

        public int ID_Clase
        {
            get
            {
                return id_clase;
            }

            set
            {
                id_clase = value;
            }
        }
        public Producto()
        {

        }
        public Producto(int idservicio)
        {
            this.id_servicio = idservicio;
        }
        public Producto(int id_servicio, string descripcion, int precio, int id_categoria, int id_clase)
        {
            this.ID_Servicio = id_servicio;
            this.Descripcion = descripcion;
            this.Precio = precio;
            this.ID_Categoria = id_categoria;
            this.ID_Categoria = id_clase;
        }

    }
}
