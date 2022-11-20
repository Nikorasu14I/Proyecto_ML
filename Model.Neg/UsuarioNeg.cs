using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dao1;
using Model.Entity;

namespace Model.Neg
{
    public class UsuarioNeg
    {
        private readonly EmpleadoDao _Db = new EmpleadoDao();

        public List<Model.Entity.Empleado> listarE()
        {
            return _Db.findAll();
        }

        public List<Model.Entity.Empleado> listarER()
        {
            return _Db.findAllE();
        }

        public static void AgregarE(Empleado Ocliente)
        {
            EmpleadoDao.AgregarE(Ocliente);
        }
        public static void Agregar(Usuario OUsuario)
        {
            EmpleadoDao.Agregar(OUsuario);
        }

        public static void ActualizarE(Empleado Ocliente)
        {
            EmpleadoDao.ActualizarE(Ocliente);
        }


        public List<Model.Entity.Usuario> listar()
        {
            return _Db.findAllU();
        }

        public static void EliminarU(int Ocliente)
        {
            EmpleadoDao.EliminarU(Ocliente);
        }

        public int Read(Usuario OUsuario)
        {
            return _Db.Read(OUsuario);
        }

        public int ReadC()
        {
            return _Db.ReadC();
        }
        public int ReadE()
        {
            return _Db.ReadE();
        }
        public int ReadA()
        {
            return _Db.ReadA();
        }
        public int ReadT()
        {
            return _Db.ReadT();
        }
    }
}
