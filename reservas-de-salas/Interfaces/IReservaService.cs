using reservas_de_salas.Models;
using reservas_de_salas.Services.Strategy;

namespace reservas_de_salas.Interfaces
{
    public interface IReservaService
    { 
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<Reserva> GetByIdAsync(long id);
        Task<Reserva> SaveASync(Reserva reserva);
        Task DeleteAsync(long id);
        void setValidador(IValidadorDeReservaStrategy validador);
        Task<bool> ValidateAsync(Reserva reserva);
    }
}
