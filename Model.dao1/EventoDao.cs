using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;

namespace Model.Dao1

{
    public class EventoDao
    {
        private ConexionDB objConexionDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public EventoDao()
        {
            objConexionDB = ConexionDB.saberEstado();
        }

        public int create(Evento objVenta)
        {
            int idevento = 0;
            string create = "insert into Evento(ID_Cliente, ID_Empleado,Fecha_Evento, Direccion, Observaciones, Hora_Inicio, Hora_Fin, Adelanto) values(" +
                objVenta.ID_Cliente + ", " + objVenta.ID_Empleado + ", '" + objVenta.Fecha + "', '" + objVenta.Direccion
                + "', '" + objVenta.Observaciones + "', '" + objVenta.HoraI + "', '" + objVenta.HoraF + "', " + objVenta.Adelanto
                + ") SELECT SCOPE_IDENTITY();";
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

        public Evento VistaC(int ID)
        {
            Evento objProducto = new Evento();
            string find = "select * from Contrato where ID_Evento = " + ID;
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon1());
                objConexionDB.getCon1().Open();
                reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    objProducto.Nombre = reader[1].ToString();
                    objProducto.Fecha = reader[2].ToString();
                    objProducto.Direccion = reader[3].ToString();
                    objProducto.Adelanto = Convert.ToSingle(reader[4].ToString());
                    objProducto.HoraI = reader[5].ToString();
                    objProducto.HoraF = reader[6].ToString();
                    objProducto.Observaciones = reader[7].ToString();
                    objProducto.ID_Cliente = Convert.ToInt32(reader[8].ToString());
                    objProducto.FechaF = reader[9].ToString();
                }
            }
            catch (Exception)
            {
                Servicios objProducto2 = new Servicios();
            }
            finally
            {
                objConexionDB.getCon1().Close();
                objConexionDB.closeDB();
            }
            return objProducto;
        }

    }
}
