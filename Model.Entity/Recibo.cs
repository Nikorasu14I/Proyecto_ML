using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Recibo
    {
        private int id_recibo;
        private string fecha;
        private int id_evento;
        private float monto;
        private float efectivo;
        private float cheque;
        private string forma;
        private string nombre;
        private string banco;
        private string concepto;

        public int ID_Recibo
        {
            get
            {
                return id_recibo;
            }

            set
            {
                id_recibo= value;
            }
        }

        public string Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Concepto
        {
            get
            {
                return concepto;
            }

            set
            {
                concepto = value;
            }
        }

        public string Banco
        {
            get
            {
                return banco;
            }

            set
            {
                banco = value;
            }
        }


        public int ID_Evento
        {
            get
            {
                return id_evento;
            }

            set
            {
                id_evento= value;
            }
        }

        public float Monto
        {
            get
            {
                return monto;
            }

            set
            {
                monto = value;
            }
        }

        public float Efectivo
        {
            get
            {
                return efectivo;
            }

            set
            {
                efectivo = value;
            }
        }

        public float Cheque
        {
            get
            {
                return cheque;
            }

            set
            {
                cheque = value;
            }
        }


        public string Forma
        {
            get
            {
                return forma;
            }

            set
            {
                forma = value;
            }
        }

      
        public Recibo()
        {

        }

        public Recibo(string Fecha, int ID_Evento, float Monto, string Forma, float Efectivo, float Cheque, string Banco, string Concepto)
        {
            this.fecha = Fecha;
            this.id_evento = ID_Evento;
            this.monto = Monto;
            this.forma = Forma;
            this.efectivo = Efectivo;
            this.cheque = Cheque;
            this.banco = Banco;
            this.concepto = Concepto;
        }


    }
}
