using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Grafo
    {
        ListaSimple l_vertices=new ListaSimple();
        int[,] ma;

        public Grafo(int cant)
        {
            Random r = new Random();
            for (int i = 0; i < cant; i++)
            {
                Lugar l=new Lugar();
                Console.Write("Ingrese nombre de la ciudad: ");
                l.nombre=Console.ReadLine();
                Console.Write("Descripcion: ");
                l.descripcion = Console.ReadLine();

                l_vertices.Insertar(l);
            }
            ma = new int[cant, cant];
        }
        
    }

}
