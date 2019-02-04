using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCVFinal
{
    public class Reporte 
    {

        private int presentes;

        public int Presentes
        {
            get { return presentes; }
            set { presentes = value; }
        }
        private int ausentes;

        public int Ausentes
        {
            get { return ausentes; }
            set { ausentes = value; }
        }

        List<listado> listaAusentes;
        List<listado> listaPresentes;

        public List<listado> ListaAusentes
        {
            get
            {
                return listaAusentes;
            }

            set
            {
                listaAusentes = value;
            }
        }
        public List<listado> ListaPresentes
        {
            get
            {
                return listaPresentes;
            }

            set
            {
                listaPresentes = value;
            }
        }

    }
}