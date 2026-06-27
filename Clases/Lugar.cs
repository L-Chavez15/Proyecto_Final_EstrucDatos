namespace Clases
{
    public class Lugar
    {
        public string nombre;
        public string descripcion;

        public override string ToString()
        {
            return $"{nombre} -> {descripcion}";
        }
    }
}