using System;

namespace PLANAZO_POO_CSharp.Modelos
{
    public class Cine : Evento
    {
        public Cine(int id, string nombre, DateTime fecha, decimal precioBase, int capacidad)
            : base(id, nombre, fecha, precioBase, capacidad) { }

        public override decimal CalcularPrecioFinal() => PrecioBase;
    }
}