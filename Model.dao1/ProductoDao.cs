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
    public class ProductoDao 
    {
        private ConexionDB objConexionDB;
        private SqlCommand comando;
        private SqlDataReader reader;
        public ProductoDao()
        {
            objConexionDB = ConexionDB.saberEstado();
        }

        public static string Cadena = ("Data Source=DBSAFESAE.mssql.somee.com;Initial Catalog=DBSAFESAE;user id=Nikorasu14_SQLLogin_1; pwd=u99nazf91d");

        /*
        public List<Model.Entity.Producto> Read()
        {
            var positions = new List<Model.Entity.Producto>();

            try
            {
                using (var connection = new SqlConnection(Connection.value))
                {
                    // Configurar Consulta
                    var cmd = new SqlCommand()
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = _commandText,
                        Connection = connection
                    };

                    // Establecer Parametros
                    cmd.Parameters.AddWithValue("Operation", "R");

                    cmd.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Message", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                    // Abrir Conexión
                    connection.Open();

                    // Ejecutar la Consulta
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Leer cada fila de la tabla
                        while (reader.Read())
                        {
                            // Añadir Cada Elemento a La Lista
                            positions.Add(new Model.Entity.Producto()
                            {
                                ID_Servicio = Convert.ToInt32(reader["ID_Servicio"]),
                                Descripcion = Convert.ToString(reader["Descripcion"]),
                                Active = Convert.ToBoolean(reader["Active"])
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return positions;
        }

        */
        public int AgregarR(Recibo objFactura)
        {
            int idrecibo = 0;
            string create = "insert into Recibo(Fecha, ID_Evento, Forma_Pago, Monto, Efectivo, Cheque, Banco, Concepto) values( '" +
                objFactura.Fecha + "' , " + objFactura.ID_Evento + ", '" + objFactura.Forma +
                "', " + objFactura.Monto + ", " + objFactura.Efectivo + ", " + objFactura.Cheque + ", '" 
                + objFactura.Banco + "', '" + objFactura.Concepto
                + "') SELECT SCOPE_IDENTITY();";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();

                //RECUPERAR EL CODIGO AUTOGENERADO
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idrecibo = Convert.ToInt32(reader[0].ToString());
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
            return idrecibo;
        }


        public static void Agregar(Servicios OServicio)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Agregar_Servicio", cn);
                cmd.Parameters.AddWithValue("Descripcion", OServicio.Descripcion);
                cmd.Parameters.AddWithValue("Precio", OServicio.Precio);
                cmd.Parameters.AddWithValue("ID_Categoria", OServicio.Estado);
                cmd.Parameters.AddWithValue("Cantidad", OServicio.Cantidad);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Actualizar(Servicios OServicio)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Actualizar_Producto", cn);
                cmd.Parameters.AddWithValue("ID_Servicio", OServicio.ID_Servicio);
                cmd.Parameters.AddWithValue("Descripcion", OServicio.Descripcion);
                cmd.Parameters.AddWithValue("Precio", OServicio.Precio);
                cmd.Parameters.AddWithValue("Cantidad", OServicio.Cantidad);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static void Eliminar(int OServicio)
        {
            using (SqlConnection cn = new SqlConnection(Cadena))
            {
                SqlCommand cmd = new SqlCommand("Eliminar_Servicio", cn);
                cmd.Parameters.AddWithValue("ID_Servicio", OServicio);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
     
        public bool find(Servicios objProducto)
        {
            bool hayRegistros;
            string find = " select * from Servicio where ID_Servicio = " + objProducto.ID_Servicio + " ";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objProducto.Descripcion = reader[2].ToString();
                    objProducto.Precio = Convert.ToSingle(reader[4].ToString());
                }
                else
                {
                    objProducto.Estado = 1;
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

            return hayRegistros;
        }

        public List<Servicios> Detalle(int id)
        {
            List<Servicios> listaProductos = new List<Servicios>();
            string find = "exec SP_Detalle '" + id + "'" ;
            try 
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Servicios objProducto = new Servicios();
                    objProducto.Descripcion = reader[0].ToString();
                    objProducto.Precio = Convert.ToSingle(reader[1].ToString());
                    objProducto.Cantidad = Convert.ToInt32(reader[2].ToString());
                    objProducto.ID_Servicio = Convert.ToInt32(reader[3].ToString());
                    listaProductos.Add(objProducto);
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
            return listaProductos;
        }

        public List<Servicios> Disponible(string FechaE)
        {
            List<Servicios> listaProductos = new List<Servicios>();
            string find = "exec SP_Disponible '" + FechaE + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Servicios objProducto = new Servicios();
                    objProducto.ID_Servicio = Convert.ToInt32(reader[0].ToString());
                    objProducto.Descripcion = reader[1].ToString();
                    objProducto.Precio = Convert.ToSingle(reader[2].ToString());
                    objProducto.Categoria = reader[3].ToString();
                    objProducto.Cantidad = Convert.ToInt32(reader[4].ToString());
                    listaProductos.Add(objProducto);
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
            return listaProductos;
        }

        public List<Servicios> findAllVistaF()
        {
            List<Servicios> listaProductos = new List<Servicios>();
            string find = "select * from View_Factura";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Servicios objProducto = new Servicios();
                    objProducto.ID_Servicio = Convert.ToInt32(reader[0].ToString());
                    objProducto.Descripcion = reader[1].ToString();
                    objProducto.Precio = Convert.ToSingle(reader[3].ToString());
                    objProducto.Categoria = reader[2].ToString();
                    listaProductos.Add(objProducto);
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
            return listaProductos;
        }

        public List<Servicios> findAllVistaD()
        {
            List<Servicios> listaProductos = new List<Servicios>();
            string find = "select * from Debido";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Servicios objProducto = new Servicios();
                    objProducto.ID_Servicio = Convert.ToInt32(reader[0].ToString());
                    objProducto.Descripcion = reader[1].ToString();
                    objProducto.Cantidad = Convert.ToInt32(reader[2].ToString());
                    objProducto.Estado = Convert.ToInt32(reader[3].ToString());
                    objProducto.Precio = Convert.ToSingle(reader[4].ToString());
                    objProducto.Categoria = reader[5].ToString();

                    listaProductos.Add(objProducto);
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
            return listaProductos;
        }

        public List<Recibo> findAllRecibos(int ID)
        {
            List<Recibo> lista = new List<Recibo>();
            string find = "select * from Recibos where ID_Evento = " + ID;
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Recibo objProducto = new Recibo();
                    objProducto.ID_Recibo = Convert.ToInt32(reader[0].ToString());
                    objProducto.Fecha = reader[1].ToString();
                    objProducto.Nombre = reader[2].ToString();
                    objProducto.Forma = reader[3].ToString();
                    objProducto.Monto = Convert.ToSingle(reader[4].ToString());                 

                    lista.Add(objProducto);
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
            return lista;
        }


        public List<Servicios> findAllVista()
        {
            List<Servicios> listaProductos = new List<Servicios>();
            string find = "select * from VistaAlmacen";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Servicios objProducto = new Servicios();
                    objProducto.ID_Servicio = Convert.ToInt32(reader[0].ToString());
                    objProducto.Descripcion = reader[1].ToString();
                    objProducto.Precio = Convert.ToSingle(reader[2].ToString());
                    objProducto.Categoria = reader[3].ToString();
                    objProducto.Cantidad = Convert.ToInt32(reader[4].ToString());

                    listaProductos.Add(objProducto);
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
            return listaProductos;
        }

        public List<Producto> findAll()
        {
            List<Producto> listaProductos = new List<Producto>();
            string find = "select * from Categoria";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Producto objProducto = new Producto();
                    objProducto.ID_Servicio = Convert.ToInt32(reader[0].ToString());
                    objProducto.ID_Categoria = Convert.ToInt32(reader[1].ToString());   
                    objProducto.Descripcion = reader[2].ToString();
                    objProducto.Precio = Convert.ToSingle(reader[3].ToString());
                    objProducto.ID_Clase = Convert.ToInt32(reader[4].ToString());

                    listaProductos.Add(objProducto);
                }

            }
            catch (Exception)
            {
                Producto objProducto2 = new Producto();
                objProducto2.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }

            return listaProductos;
        }

        public List<Producto> findAllCategoria()
        {
            List<Producto> listaProductos = new List<Producto>();
            string find = "select * from Categoria where ID_Categoria != 4";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Producto objProducto = new Producto();
                    objProducto.ID_Servicio = Convert.ToInt32(reader[0].ToString());
                    objProducto.Descripcion = reader[1].ToString();

                    listaProductos.Add(objProducto);
                }

            }
            catch (Exception)
            {
                Producto objProducto2 = new Producto();
                objProducto2.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }

            return listaProductos;
        }


        public List<Servicios> findAllVistaT()
        {
            List<Servicios> listaProductos = new List<Servicios>();
            string find = "select * from VistaTransporte";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Servicios objProducto = new Servicios();
                    objProducto.ID_Servicio = Convert.ToInt32(reader[0].ToString());
                    objProducto.Descripcion = reader[1].ToString();
                    objProducto.Precio = Convert.ToSingle(reader[2].ToString());
                    objProducto.Categoria = reader[3].ToString();
                    objProducto.Cantidad = Convert.ToInt32(reader[4].ToString());

                    listaProductos.Add(objProducto);
                }

            }
            catch (Exception)
            {
                Producto objProducto2 = new Producto();
                objProducto2.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }

            return listaProductos;
        }


        public void update(Producto objProducto)
        {
            
        }

        public bool findProductoPorCategoriaId(Producto objProducto)
        {
            bool hayRegistros;
            string find = "select*from producto where ID_Clase='" + objProducto.ID_Clase + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objProducto.Estado = 99;
                }
                else
                {
                    objProducto.Estado = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
            return hayRegistros;
        }

        public List<Producto> findAllProductos(Producto objProducto)
        {
            List<Producto> listaProductos = new List<Producto>();
            //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
            string findAll = "select* from Servicio where Descripcion like '%" + objProducto.Descripcion + "%' or ID_Clase like '%" + objProducto.ID_Clase + "%'";
            try
            {
                comando = new SqlCommand(findAll, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Producto objProductos = new Producto();
                    objProductos.ID_Servicio = Convert.ToInt32(reader[0].ToString());
                    objProductos.Descripcion = reader[1].ToString();
                    objProductos.Precio = Convert.ToSingle(reader[2].ToString());
                    objProductos.ID_Categoria = Convert.ToInt32(reader[3].ToString());
                    objProductos.ID_Clase = Convert.ToInt32(reader[4].ToString());
                    listaProductos.Add(objProductos);
                }
            }
            catch (Exception)
            {

                objProducto.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return listaProductos;

        }
        public List<Producto> findAllProductosPorCategoria(Producto objProducto)
        {
            List<Producto> listaProductos = new List<Producto>();
            string findAll = "select * from Servicio where ID_Categoria='" + objProducto.ID_Categoria + "'";

            //string findAll = "sp_producto_categoria " + objProducto.Categoria;
            try
            {
                comando = new SqlCommand(findAll, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Producto objProductos = new Producto();
                    objProductos.ID_Servicio = Convert.ToInt32(reader[0].ToString());
                    objProductos.Descripcion = reader[1].ToString();
                    objProductos.Precio = Convert.ToSingle(reader[2].ToString());
                    objProductos.ID_Categoria = Convert.ToInt32(reader[3].ToString());
                    objProductos.ID_Clase = Convert.ToInt32(reader[4].ToString());
                    listaProductos.Add(objProductos);

                }
            }
            catch (Exception)
            {

                objProducto.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return listaProductos;

        }

        public List<Producto> findPrecioProducto(Producto objProducto)
        {
            List<Producto> listaServicio = new List<Producto>();
            string find = "select * from Servicio where ID_Servicio='" + objProducto.ID_Servicio + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                { 
                    objProducto.ID_Servicio = Convert.ToInt32(reader[0].ToString());
                    objProducto.Descripcion = reader[1].ToString();
                    objProducto.Precio = Convert.ToSingle(reader[2].ToString());
                    objProducto.ID_Categoria = Convert.ToInt32(reader[3].ToString());
                    objProducto.ID_Clase = Convert.ToInt32(reader[4].ToString());
                    listaServicio.Add(objProducto);
                }
            }
            catch (Exception)
            {
                Producto objProducto2 = new Producto();
                objProducto2.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return listaServicio;
        }

    }
}
