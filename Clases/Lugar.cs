namespace Clases
{
    public class Lugar
    {
        // punto de control
        public string nombre;
        public string clima;
        public int temperatura;
        public string descripcion; // NUEVO: texto narrativo del lugar

        public override string ToString()
        {
            return $"{nombre} - CLIMA: {clima}, TEMPERATURA: {temperatura}";
        }
    }
}