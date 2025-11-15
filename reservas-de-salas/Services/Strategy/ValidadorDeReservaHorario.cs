using reservas_de_salas.Interfaces;
using reservas_de_salas.Models;

namespace reservas_de_salas.Services.Strategy
{
    public class ValidadorDeReservaHorario : IValidadorDeReservaStrategy
    {
        private readonly IReservaRepository _repo;
        public ValidadorDeReservaHorario(IReservaRepository repo)
        {
            _repo = repo;   
        }
        public async Task<bool>Validar(Reserva reserva)
        {
            if(reserva.Data.Date < DateTime.Today || (reserva.Data.Date == DateTime.Today && reserva.HoraInicio < DateTime.Now.TimeOfDay))
            {
                return false;
            }

            var existentes = await _repo.FindBySalaIdAndDataAsync(reserva.SalaId, reserva.Data);

            existentes = existentes.Where(r => r.Id != reserva.Id).ToList();

            bool overlap = existentes.Any(r =>
                (reserva.HoraInicio < r.HoraFim) && (r.HoraInicio < reserva.HoraFim)
            );
            return !overlap;
        }
    }
}
