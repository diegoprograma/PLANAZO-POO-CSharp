using System;
using System.Linq;
using PLANAZO_POO_CSharp.Modelos;
using PLANAZO_POO_CSharp.Servicios;

namespace PLANAZO_POO_CSharp
{
    internal class Program
    {
        private static GestorReservas _gestor = new GestorReservas();

        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("        PLANAZO - EVENTOS        ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Ver eventos disponibles");
                Console.WriteLine("2. Consultar evento");
                Console.WriteLine("3. Reservar entradas");
                Console.WriteLine("4. Ver reservas realizadas");
                Console.WriteLine("5. Cancelar una reserva");
                Console.WriteLine("6. Ver resumen de reservas");
                Console.WriteLine("7. Consultar reservas por usuario (Reto)");
                Console.WriteLine("0. Salir");
                Console.WriteLine("=================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (opcion)
                    {
                        case "1": VerEventosDisponibles(); break;
                        case "2": ConsultarEvento(); break;
                        case "3": ReservarEntradas(); break;
                        case "4": VerReservasRealizadas(); break;
                        case "5": CancelarReserva(); break;
                        case "6": VerResumenReservas(); break;
                        case "7": ConsultarReservasPorUsuario(); break;
                        case "0": salir = true; break;
                        default: Console.WriteLine("Opción inválida. Intente de nuevo."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERROR]: {ex.Message}");
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void VerEventosDisponibles()
        {
            Console.WriteLine("--- EVENTOS DISPONIBLES ---");
            foreach (var item in _gestor.ObtenerEventos())
            {
                Console.WriteLine($"ID: {item.Id} | Tipo: {item.GetType().Name} | Nombre: {item.Nombre} | Fecha: {item.Fecha:dd/MM/yyyy} | Base: ${item.PrecioBase:N0} | Precio Final: ${item.CalcularPrecioFinal():N0} | Cupos: {item.CuposDisponibles}/{item.CapacidadMaxima}");
            }
        }

        static void ConsultarEvento()
        {
            Console.Write("Ingrese el ID del evento a consultar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var ev = _gestor.ObtenerEventoPorId(id);
                Console.WriteLine($"\nDetalles del Evento #{ev.Id}:");
                Console.WriteLine($"Nombre: {ev.Nombre}");
                Console.WriteLine($"Tipo: {ev.GetType().Name}");
                Console.WriteLine($"Fecha: {ev.Fecha}");
                Console.WriteLine($"Precio Base: ${ev.PrecioBase:N0}");
                Console.WriteLine($"Precio Final: ${ev.CalcularPrecioFinal():N0}");
                Console.WriteLine($"Cupos Disponibles: {ev.CuposDisponibles}/{ev.CapacidadMaxima}");
            }
            else Console.WriteLine("ID inválido.");
        }

        static void ReservarEntradas()
        {
            Console.Write("Ingrese ID del cliente: ");
            string doc = Console.ReadLine();
            Console.Write("Ingrese nombre del cliente: ");
            string nombre = Console.ReadLine();
            Usuario usuario = new Usuario(doc, nombre);

            Console.Write("Ingrese ID del evento a reservar: ");
            int idEv = int.Parse(Console.ReadLine());

            Console.Write("Ingrese cantidad de entradas: ");
            int cant = int.Parse(Console.ReadLine());

            var res = _gestor.RealizarReserva(usuario, idEv, cant);
            Console.WriteLine($"\n¡Reserva exitosa! ID Reserva: {res.Id} | Total a pagar: ${res.ValorTotal:N0}");
        }

        static void VerReservasRealizadas()
        {
            Console.WriteLine("--- HISTORIAL DE RESERVAS ---");
            var lista = _gestor.ObtenerReservas();
            if (!lista.Any()) { Console.WriteLine("No hay reservas registradas."); return; }

            foreach (var r in lista)
            {
                Console.WriteLine($"ID Reserva: {r.Id} | Cliente: {r.Cliente.Nombre} ({r.Cliente.Identificacion}) | Evento: {r.EventoReservado.Nombre} | Cantidad: {r.CantidadEntradas} | Total: ${r.ValorTotal:N0} | Estado: {r.Estado}");
            }
        }

        static void CancelarReserva()
        {
            Console.Write("Ingrese el ID de la reserva a cancelar: ");
            if (int.Parse(Console.ReadLine()) is int id)
            {
                _gestor.CancelarReserva(id);
                Console.WriteLine("Reserva cancelada exitosamente y cupos devueltos.");
            }
        }

        static void VerResumenReservas()
        {
            var activas = _gestor.ObtenerReservas().Where(r => r.Estado == EstadoReserva.Activa).ToList();
            int cantidadReservas = activas.Count;
            int totalEntradas = activas.Sum(r => r.CantidadEntradas);
            decimal valorTotal = activas.Sum(r => r.ValorTotal);

            Console.WriteLine("--- RESUMEN DE RESERVAS ACTIVAS ---");
            Console.WriteLine($"Cantidad de reservas activas: {cantidadReservas}");
            Console.WriteLine($"Total entradas reservadas: {totalEntradas}");
            Console.WriteLine($"Valor total acumulado: ${valorTotal:N0}");
        }

        static void ConsultarReservasPorUsuario()
        {
            Console.Write("Ingrese el documento del usuario: ");
            string doc = Console.ReadLine();
            var reservas = _gestor.ObtenerReservasPorUsuario(doc);

            if (!reservas.Any())
            {
                Console.WriteLine("El usuario no registra reservas.");
                return;
            }

            Console.WriteLine($"\nReservas encontradas para la identificación {doc}:");
            foreach (var r in reservas)
            {
                Console.WriteLine($"Reserva #{r.Id} | Evento: {r.EventoReservado.Nombre} | Cantidad: {r.CantidadEntradas} | Estado: {r.Estado}");
            }
        }
    }
}