using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dao1;
using Model.Entity;

namespace Model.Neg
{
    public class ClienteNeg

    {
        private ClienteDao objClienteDao;
        private VentaDao objVentaDao;
        private readonly ClienteDao _Db = new ClienteDao();
        public ClienteNeg()
        {
            objClienteDao = new ClienteDao();
            objVentaDao = new VentaDao();
        }

        public List<Model.Entity.Cliente_Venta> ReadC()
        {
            return _Db.findAll();
        }

        public List<Model.Entity.Cliente_Venta> Read()
        {
            return _Db.findAllDeleted();
        }

        public Cliente_Venta Buscar(Cliente_Venta Ocliente)
        {
            return _Db.Buscar(Ocliente);
        }

        public static void Agregar(Cliente_Venta Ocliente)
        {
            ClienteDao.Agregar(Ocliente);
        }
        public static int AgregarR(Cliente_Venta Ocliente)
        {
            return ClienteDao.AgregarR(Ocliente);
        }

        public static void Actualizar(Cliente_Venta Ocliente)
        {
            ClienteDao.Actualizar(Ocliente);
        }

        public static void Eliminar(int Ocliente)
        {
            ClienteDao.Eliminar(Ocliente);
        }

        public void create(Cliente_Venta objCliente)
        {
            bool verificacion = true;
            ////begin validar codigo retorna estado=1
            //string codigo = objCliente.IdCliente.ToString();
            //long id = 0;
            //if (codigo == null)
            //{
            //    objCliente.Estado = 10;
            //    return;
            //}else
            //{
            //    try
            //    {
            //        id = Convert.ToInt64(objCliente.IdCliente);
            //        verificacion = codigo.Length > 0 && codigo.Length < 999999; ;


            //        if (!verificacion)
            //        {
            //            objCliente.Estado = 1;
            //            return;
            //        }
            //    }catch(Exception e)
            //    {
            //        objCliente.Estado = 100;
            //        return;
            //    }

            //}
            ////end validar codigo

            //begin validar nombre retorna estado=2
            string nombre = objCliente.Nombre;
            if (nombre == null)
            {
                objCliente.Estado = 20;
                return;
            }
            else
            {
                nombre = objCliente.Nombre.Trim();
                verificacion = nombre.Length <= 30 && nombre.Length > 0;
                if (!verificacion)
                {
                    objCliente.Estado = 2;
                    return;
                }

            }
            //end validar nombre

            //begin validar apPaterno retorna estado=3
            string apPaterno = objCliente.Apellido;
            if (apPaterno == null)
            {
                objCliente.Estado = 30;
                return;
            }
            else
            {
                apPaterno = objCliente.Apellido.Trim();
                verificacion = apPaterno.Length <= 30 && apPaterno.Length > 0;
                if (!verificacion)
                {
                    objCliente.Estado = 3;
                    return;
                }

            }
            //end validar apPaterno

            //begin validar apMaterno retorna estado=4
            /*string apMaterno = objCliente.Apmaterno;
            if (apMaterno == null)
            {
                objCliente.Estado = 40;
                return;
            }
            else
            {
                apMaterno = objCliente.Apmaterno.Trim();
                verificacion = apMaterno.Length <= 30 && apMaterno.Length > 0;
                if (!verificacion)
                {
                    objCliente.Estado = 3;
                    return;
                }
            
            }*/
            //end validar apMaterno

            //begin validar dni retorna estado=5
            string dni = objCliente.Cedula;
            if (dni == null)
            {
                objCliente.Estado = 50;
                return;
            }
            else
            {
                dni = objCliente.Cedula.Trim();
                verificacion = dni.Length < 19 && dni.Length > 14;
                if (!verificacion)
                {
                    objCliente.Estado = 5;
                    return;
                }
                
            }
            //end validar dni

            //begin validar direccion retorna estado=6
            string direccion = objCliente.Direccion;
            if (direccion == null)
            {
                objCliente.Estado = 60;
                return;
            }
            else
            {
                direccion = objCliente.Direccion.Trim();
                verificacion = direccion.Length <= 50 && direccion.Length > 0;
                if (!verificacion)
                {
                    objCliente.Estado = 6;
                    return;
                }

            }
            //end validar direccion

            //begin validar telefono retorna estado=7
            string telefono = objCliente.Telefono;
            if (telefono == null)
            {
                objCliente.Estado = 70;
                return;
            }
            else
            {
                telefono = objCliente.Telefono.Trim();
                verificacion = telefono.Length <= 15 && telefono.Length > 6;
                if (!verificacion)
                {
                    objCliente.Estado = 7;
                    return;
                }

            }
            //end validar telefono

            //begin verificar duplicidad retorna estado=8
            Cliente_Venta objClienteAux = new Cliente_Venta();
            objClienteAux.ID_Cliente = objCliente.ID_Cliente;
            verificacion = !objClienteDao.find(objClienteAux);
            if (!verificacion)
            {
                objCliente.Estado = 8;
                return;
            }
            //end validar duplicidad

            //begin verificar duplicidad dni retorna estado=8
            Cliente_Venta objCliente1 = new Cliente_Venta();
            objCliente1.Cedula = objCliente.Cedula;
            verificacion = !objClienteDao.findClientePorDni(objCliente1);
            if (!verificacion)
            {
                objCliente.Estado = 9;
                return;
            }
            //end validar duplicidad

            //si no hay error
            objCliente.Estado = 99;
          //  objClienteDao.create1(objCliente);
            return;
        }

