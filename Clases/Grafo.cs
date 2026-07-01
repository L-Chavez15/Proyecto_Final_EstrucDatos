using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Grafo
    {
        ListaSimple l_vertices = new ListaSimple();
        int[,] ma;
        int cantidad;

        string[] nom_puntos = { "Faldas de la Montaña", "Bosque Susurrante", "Ciudad Olvidada", "Puente Colgante",
                                 "Resort Celestial", "Cueva de Cristal", "Templo del Espejo", "Glaciar Eterno",
                                 "Refugio del Águila", "La Cumbre" };
        string[] climas = { "Tranquilo", "Nublado", "Viento", "Neblina", "Fantasmal", "Húmedo", "Oscuridad", "Helado", "Ventisca", "Helado" };

        public Grafo(int cant)
        {
            cantidad = cant;
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
                    if (j == i + 1) ma[i, j] = 1; // siempre existe el "salto obligado" al siguiente
                    else if (j > i + 1) ma[i, j] = r.Next(0, 10) < 4 ? 1 : 0; // ~40% de probabilidad de atajo/ruta extra
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
                    if (ma[i, j] == 1)
                    {
                        temp_i.ls.Insertar(temp_j, r.Next(10, 50));
                    }
                    temp_j = temp_j.sig;
                }
                temp_i = temp_i.sig;
            }
        }

        // -------- MODO MANUAL: el jugador elige el camino --------
        public void JugarManual(Vertice v, ref float total, ref string ruta)
        {
            ruta += " -> " + v.dato.nombre;

            Console.Clear();
            Console.WriteLine("==================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("📍 UBICACIÓN ACTUAL: " + v.dato.nombre);
            Console.ResetColor();
            Console.WriteLine("   Estamina gastada: " + total + " Pts.");
            Console.WriteLine("==================================================\n");

            if (v.ls.primero == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🏆 ¡HAS LLEGADO A LA CUMBRE!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Saltos disponibles:");
            v.ls.Mostrar();
            Console.WriteLine("--------------------------------");
            Console.Write("🎮 Ingresa el número del camino que deseas tomar: ");

            int op = int.Parse(Console.ReadLine());
            if (op == 0) return;

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

        // -------- BUSCAR EL INDICE DE UN VERTICE DENTRO DEL ARREGLO AUXILIAR --------
        private int BuscarIndice(Vertice v, Vertice[] nodos)
        {
            for (int i = 0; i < nodos.Length; i++)
            {
                if (nodos[i] == v) return i;
            }
            return -1;
        }

        // -------- DIJKSTRA: EL SISTEMA CALCULA EL CAMINO REALMENTE MÁS ÓPTIMO --------
        // No usa ArrayList/List<T>/PriorityQueue: solo arreglos simples, del mismo
        // tamaño que la matriz de adyacencia (ma), tal como ya hace el resto del proyecto.
        public void CalcularRutaOptima(Vertice inicio, out float total, out string ruta)
        {
            // 1) Pasamos la lista enlazada de vertices a un arreglo, para poder
            //    indexarlos igual que en la matriz "ma" (mismo orden de creación)
            Vertice[] nodos = new Vertice[cantidad];
            Vertice temp = l_vertices.primero;
            int idx = 0;
            while (temp != null)
            {
                nodos[idx] = temp;
                idx++;
                temp = temp.sig;
            }

            float[] distancia = new float[cantidad]; // costo acumulado mínimo hasta cada nodo
            bool[] visitado = new bool[cantidad];
            int[] anterior = new int[cantidad];       // para reconstruir el camino

            for (int i = 0; i < cantidad; i++)
            {
                distancia[i] = float.MaxValue;
                visitado[i] = false;
                anterior[i] = -1;
            }

            int origen = BuscarIndice(inicio, nodos);
            distancia[origen] = 0;

            // 2) Bucle principal de Dijkstra: se repite "cantidad" de veces
            for (int c = 0; c < cantidad; c++)
            {
                // Elegimos el nodo NO visitado con menor distancia acumulada
                // (esto reemplaza a la cola de prioridad de la versión clásica)
                int u = -1;
                float menor = float.MaxValue;
                for (int i = 0; i < cantidad; i++)
                {
                    if (!visitado[i] && distancia[i] < menor)
                    {
                        menor = distancia[i];
                        u = i;
                    }
                }

                if (u == -1) break; // ya no quedan nodos alcanzables
                visitado[u] = true;

                // 3) Relajamos las aristas que salen de u
                Arista a = nodos[u].ls.primero;
                while (a != null)
                {
                    int v = BuscarIndice(a.destino, nodos);
                    float nuevaDistancia = distancia[u] + a.peso;
                    if (nuevaDistancia < distancia[v])
                    {
                        distancia[v] = nuevaDistancia;
                        anterior[v] = u;
                    }
                    a = a.sig;
                }
            }

            // 4) La meta siempre es el último vértice creado ("La Cumbre")
            int destino = cantidad - 1;
            total = distancia[destino];

            // 5) Reconstruimos la ruta usando nuestra Pila propia
            //    (anterior[] nos da el camino al revés: de la meta al inicio)
            Pila pila = new Pila();
            int actual = destino;
            while (actual != -1)
            {
                pila.Apilar(nodos[actual].dato.nombre);
                actual = anterior[actual];
            }

            ruta = "";
            while (!pila.EstaVacia())
            {
                ruta += " -> " + pila.Desapilar();
            }
        }
    }

}
