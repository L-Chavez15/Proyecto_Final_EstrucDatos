using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Cola
    {
        public Nodo frente = null;
        public Nodo fin = null;
        public void Encolar(Vertice v)
        {
            Nodo nuevo = new Nodo();
            nuevo.datoCola = v;
            if (frente == null)
            {
                frente = nuevo;
                fin = nuevo;
            }
            else
            {
                if (v.distancia < frente.datoCola.distancia)
                {
                    nuevo.sig = frente;
                    frente = nuevo;
                }
                else
                {
                    Nodo temp = frente;

                    // Buscar la posición correcta
                    while (temp.sig != null &&
                           temp.sig.datoCola.distancia <= v.distancia)
                    {
                        temp = temp.sig;
                    }

                    nuevo.sig = temp.sig;
                    temp.sig = nuevo;

                    if (nuevo.sig == null)
                    {
                        fin = nuevo;
                    }
                }
            }
        }
        public Vertice Desencolar()
        {
            Vertice aux = null;

            if (frente != null)
            {
                aux = frente.datoCola;
                frente = frente.sig;

                if (frente == null)
                    fin = null;
            }

            return aux;
        }
        public void Destruir()
        {
            frente = null;
            fin = null;
        }
        public void Mostrar()
        {
            Nodo temp = frente;

            while (temp != null)
            {
                Console.WriteLine(temp.datoCola +" -> " +temp.datoCola.distancia);

                temp = temp.sig;
            }
        }
        public bool EstaVacia()
        {
            return frente == null;
        }
    }
}
