namespace PLANAZO_POO_CSharp.Modelos
{
    public class Usuario
    {
        public string Identificacion { get; private set; }
        public string Nombre { get; private set; }

        public Usuario(string identificacion, string nombre)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
                throw new ArgumentException("La identificación no puede estar vacía.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            Identificacion = identificacion;
            Nombre = nombre;
        }
    }
}