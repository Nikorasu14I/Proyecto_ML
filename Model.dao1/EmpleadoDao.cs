using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;

namespace Model.Dao1
{
    public class EmpleadoDao
    {
        private ConexionDB objConexionDB;
        private SqlCommand comando;
        public static string Cadena = "Data Source=DBSAFESAE.mssql.somee.com;Initial Catalog=DBSAFESAE;user id=Nikorasu14_SQLLogin_1; pwd=u99nazf91d";


        public EmpleadoDao()
        {
            objConexionDB = ConexionDB.saberEstado();
        }

        public static void Agregar(Usuario oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Agregar_Usu", cn);
                cmd.Parameters.AddWithValue("IDU", oUsuario.ID_Empleado);
                cmd.Parameters.AddWithValue("User", oUsuario.User);
                cmd.Parameters.AddWithValue("Pass", oUsuario.Pass);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void AgregarE(Empleado oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Agregar_Empleado", cn);
                cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                cmd.Parameters.AddWithValue("Apellido", oUsuario.Apellido);
                cmd.Parameters.AddWithValue("Cedula", oUsuario.Cedula);
                cmd.Parameters.AddWithValue("ID_Cargo", oUsuario.ID_Cargo);
                cmd.Parameters.AddWithValue("Telefono", oUsuario.Telefono);
                cmd.Parameters.AddWithValue("Direccion", oUsuario.Direccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void ActualizarE(Empleado oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Actualizar_Empleado", cn);
                cmd.Parameters.AddWithValue("ID_Empleado", oUsuario.ID_Empleado);
                cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                cmd.Parameters.AddWithValue("Apellido", oUsuario.Apellido);
                cmd.Parameters.AddWithValue("Cedula", oUsuario.Cedula);
                cmd.Parameters.AddWithValue("Telefono", oUsuario.Telefono);
                cmd.Parameters.AddWithValue("Direccion", oUsuario.Direccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void EliminarU(int oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Eliminar_Usuario", cn);
                cmd.Parameters.AddWithValue("ID_Usuario", oUsuario);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public List<Usuario> findAllU()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            string findAll = "select * from ViewUsuario ";
            try
            {
                comando = new SqlCommand(findAll, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Usuario objCliente = new Usuario();
                    objCliente.ID_Usuario = Convert.ToInt32(reader[0].ToString());
                    objCliente.Pass = reader[1].ToString();
                    objCliente.Cargo = reader[2].ToString();
                    objCliente.User = reader[3].ToString();

                    listaUsuarios.Add(objCliente);

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }

            return listaUsuarios;
        }

        public List<Empleado> findAll()
        {
            List<Empleado> listaUsuarios = new List<Empleado>();
            string findAll = "select * from EmpleadosV ";
            try
            {
                comando = new SqlCommand(findAll, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Empleado objCliente = new Empleado();
                    objCliente.ID_Empleado = Convert.ToInt32(reader[0].ToString());
                    objCliente.Nombre = reader[1].ToString();
                    objCliente.Apellido = reader[2].ToString();
                    objCliente.Cedula = reader[3].ToString();
                    objCliente.Cargo = reader[4].ToString();
                    objCliente.Telefono = reader[5].ToString();
                    objCliente.Direccion = reader[6].ToString();
                    listaUsuarios.Add(objCliente);

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }

            return listaUsuarios;
        }

        public List<Empleado> findAllE()
        {
            List<Empleado> listaUsuarios = new List<Empleado>();
            string findAll = "select E.ID_Empleado, CONCAT(E.Nombre_E, ' ', E.Apellido_E), C.Nombre_Cargo from Empleado E " +
                "inner join Cargo C on C.ID_Cargo = E.ID_Cargo where not exists (select 1 from Usuario " +
                "where Usuario.ID_Empleado = E.ID_Empleado) and (C.ID_Cargo = 1 or C.ID_Cargo = 4)";
            try
            {
                comando = new SqlCommand(findAll, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Empleado objCliente = new Empleado();
                    objCliente.ID_Empleado = Convert.ToInt32(reader[0].ToString());
                    objCliente.Nombre = reader[1].ToString();
                    objCliente.Apellido = reader[2].ToString();

                    listaUsuarios.Add(objCliente);

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }

            return listaUsuarios;
        }

        public int Read(Usuario oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Inicio_Sesion", cn);
                cmd.Parameters.AddWithValue("Usuario", oUsuario.User);
                cmd.Parameters.AddWithValue("Contraseña", oUsuario.Pass);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                oUsuario.ID_Empleado = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            return oUsuario.ID_Empleado;
        }

        public int ReadC()
        {
            int idevento = 0;
            string create = " select count(ID_Cliente) from Cliente ";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();

                //RECUPERAR EL CODIGO AUTOGENERADO
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idevento = Convert.ToInt32(reader[0].ToString());
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }
            return idevento;

        }

        public int ReadE()
        {
            int idevento = 0;
            string create = " select count(ID_Evento) from Evento ";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();

                //RECUPERAR EL CODIGO AUTOGENERADO
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idevento = Convert.ToInt32(reader[0].ToString());
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }
            return idevento;

        }

        public int ReadA()
        {
            int idevento = 0;
            string create = " select count(ID_Servicio) from VistaAlmacen ";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();

                //RECUPERAR EL CODIGO AUTOGENERADO
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idevento = Convert.ToInt32(reader[0].ToString());
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }
            return idevento;

        }

        public int ReadT()
        {
            int idevento = 0;
            string create = " select count(ID_Servicio) from VistaTransporte ";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();

                //RECUPERAR EL CODIGO AUTOGENERADO
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idevento = Convert.ToInt32(reader[0].ToString());
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }
            return idevento;

        }


    }
}