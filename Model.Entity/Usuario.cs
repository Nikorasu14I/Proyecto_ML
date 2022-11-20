using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Usuario
    {
        private int id_usuario;
        private int id_empleado;
        private string user;
        private string pass;
        private string cargo;

        public int ID_Usuario
        {
            get
            {
                return id_usuario;
            }

            set
            {
                id_usuario = value;
            }
        }

        public int ID_Empleado
        {
            get
            {
                return id_empleado;
            }

            set
            {
                id_empleado = value;
            }
        }

        public string User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public string Cargo
        {
            get
            {
                return cargo;
            }

            set
            {
                cargo = value;
            }
        }

        public string Pass
        {
            get
            {
                return pass;
            }

            set
            {
                pass = value;
            }
        }

        public Usuario()
        {

        }
        public Usuario(int id_empleado)
        {
            this.id_empleado = id_empleado;
        }

        public Usuario(int id_empleado, string user, string pass, string cargo)
        {
            this.id_empleado = id_empleado;
            this.user = user;
            this.pass = pass;
            this.cargo = cargo;
        }

    }
}
