using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;

namespace Model.Dao1
{
    public class DetalleFacturaDao
    {
        private ConexionDB objConexionDB;
        private SqlCommand comando;
        private SqlDataReader reader;
        public DetalleFacturaDao()
        {
            objConexionDB = ConexionDB.saberEstado();
        }

        public void create(DetalleFactura objDetalleVenta)
        {
            string create = "insert into Detalle_Factura (ID_Factura, ID_Evento, ID_Servicio, Cantidad, subtotal)" +
                "values(" + objDetalleVenta.ID_Factura + ", " + objDetalleVenta.ID_Evento + 
                ", " + objDetalleVenta.ID_Servicio + "," + objDetalleVenta.Cantidad 
                + "," + objDetalleVenta.SubTotal + ")";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                objDetalleVenta.Estado = 1;
            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }
        }

        public void createP(DetalleFactura objDetalleVenta)
        {
            string create = "insert into Detalle_Proforma(ID_Proforma, ID_Servicio,Cantidad, Subtotal)" +
                "values(" + objDetalleVenta.ID_Factura  +
                ", " + objDetalleVenta.ID_Servicio + "," + objDetalleVenta.Cantidad
                + "," + objDetalleVenta.SubTotal + ")";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {
                objDetalleVenta.Estado = 1;
            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }
        }

    }
}
