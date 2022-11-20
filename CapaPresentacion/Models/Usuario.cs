using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapaPresentacion.Models
{
    public class Usuario
    {
        public int ID_Empleado { get; set; }
        public string User { get; set; }
        public string Contraseña { get; set; }
        public string NombreUsuario { get; set; }

        public string Confirmar_Contraseña{ get; set; }

    }
}