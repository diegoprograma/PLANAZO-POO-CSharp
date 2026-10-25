using System;

namespace PLANAZO_POO_CSharp.Modelos
{
    public class Taller : Evento
    {
        public Taller(int id, string nombre, DateTime fecha, decimal precioBase, int capacidad)
            : base(id, nombre, fecha, precioBase, capacidad) { }

        public override decimal CalcularPrecioFinal() => PrecioBase * 0.90m; // Descuento del 10%
    }
}