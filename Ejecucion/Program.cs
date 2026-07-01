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
            int cantidadNodos = 5;
            Grafo montaña = new Grafo(cantidadNodos);

            montaña.GenerarMatriz();
            montaña.CrearGrafo();

            Vertice inicio = montaña.GetInicio();

            // Variables del Jugador
            float estaminaJugador = 0;
            string rutaJugador = "";

            // Variables del Sistema (Óptimo)
            float estaminaOptima = 0;
            string rutaOptima = "";

            Console.WriteLine("==================================================");
            Console.WriteLine("   BIENVENIDO AL SIMULADOR DE ESCALADA CELESTE");
            Console.WriteLine("==================================================");
            Console.WriteLine("Presiona ENTER para empezar a jugar...");
            Console.ReadLine();

            montaña.JugarManual(inicio, ref estaminaJugador, ref rutaJugador);

            montaña.CalcularRutaOptima(inicio, ref estaminaOptima, ref rutaOptima);

            Console.WriteLine("\n================ RESULTADOS  ================");
            Console.WriteLine("TU PARTIDA:");
            Console.WriteLine("> Camino que tomaste: " + rutaJugador.Substring(4));
            Console.WriteLine("> Estamina total consumida: " + estaminaJugador);
            Console.WriteLine("\nRECOMENDACIÓN DEL SISTEMA :");
            Console.WriteLine("> El camino más óptimo era: " + rutaOptima.Substring(4));
            Console.WriteLine("> Estamina mínima posible: " + estaminaOptima);
            Console.WriteLine("===========================================================");

            if (estaminaJugador <= estaminaOptima)
            {
                Console.WriteLine("\n¡Felicidades! Encontraste la ruta perfecta.");
            }
            else
            {
                Console.WriteLine("\nSobreviviste, pero el sistema encontró una ruta mejor.");
            }

            Console.WriteLine("\nPresione cualquier tecla para finalizar...");
            Console.ReadKey();
        }
    }
}
