using System;
using System.Collections.Generic;
using System.Linq;
using PLANAZO_POO_CSharp.Modelos;

namespace PLANAZO_POO_CSharp.Servicios
{
    public class GestorReservas
    {
        private readonly List<Evento> _eventos = new();
        private readonly List<Reserva> _reservas = new();
        private int _contadorReservas = 1;

        public GestorReservas()
        {
            CargarEventosIniciales();
        }

        private void CargarEventosIniciales()
        {
            // Mínimo 6 eventos iniciales requeridos + 1 festival del reto
            _eventos.Add(new Cine(1, "Película: Inception", DateTime.Now.AddDays(2), 15000m, 50));
            _eventos.Add(new Cine(2, "Película: Avatar 2", DateTime.Now.AddDays(3), 18000m, 40));
            _eventos.Add(new Concierto(3, "Concierto: Rock en Vivo", DateTime.Now.AddDays(5), 50000m, 100));
            _eventos.Add(new Concierto(4, "Concierto: Pop Stars", DateTime.Now.AddDays(10), 80000m, 80));
            _eventos.Add(new Taller(5, "Taller: Introducción a C#", DateTime.Now.AddDays(1), 30000m, 20));
            _eventos.Add(new Taller(6, "Taller: Cocina Italiana", DateTime.Now.AddDays(7), 45000m, 15));
            _eventos.Add(new Festival(7, "Festival: Estéreo Picnic", DateTime.Now.AddDays(15), 120000m, 200));
        }

        public List<Evento> ObtenerEventos() => _eventos;

        public Evento ObtenerEventoPorId(int id)
        {
            var evento = _eventos.FirstOrDefault(e => e.Id == id);
            if (evento == null)
                throw new KeyNotFoundException($"No se encontró ningún evento con el ID {id}.");
            return evento;
        }

        public Reserva RealizarReserva(Usuario usuario, int idEvento, int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad de entradas debe ser mayor a cero.");

            var evento = ObtenerEventoPorId(idEvento);

            if (cantidad > evento.CuposDisponibles)
                throw new InvalidOperationException($"Cupos insuficientes. Solo quedan {evento.CuposDisponibles} cupos disponibles.");

            if (evento.ReducirCupos(cantidad))
            {
                var reserva = new Reserva(_contadorReservas++, usuario, evento, cantidad);
                _reservas.Add(reserva);
                return reserva;
            }

            throw new InvalidOperationException("No se pudo procesar la reserva.");
        }

        public void CancelarReserva(int idReserva)
        {
            var reserva = _reservas.FirstOrDefault(r => r.Id == idReserva);
            if (reserva == null)
                throw new KeyNotFoundException($"No se encontró ninguna reserva con el ID {idReserva}.");

            if (reserva.Estado == EstadoReserva.Cancelada)
                throw new InvalidOperationException("La reserva ya se encuentra cancelada.");

            reserva.Cancelar();
            reserva.EventoReservado.DevolverCupos(reserva.CantidadEntradas);
        }

        public List<Reserva> ObtenerReservas() => _reservas;

        public List<Reserva> ObtenerReservasPorUsuario(string identificacion)
        {
            return _reservas.Where(r => r.Cliente.Identificacion.Equals(identificacion, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}