using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Pila
    {
        public Nodo cima = null;

        //apilar(PUSH)
        public void Apilar(string nombreLugar)
        {
            //1. crear nodo
            Nodo nuevo = new Nodo();
            nuevo.dato = nombreLugar;
            /*reducir código
            nuevo.siguiente = cima;
            cima = nuevo;*/
            if (cima == null)
            {
                cima = nuevo;
            }
            else
            {
                nuevo.siguiente = cima;
                cima = nuevo;
            }
        }

        //Desapilar
        public string Desapilar()
        {
            string dato = null;
            if (cima != null)
            {
                dato = cima.dato;
                cima = cima.siguiente;
            }
            return dato;
        }

    }
}
