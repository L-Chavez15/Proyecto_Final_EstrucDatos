namespace Clases
{
    public class Lugar
    {
        public string nombre;
        public string clima;
        public float temperatura;

        public override string ToString()
        {
            return $"{nombre} (Clima: {clima} - {temperatura} C°)";
        }
    }
}