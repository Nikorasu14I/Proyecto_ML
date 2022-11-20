using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dao1;
using Model.Entity;

namespace Model.Neg
{
    public class ProductoNeg
    {

        private readonly ProductoDao _Db = new ProductoDao();

        private ProductoDao objProductoDao;
        /*private DetalleVentaDao objDetalleVentaDao;*/
        public ProductoNeg()
        {
            objProductoDao = new ProductoDao();
            /*objDetalleVentaDao = new DetalleVentaDao();*/
        }

        public List<Model.Entity.Producto> Read()
        {
            return _Db.findAllCategoria();
        }

        public int AgregarR(Recibo ORecibo)
        {
           return objProductoDao.AgregarR(ORecibo);
        }

        public static void Agregar(Servicios OServicio)
        {
            ProductoDao.Agregar(OServicio);
        }

        public static void Actualizar(Servicios OServicio)
        {
            ProductoDao.Actualizar(OServicio);
        }

        public static void Eliminar(int IdAlmacen)
        {
            ProductoDao.Eliminar(IdAlmacen);
        }

        //Leer consulta disponible
        public List<Model.Entity.Servicios> ReadDetalle(int id)
        {
            return _Db.Detalle(id);
        }

        //Leer consulta disponible
        public List<Model.Entity.Servicios> ReadDisponible(String FechaE)
        {
            return _Db.Disponible(FechaE);
        }

        //Leer vista almacen
        public List<Model.Entity.Servicios> ReadVista()
        {
            return _Db.findAllVista();
        }

        //Leer vista Factura
        public List<Model.Entity.Servicios> ReadVistaF()
        {
            return _Db.findAllVistaF();
        }

        public List<Model.Entity.Servicios> ReadVistaD()
        {
            return _Db.findAllVistaD();
        }

        public List<Recibo> ReadVistaRecibo(int ID)
        {
            return _Db.findAllRecibos(ID);
        }

        public List<Model.Entity.Servicios> ReadVistaT()
        {
            return _Db.findAllVistaT();
        }


        public void create(Producto objProducto)
        {
            /*bool verificacion = true;
            //inicio verificacion de codigo estado=1
            int codigo = objProducto.ID_Servicio;
            if (codigo != 0)
            {
                objProducto.Estado = 10;
                return;
            }
            else
            {
                objProducto.Estado = 1;
                return;
            }
            //fin verificacion de codigo

            //inicio verificacion de nombre estado=2
            string nombre = objProducto.Descripcion;
            if (nombre == null)
            {
                objProducto.Estado = 20;
                return;
            }
            else
            {
                nombre = objProducto.Descripcion.Trim();
                verificacion = nombre.Length > 0 && nombre.Length <= 30;
                if (!verificacion)
                {
                    objProducto.Estado = 2;
                    return;
                }
            }
            //fin verificacion de codigo

            //inicio verificacion de precioUnitario estado=3
            string precioUni = objProducto.Precio.ToString();
            float preUni = 0;
            if (nombre == null)
            {
                objProducto.Estado = 30;
                return;
            }
            else
            {
                try
                {
                    preUni = Convert.ToSingle(objProducto.Precio);
                    verificacion = preUni > 0 && preUni <= 9999;
                    if (!verificacion)
                    {
                        objProducto.Estado = 3;
                        return;
                    }
                }
                catch (Exception)
                {
                    objProducto.Estado = 300;
                    return;
                }

            }
            //fin verificacion de precioUnitario

            //inicio verificacion de categoria estado=4
            int categoria = objProducto.ID_Categoria;
            if (categoria != 0)
            {
                objProducto.Estado = 40;
                return;
            }
            else
            {
                    objProducto.Estado = 4;
                    return;
            }
            //fin verificacion de categoria

            //verificacion de duplicidad estado=5
            Producto objProductoAux = new Producto();
            objProductoAux.ID_Servicio = objProducto.ID_Servicio;
            verificacion = !objProductoDao.find(objProductoAux);
            if (!verificacion)
            {
                objProducto.Estado = 5;
                return;
            }
            //fin verificacion duplicidad

            //todo bien
            objProducto.Estado = 99;
            objProductoDao.create(objProducto);
            return;*/
        }

        public void update(Producto objProducto)
        {
           
        }

        public void delete(Producto objProducto)
        {
            
        }

        public bool find(Servicios objProducto)
        {
            return objProductoDao.find(objProducto);
        }

        public List<Producto> findAll()
        {
            return objProductoDao.findAll();
        }


        public List<Producto> findPrecioProducto(Producto objPrducto)
        {
            return objProductoDao.findPrecioProducto(objPrducto);
        }

        public List<Producto> findAllProductos(Producto objProducto)
        {
            return objProductoDao.findAllProductos(objProducto);
        }
        public List<Producto> findAllProductosPorCategoria(Producto objProducto)
        {
            return objProductoDao.findAllProductosPorCategoria(objProducto);
        }
    }
}
