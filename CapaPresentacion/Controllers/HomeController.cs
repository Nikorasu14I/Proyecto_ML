using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaPresentacion.Models;

namespace CapaPresentacion.Controllers
{
    public class HomeController : Controller
    {

        public string Cadena = "Data Source=DESKTOP-SLBULOH;Initial Catalog=ML_V;Integrated Security=true";


     

        public ActionResult Cliente()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cliente_D()
        {
            return View();
        }


        public ActionResult InicioRegistro()
        {
            return View();
        }



        [HttpPost]
        public ActionResult InicioRegistro(ClienteEvento oCliente)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Buscar_Cliente", cn);
                cmd.Parameters.AddWithValue("Cedula", oCliente.Cedula);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                /*Lee primera fila y primera columna*/
                oCliente.ID_Cliente = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }


            if (oCliente.ID_Cliente != 0)
            {
                Session["cliente"] = oCliente;
                ViewData["Mensaje"] = "Usuario o Contraseña Correcto";

                SqlConnection con = new SqlConnection(Cadena);
                con.Open();
                SqlCommand cm = new SqlCommand("select concat(Nombre_E, Apellido_E) from Cliente where ID_Empleado = " + oCliente.ID_Cliente, con);
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    oCliente.Nombre = dr.GetString(0);
                }
                return RedirectToAction("Registro", "Home", oCliente.Nombre);
            }
            else
            {
                return View();
            }

        }

        /*public ActionResult CerrarSesion()
        {
            
        }*/
    }
}