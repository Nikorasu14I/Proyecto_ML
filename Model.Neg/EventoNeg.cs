using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Dao1;

namespace Model.Neg
{
    public class EventoNeg
    {
        private EventoDao objEventoDao;
        //private DetalleFacturaDao objDetalleFacturaDao;
        public EventoNeg()
        {
            objEventoDao = new EventoDao();
        //    objDetalleFacturaDao = new DetalleFacturaDao();
        }

        public int create(Evento objVenta)
        {           
           
            return objEventoDao.create(objVenta);
        }

        public Evento VistaC(int ID)
        {
            return objEventoDao.VistaC(ID);
        }
    }
}
