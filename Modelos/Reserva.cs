namespace PLANAZO_POO_CSharp.Modelos
{
    public enum EstadoReserva { Activa, Cancelada }

    public class Reserva
    {
        public int Id { get; private set; }
        public Usuario Cliente { get; private set; }
        public Evento EventoReservado { get; private set; }
        public int CantidadEntradas { get; private set; }
        public decimal ValorTotal { get; private set; }
        public EstadoReserva Estado { get; private set; }

        public Reserva(int id, Usuario cliente, Evento evento, int cantidadEntradas)
        {
            Id = id;
            Cliente = cliente;
            EventoReservado = evento;
            CantidadEntradas = cantidadEntradas;
            ValorTotal = evento.CalcularPrecioFinal() * cantidadEntradas;
            Estado = EstadoReserva.Activa;
        }

        public void Cancelar()
        {
            Estado = EstadoReserva.Cancelada;
        }
    }
}