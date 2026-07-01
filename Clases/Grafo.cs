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
        string[] nom_puntos = { "Faldas de la Montaña", "Ciudad Olvidada", "Resort Celestial", "Templo del Espejo", "La Cumbre" };
        string[] climas = { "Tranquilo", "Viento", "Fantasmal", "Oscuridad", "Helado" };

        

        public Grafo(int cant)
        {
            Random r = new Random();
            for (int i = 0; i < cant; i++)
            {
                Lugar p = new Lugar();
                p.nombre = nom_puntos[i];
                p.clima = climas[i];
                p.temperatura = r.Next(-10, 15);

                l_vertices.Insertar(p);
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
                    // Evita bucles infinitos: Siempre vamos hacia adelante
                    if (j == i + 1) ma[i, j] = 1;
                    else if (j > i + 1) ma[i, j] = r.Next(0, 2);
                    else ma[i, j] = 0;
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
                        temp_i.ls.Insertar(temp_j, r.Next(10, 50));
                    }
                    temp_j = temp_j.sig;
                }
                temp_i = temp_i.sig;
            }
        }

        public void JugarManual(Vertice v, ref float total, ref string ruta)
        {
            ruta += " -> " + v.dato.nombre;

            Console.Clear();
            Console.WriteLine("==================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("* UBICACIÓN ACTUAL: " + v.dato.nombre);
            Console.ResetColor();
            Console.WriteLine("   Estamina gastada: " + total + " Pts.");
            Console.WriteLine("==================================================\n");

            // Si ya no hay caminos, llegaste a la meta
            if (v.ls.primero == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("* ¡HAS LLEGADO A LA CUMBRE!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Saltos disponibles:");
            v.ls.Mostrar(); // Muestra las opciones
            Console.WriteLine("--------------------------------");
            Console.Write("* Ingresa el número del camino que deseas tomar: ");

            int op = int.Parse(Console.ReadLine());

            if (op == 0) return;

            // Buscar la arista seleccionada
            Arista temp = v.ls.primero;
            for (int i = 1; i < op; i++)
            {
                if (temp != null) temp = temp.sig;
            }

            if (temp != null)
            {
                total = total + temp.peso;
                JugarManual(temp.destino, ref total, ref ruta);
            }
        }
        private int BuscarIndice(Vertice v, Vertice[] nodos)
        {
            for (int i = 0; i < nodos.Length; i++)
            {
                if (nodos[i] == v) return i;
            }
            return -1;
        }
        public void CalcularRutaOptima(Vertice v, ref float total, ref string ruta)
        {
            if (v == null) return;
            ruta += " -> " + v.dato.nombre;

            if (v.ls.primero == null) return;

            Arista temp = v.ls.primero;
            Arista mejorOpcion = temp;

            while (temp != null)
            {
                if (temp.peso < mejorOpcion.peso) mejorOpcion = temp;
                temp = temp.sig;
            }

            total = total + mejorOpcion.peso;
            CalcularRutaOptima(mejorOpcion.destino, ref total, ref ruta);
        }

    }

}
