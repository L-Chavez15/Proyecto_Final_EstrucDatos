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

            // 1. TÚ JUEGAS (Interactivo)
            montaña.JugarManual(inicio, ref estaminaJugador, ref rutaJugador);

            // 2. EL SISTEMA CALCULA EL MEJOR CAMINO (Silencioso)
            montaña.CalcularRutaOptima(inicio, ref estaminaOptima, ref rutaOptima);

            // 3. RESULTADOS DEL EXAMEN (Comparación)
            Console.WriteLine("\n================ RESULTADOS (EXAMEN FINAL) ================");
            Console.WriteLine("TU PARTIDA:");
            Console.WriteLine("> Camino que tomaste: " + rutaJugador.Substring(4));
            Console.WriteLine("> Estamina total consumida: " + estaminaJugador);
            /*Console.WriteLine("\nRECOMENDACIÓN DEL SISTEMA (Algoritmo Ávido):");
            Console.WriteLine("> El camino más óptimo era: " + rutaOptima.Substring(4));
            Console.WriteLine("> Estamina mínima posible: " + estaminaOptima);*/
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
