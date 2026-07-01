using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Vertice
    {
        public Lugar dato;
        public string dato2;
        public Vertice sig=null;

        public ListaAristas ls = new ListaAristas();
    }
}
