using System;

namespace PLANAZO_POO_CSharp.Modelos
{
    public class Concierto : Evento
    {
        public Concierto(int id, string nombre, DateTime fecha, decimal precioBase, int capacidad)
            : base(id, nombre, fecha, precioBase, capacidad) { }

        public override decimal CalcularPrecioFinal() => PrecioBase * 1.15m; // Aumento del 15%
    }
}