        public void update(Cliente_Venta objCliente)
        {
            bool verificacion = true;
            //begin validar codigo retorna estado=1
            string codigo = objCliente.ID_Cliente.ToString();
            long id = 0;
            if (codigo == null)
            {
                objCliente.Estado = 10;
                return;
            }
            else
            {
                try
                {
                    id = Convert.ToInt64(objCliente.ID_Cliente);
                    verificacion = codigo.Length > 0 && codigo.Length < 9999; ;


                    if (!verificacion)
                    {
                        objCliente.Estado = 1;
                        return;
                    }
                }
                catch (Exception)
                {
                    objCliente.Estado = 100;
                    return;
                }

            }
            //end validar codigo


            //begin validar nombre retorna estado=2
            string nombre = objCliente.Nombre;
            if (nombre == null)
            {
                objCliente.Estado = 20;
                return;
            }
            else
            {
                nombre = objCliente.Nombre.Trim();
                verificacion = nombre.Length <= 30 && nombre.Length > 0;
                if (!verificacion)
                {
                    objCliente.Estado = 2;
                    return;
                }

            }
            //end validar nombre

            //begin validar apPaterno retorna estado=3
            string apPaterno = objCliente.Apellido;
            if (apPaterno == null)
            {
                objCliente.Estado = 30;
                return;
            }
            else
            {
                apPaterno = objCliente.Apellido.Trim();
                verificacion = apPaterno.Length <= 30 && apPaterno.Length > 0;
                if (!verificacion)
                {
                    objCliente.Estado = 3;
                    return;
                }

            }
            //end validar apPaterno

            /*//begin validar apMaterno retorna estado=4
            string apMaterno = objCliente.Apmaterno;
            if (apMaterno == null)
            {
                objCliente.Estado = 40;
                return;
            }
            else
            {
                apMaterno = objCliente.Apmaterno.Trim();
                verificacion = apMaterno.Length <= 30 && apMaterno.Length > 0;
                if (!verificacion)
                {
                    objCliente.Estado = 3;
                    return;
                }

            }*/
            //end validar apMaterno

            //begin validar dni retorna estado=5
            string dni = objCliente.Cedula;
            if (dni == null)
            {
                objCliente.Estado = 50;
                return;
            }
            else
            {
                dni = objCliente.Cedula.Trim();
                verificacion = dni.Length <= 18 && dni.Length > 14;
                if (!verificacion)
                {
                    objCliente.Estado = 5;
                    return;
                }

            }
            //end validar dni

            //begin validar direccion retorna estado=6
            string direccion = objCliente.Direccion;
            if (direccion == null)
            {
                objCliente.Estado = 60;
                return;
            }
            else
            {
                direccion = objCliente.Direccion.Trim();
                verificacion = direccion.Length <= 50 && direccion.Length > 0;
                if (!verificacion)
                {
                    objCliente.Estado = 6;
                    return;
                }

            }
            //end validar direccion

            //begin validar telefono retorna estado=7
            string telefono = objCliente.Telefono;
            if (telefono == null)
            {
                objCliente.Estado = 70;
                return;
            }
            else
            {
                telefono = objCliente.Telefono.Trim();
                verificacion = telefono.Length <= 30 && telefono.Length > 0;
                if (!verificacion)
                {
                    objCliente.Estado = 7;
                    return;
                }

            }
            //end validar telefono

            //begin verificar duplicidad dni retorna estado=8
            Cliente_Venta objCliente1 = new Cliente_Venta();
            objCliente1.Cedula = objCliente.Cedula;
            verificacion = !objClienteDao.findClientePorDni(objCliente1);
            if (!verificacion)
            {
                objCliente.Estado = 9;
                return;
            }
            //end validar duplicidad

            //si no hay error
            objCliente.Estado = 99;
            objClienteDao.update(objCliente);
            return;
        }

        public void delete(Cliente_Venta objCliente)
        {
            bool verificacion = true;
            //verificacion de existencia
            Cliente_Venta objClienteAux = new Cliente_Venta();
            objClienteAux.ID_Cliente = objCliente.ID_Cliente;
            verificacion = objClienteDao.find(objClienteAux);
            if (!verificacion)
            {
                objCliente.Estado = 33;
                return;
            }

            //verificaicon de existencia de Ventas
            /*Venta objVenta = new Venta();
            objVenta.ID_Cliente = objCliente.ID_Cliente;
            verificacion = !objVentaDao.findVentaPorClienteId(objVenta);
            if (!verificacion)
            {
                objCliente.Estado = 34;
                return;
            }
            objCliente.Estado = 99;
            objClienteDao.delete(objCliente);
            return;*/
        }
         
        public bool find(Cliente_Venta objCliente)
        {
            return objClienteDao.find(objCliente);
        }
           
        public List<Cliente_Venta> findAll()
        {
            return objClienteDao.findAll();
        }
        public List<Cliente_Venta> findAllClientes(Cliente_Venta objCLiente)
        {
            return objClienteDao.findAllCliente(objCLiente);
        }
    }
}
