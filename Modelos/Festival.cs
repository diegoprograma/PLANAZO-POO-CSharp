using System;

namespace PLANAZO_POO_CSharp.Modelos
{
    public class Festival : Evento
    {
        public Festival(int id, string nombre, DateTime fecha, decimal precioBase, int capacidad)
            : base(id, nombre, fecha, precioBase, capacidad) { }

        public override decimal CalcularPrecioFinal() => PrecioBase * 1.05m; // Aumento del 5%
    }
}