using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;


namespace Model.Dao1
{
    public class FacturaDao
    {
        private ConexionDB objConexionDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public FacturaDao()
        {
            objConexionDB = ConexionDB.saberEstado();
        }

        public int create(Factura objFactura)
        {
            int idFactura = 0;
            string create = "insert into Factura(Fecha_Factura, Impuesto, Total, Descuento, Subtotal) values( '" +
                objFactura.Fecha + "' , " + objFactura.Impuesto + ", " + objFactura.Total +
                ", " + objFactura.Descuento + ", " + objFactura.Subtotal
                + ") SELECT SCOPE_IDENTITY();";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();

                //RECUPERAR EL CODIGO AUTOGENERADO
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idFactura = Convert.ToInt32(reader[0].ToString());
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
            return idFactura;
        }

        public int BuscarC(int ID)
        {
            int idEvento = 0;
            string create = "select * from FE where ID_Factura = " + ID ;
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();

                //RECUPERAR EL CODIGO AUTOGENERADO
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idEvento = Convert.ToInt32(reader[0].ToString());
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
            return idEvento;
        }


        public int createR(Recibo objFactura)
        {
            int idrecibo = 0;
            string create = "insert into Recibo(Fecha, ID_Evento, Forma_Pago, Monto, Efectivo, Cheque, Banco, Concepto) values( '" +
                objFactura.Fecha + "' , " + objFactura.ID_Evento + ", '" + objFactura.Forma +
                "', " + objFactura.Monto + ", " + objFactura.Efectivo + ", " + objFactura.Cheque 
                + ", '" + objFactura.Banco + "', '" + objFactura.Concepto
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


        public int createP(Proforma objFactura)
        {
            int idFactura = 0;
            string create = "insert into Proforma(Fecha, Impuesto, Total, Subtotal) values( '" +
                objFactura.Fecha + "' , " + objFactura.Impuesto + ", " + objFactura.Total
                + ", " + objFactura.Subtotal
                + ") SELECT SCOPE_IDENTITY();";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();

                //RECUPERAR EL CODIGO AUTOGENERADO
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idFactura = Convert.ToInt32(reader[0].ToString());
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
            return idFactura;
        }

        public Factura VistaF(int ID)
        {
            Factura objProducto = new Factura();
            string find = "select * from VistaF where ID_Factura = " + ID;
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    objProducto.ID_Factura = Convert.ToInt32(reader[0].ToString());
                    objProducto.Nombre = reader[1].ToString();
                    objProducto.Fecha = reader[2].ToString();
                    objProducto.Descuento = Convert.ToInt32(reader[3].ToString());
                    objProducto.Impuesto = Convert.ToInt32(reader[4].ToString());
                    objProducto.Total = Convert.ToSingle(reader[5].ToString());
                    objProducto.Direccion = reader[6].ToString();
                    objProducto.Subtotal = Convert.ToSingle(reader[7].ToString());
                    objProducto.Telefono = reader[8].ToString();
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

        public Recibo VistaR(int ID)
        {
            Recibo objProducto = new Recibo();
            string find = "select * from ReciboR where ID_Recibo = " + ID;
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    objProducto.Nombre = reader[1].ToString();
                    objProducto.Fecha = reader[2].ToString();
                    objProducto.Forma = reader[3].ToString();
                    objProducto.Monto = Convert.ToSingle(reader[4].ToString());
                    objProducto.Efectivo = Convert.ToSingle(reader[5].ToString());
                    objProducto.Cheque = Convert.ToSingle(reader[6].ToString());
                    objProducto.Banco = reader[7].ToString();
                    objProducto.Concepto = reader[8].ToString();
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


        public Proforma VistaP(int ID)
        {
            Proforma objProducto = new Proforma();
            string find = "select * from ProformaV where ID_Proforma = " + ID;
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    objProducto.Fecha = reader[0].ToString();
                    objProducto.Total = Convert.ToSingle(reader[2].ToString());
                    objProducto.Impuesto = Convert.ToInt32(reader[1].ToString());
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

        public List<Servicios> Detalle(int id)
        {
            List<Servicios> listaProductos = new List<Servicios>();
            string find = "exec SP_DetalleP " + id ;
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Servicios objProducto = new Servicios();
                    objProducto.Descripcion = reader[0].ToString();
                    objProducto.Precio = Convert.ToInt32(reader[1].ToString());
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

        public List<Servicios> DetalleF(int id)
        {
            List<Servicios> listaProductos = new List<Servicios>();
            string find = "exec SP_DetalleF " + id;
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Servicios objProducto = new Servicios();
                    objProducto.Descripcion = reader[1].ToString();
                    objProducto.Precio = Convert.ToInt32(reader[2].ToString());
                    objProducto.Cantidad = Convert.ToInt32(reader[3].ToString());
                    objProducto.ID_Servicio = Convert.ToInt32(reader[4].ToString());

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


    }
}
