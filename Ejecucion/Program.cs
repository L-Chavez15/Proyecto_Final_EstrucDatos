using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecucion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Grafo gf=new Grafo(5); // Por ejemplo, 5 ciudades
            int op = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("\t\t========HABITTRACK========");
                Console.WriteLine("1. Ingrese cantidad de lugares: ");
                Console.WriteLine("2. Generar matriz de adyacencia");
                Console.WriteLine("3. Calcular ruta más corta");
                Console.WriteLine("0. Salir");
                Console.Write("INGRESE UNA OPCIÓN: ");
                op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        Console.Write("Ingrese la cantidad de lugares: ");
                        int cant = int.Parse(Console.ReadLine());
                        Grafo gf = new Grafo(cant);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;

                    default:
                        Console.WriteLine("Ingrese una opción válida.");
                        break;
                }
                Console.ReadKey();  
            } while (op!=0);


        }
    }
}
