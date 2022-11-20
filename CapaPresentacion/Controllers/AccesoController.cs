using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

using CapaPresentacion.Models;
using Model.Neg;

namespace CapaPresentacion.Controllers
{
    public class AccesoController : Controller
    {
        private UsuarioNeg ObjUsuarioNeg;
        // GET: Acceso
        public AccesoController()
        {
            ObjUsuarioNeg = new UsuarioNeg();         
        }

        public ActionResult Inicio()
        {
            ViewData["Cliente"] = ObjUsuarioNeg.ReadC();
            ViewData["Evento"] = ObjUsuarioNeg.ReadE();
            ViewData["Almacen"] = ObjUsuarioNeg.ReadA();
            ViewData["Transporte"] = ObjUsuarioNeg.ReadT();
            return View();
        }

        public ActionResult Login()
        {            
            return View();
        }
     
        [HttpPost]
        public ActionResult InicioS(Model.Entity.Usuario oUsuario)
        {           
            int ID = ObjUsuarioNeg.Read(oUsuario);

            if (ID != 0)
            {               
                Session["User"] = oUsuario;
                //SqlConnection con = new SqlConnection(Cadena);
                //con.Open();
                //SqlCommand cm = new SqlCommand("select concat(Nombre_E, Apellido_E) from Empleado where ID_Empleado = " + oUsuario.ID_Empleado, con);
                //SqlDataReader dr = cm.ExecuteReader();
                //while (dr.Read())
                //{
                //    string NombreUsuario = dr.GetString(0);
                //}
                return Json(ID);
            }
            else
            {
                return Json(ID);
            }
        }
    }
}

