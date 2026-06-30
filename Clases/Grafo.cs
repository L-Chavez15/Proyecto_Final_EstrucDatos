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
        public Vertice GetInicio()
        {
            return l_vertices.primero;
        }
        public void GenerarMatriz()
        {
            Random r = new Random();
            for (int i = 0; i < ma.GetLength(0); i++)
            {
                for (int j = 0; j < ma.GetLength(1); j++)
                {
                    ma[i, j] = r.Next(0, 2);
                }
            }
        }
        public void MostrarMatriz()
        {
            for (int i = 0; i < ma.GetLength(0); i++)
            {
                for (int j = 0; j < ma.GetLength(1); j++)
                {
                    Console.Write(ma[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public void CrearGrafo()
        {
            Random r = new Random();
            Vertice temp_i = l_vertices.primero;
            for (int i = 0; i < ma.GetLength(0); i++)
            {
                Vertice temp_j = l_vertices.primero;
                for (int j = 0; j < ma.GetLength(1); j++)
                {
                    //i,j
                    //temp_i,temp_j
                    if (ma[i, j] == 1)
                    {
                        //unir temp_i con el temp_j
                        temp_i.ls.Insertar(temp_j, r.Next(100, 500));
                    }
                    temp_j = temp_j.sig;
                }
                temp_i = temp_i.sig;
            }
        }

        public void Recorrer(Vertice v, ref float total)
        {
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("--------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ciudad actual: \n" + v.dato + "\n");
            Console.ResetColor();
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Viajes disponibles: ");
            v.ls.Mostrar();
            Console.WriteLine("--------------------------------");
            Console.Write("Ingrese el numero de la ciudad a la que desea viajar: ");
            int op = int.Parse(Console.ReadLine());

            if (op == 0) return;

            Arista temp = v.ls.primero;
            for (int i = 1; i < op; i++)
            {
                temp = temp.sig;
            }
            total = total + temp.peso;
            //con la arista por la que tengo que recorrer
            Recorrer(temp.destino, ref total);
        }

    }

}
