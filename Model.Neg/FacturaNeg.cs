using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Dao1;

namespace Model.Neg
{
    public class FacturaNeg
    {
        private FacturaDao objFacturaDao;

        private readonly FacturaDao _Db = new FacturaDao();
        public FacturaNeg()
        {
            objFacturaDao = new FacturaDao();
            // objDetalleFacturaDao = new DetalleFacturaDao();
        }

        public Factura VistaF(int ID)
        {
            return _Db.VistaF(ID);
        }
        public Recibo VistaR(int ID)
        {
            return _Db.VistaR(ID);
        }


        public Proforma VistaP(int ID)
        {
            return _Db.VistaP(ID);
        }


        public List<Servicios> VistaD(int ID)
        {
            return _Db.Detalle(ID);
        }

        public List<Servicios> VistaDF(int ID)
        {
            return _Db.DetalleF(ID);
        }

        public int create(Factura objFactura)
        {
            return objFacturaDao.create(objFactura);
        }

        public int BuscarC(int ID)
        {
            return objFacturaDao.BuscarC(ID);
        }

        public int createR(Recibo objRecibo)
        {
            return objFacturaDao.createR(objRecibo);
        }

        public int createP(Proforma objFactura)
        {
            return objFacturaDao.createP(objFactura);
        }

    }
}
