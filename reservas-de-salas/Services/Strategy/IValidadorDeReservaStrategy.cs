using reservas_de_salas.Models;

namespace reservas_de_salas.Services.Strategy
{
    public interface IValidadorDeReservaStrategy
    {
        Task<bool> Validar(Reserva reserva);
    }
}
