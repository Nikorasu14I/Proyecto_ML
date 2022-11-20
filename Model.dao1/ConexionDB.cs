using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao1
{
    public class ConexionDB
    {

        private static ConexionDB objConexionDB = null;
        private System.Data.SqlClient.SqlConnection con;
        private System.Data.SqlClient.SqlConnection con1;

        private ConexionDB()
        {
            con1 = new SqlConnection("Data Source=DBSAFESAE.mssql.somee.com;Initial Catalog=DBSAFESAE;user id=Nikorasu14_SQLLogin_1; pwd=u99nazf91d");
        }

        public static ConexionDB saberEstado()
        {
            if (objConexionDB == null)
            {
                objConexionDB = new ConexionDB();

            }
            return objConexionDB;
        }

        public SqlConnection getCon()
        {
            return con;
        }

        public SqlConnection getCon1()
        {
            return con1;
        }

        public void closeDB()
        {
            objConexionDB = null;
        }

    }
}
