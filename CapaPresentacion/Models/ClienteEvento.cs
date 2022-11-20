using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapaPresentacion.Models
{
    public class ClienteEvento
    {
        public int ID_Cliente { get; set; }
        
        public string Nombre { get; set; }

        public string Cedula { get; set; }

        public string Telefono { get; set; }

        public string Direccion{ get; set; }

    }
}