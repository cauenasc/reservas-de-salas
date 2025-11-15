using reservas_de_salas.Interfaces;
using reservas_de_salas.Models;

namespace reservas_de_salas.Services.Strategy
{
    public class ValidadorDeReservaCapacidade : IValidadorDeReservaStrategy
    {
        private readonly ISalaRepository _salaRepo;
        public ValidadorDeReservaCapacidade(ISalaRepository salaRepo)
        {
            _salaRepo = salaRepo;
        }

        public async Task<bool> Validar(Reserva reserva)
        {
            var sala = await _salaRepo.GetByIdAsync(reserva.SalaId);
            return reserva.NumeroPessoas <= sala.Capacidade;
        }
    }
}
