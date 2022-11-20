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
    public class ClienteDao : Obligatorio<Cliente_Venta>
    {
        private ConexionDB objConexinDB;
        private SqlCommand comando;
        public static string Cadena = "Data Source=DBSAFESAE.mssql.somee.com;Initial Catalog=DBSAFESAE;user id=Nikorasu14_SQLLogin_1; pwd=u99nazf91d";
        public string CadenaR = "Data Source=DBSAFESAE.mssql.somee.com;Initial Catalog=DBSAFESAE;user id=Nikorasu14_SQLLogin_1; pwd=u99nazf91d";


        private ConexionDB objConexionDB;
        private SqlDataReader reader;

        public ClienteDao()
        {
            objConexinDB = ConexionDB.saberEstado();
            objConexionDB = ConexionDB.saberEstado();
        }

        public static int AgregarR(Cliente_Venta oUsuario)
        {
            int ID_Cliente;
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Agregar_ClienteR", cn);
                cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                cmd.Parameters.AddWithValue("Apellido", oUsuario.Apellido);
                cmd.Parameters.AddWithValue("Cedula", oUsuario.Cedula);
                cmd.Parameters.AddWithValue("Telefono", oUsuario.Telefono);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                ID_Cliente = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            return ID_Cliente;
        }
        public static void Agregar(Cliente_Venta oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Agregar_Cliente", cn);
                cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                cmd.Parameters.AddWithValue("Apellido", oUsuario.Apellido);
                cmd.Parameters.AddWithValue("Cedula", oUsuario.Cedula);
                cmd.Parameters.AddWithValue("Telefono", oUsuario.Telefono);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void Eliminar(int oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Eliminar_Cliente", cn);
                cmd.Parameters.AddWithValue("ID_Cliente", oUsuario);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void Actualizar(Cliente_Venta oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Actualizar_Cliente", cn);
                cmd.Parameters.AddWithValue("ID_Cliente", oUsuario.ID_Cliente);
                cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                cmd.Parameters.AddWithValue("Apellido", oUsuario.Apellido);
                cmd.Parameters.AddWithValue("Cedula", oUsuario.Cedula);
                cmd.Parameters.AddWithValue("Telefono", oUsuario.Telefono);
              
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void create(Cliente_Venta objCliente)
        {
            string create = "Agregar_Cliente " + objCliente.Nombre + "," + objCliente.Apellido + "," + objCliente.Cedula + "," + objCliente.Telefono + "" ;
            try
            {
                comando = new SqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {
                objCliente.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

        }

        public void delete(Cliente_Venta objCliente)
        {
            string delete = "delete from cliente where idCliente='" + objCliente.ID_Cliente + "'";
            try
            {
                comando = new SqlCommand(delete, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                objCliente.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

        }

        public bool find(Cliente_Venta objCliente)
        {
            bool hayRegistros;
            string find = "select * from cliente where ID_Cliente ='" + objCliente.ID_Cliente + "'";
            try
            {
                comando = new SqlCommand(find, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                //bool hayRegistros;
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objCliente.Nombre = reader[1].ToString();
                    objCliente.Apellido = reader[2].ToString();
                    objCliente.Cedula = reader[3].ToString();
                    objCliente.Direccion = reader[4].ToString();
                    objCliente.Telefono = reader[5].ToString();                

                    objCliente.Estado = 99;

                }
                else
                {
                    objCliente.Estado = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return hayRegistros;
        }
        
        public List<Cliente_Venta> findAll()
        {
            List<Cliente_Venta> listaClientes = new List<Cliente_Venta>();
            string findAll = "Select * from Cliente ";
            try
            {
                comando = new SqlCommand(findAll, objConexinDB.getCon1());
                objConexinDB.getCon1().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Cliente_Venta objCliente = new Cliente_Venta();
                    objCliente.ID_Cliente = (int)Convert.ToInt64(reader[0].ToString());
                    objCliente.Cedula = reader[1].ToString();
                    objCliente.Nombre = reader[2].ToString();
                    objCliente.Apellido = reader[3].ToString();                  
                    objCliente.Telefono = reader[4].ToString();
                   
                    listaClientes.Add(objCliente);

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexinDB.getCon1().Close();
                objConexinDB.closeDB();
            }

            return listaClientes;

        }

        public Cliente_Venta Buscar(Cliente_Venta Ocliente)
        {
            Cliente_Venta objProducto = new Cliente_Venta();
            string find = "select ID_Cliente, CONCAT(Nombre, ' ', Apellido), Cedula" +
                " from Cliente where Cedula = '" + Ocliente.Cedula + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    objProducto.ID_Cliente = Convert.ToInt32(reader[0].ToString());
                    objProducto.Nombre = reader[1].ToString();
                    objProducto.Cedula = reader[2].ToString();
                }
            }
            catch (Exception)
            {
                Servicios objProducto2 = new Servicios();
                objProducto2.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }
            return objProducto;
        }

        public List<Cliente_Venta> findAllDeleted()
        {
            List<Cliente_Venta> listaClientes = new List<Cliente_Venta>();
            string findAll = "select * from Cliente_Eliminado ";
            try
            {
                comando = new SqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Cliente_Venta objCliente = new Cliente_Venta();
                    objCliente.Nombre = reader[1].ToString();
                    objCliente.Apellido = reader[2].ToString();
                    objCliente.Cedula = reader[3].ToString();
                    objCliente.Direccion = reader[4].ToString();
                    objCliente.Telefono = reader[5].ToString();

                    listaClientes.Add(objCliente);

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaClientes;

        }

        public void update(Cliente_Venta objCliente)
        {
            string update = "update Cliente set nombre='" + objCliente.Nombre + "',Apellido='" + objCliente.Apellido + "',Cedula='" + objCliente.Cedula + "',Direccion='" + objCliente.Direccion + "',Telefono='" + objCliente.Telefono + "' where ID_Cliente='" + objCliente.ID_Cliente + "'";
            try
            {
                comando = new SqlCommand(update, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                objCliente.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
        }

        public bool findClientePorDni(Cliente_Venta objCliente)
        {
            bool hayRegistros;
            string find = "select*from cliente where Cedula='" + objCliente.Cedula + "'";
            try
            {
                comando = new SqlCommand(find, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                //bool hayRegistros;
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objCliente.Nombre = reader[1].ToString();
                    objCliente.Apellido = reader[2].ToString();
                    objCliente.Cedula = reader[3].ToString();
                    objCliente.Direccion = reader[4].ToString();
                    objCliente.Telefono = reader[5].ToString();
                    

                    objCliente.Estado = 99;

                }
                else
                {
                    objCliente.Estado = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return hayRegistros;
        }

        public List<Cliente_Venta> findAllCliente(Cliente_Venta objCLiente)
        {
            List<Cliente_Venta> listaClientes = new List<Cliente_Venta>();
            //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
            string findAll = "select* from cliente where Nombre like '%" + objCLiente.Nombre + "%' or Cedula like '%" + objCLiente.Cedula + "%' or idCliente like '%" + objCLiente.ID_Cliente + "%' or apPaterno like '%" + objCLiente.Apellido + "%'";
            try
            {
                comando = new SqlCommand(findAll, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Cliente_Venta objCliente = new Cliente_Venta();
                    objCliente.ID_Cliente = (int)Convert.ToInt64(reader[0].ToString());
                    objCliente.Nombre = reader[1].ToString();
                    objCliente.Apellido = reader[2].ToString();
                    objCliente.Cedula = reader[3].ToString();
                    objCliente.Direccion = reader[4].ToString();
                    objCliente.Telefono = reader[5].ToString();
                    
                    listaClientes.Add(objCliente);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaClientes;

        }
    }
}
