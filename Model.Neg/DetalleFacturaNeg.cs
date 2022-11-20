using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Dao1;

namespace Model.Neg
{
    public class DetalleFacturaNeg
    {
        private DetalleFacturaDao objDetalleFacturaDao;
        public DetalleFacturaNeg()
        {
            objDetalleFacturaDao = new DetalleFacturaDao();
        }

        public void create(DetalleFactura objVenta)
        {
            objDetalleFacturaDao.create(objVenta);
        }
        public void createP(DetalleFactura objVenta)
        {
            objDetalleFacturaDao.createP(objVenta);
        }


    }
}
