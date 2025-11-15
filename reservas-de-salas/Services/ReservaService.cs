using reservas_de_salas.Interfaces;
using reservas_de_salas.Models;
using reservas_de_salas.Services.Strategy;

namespace reservas_de_salas.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private IValidadorDeReservaStrategy _strategy;

        public ReservaService(IReservaRepository repository)
        {
            _reservaRepository = repository;    
        }
        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await _reservaRepository.GetAllAsync();
        }
        public async Task<Reserva> GetByIdAsync(long id)
        {
            var reserva =  await _reservaRepository.GetByIdAsync(id);
            if (reserva == null)
            {
                throw new KeyNotFoundException("Reserva não encontrada.");
            }
            return reserva;
        }
        public async Task<Reserva> SaveASync(Reserva reserva)
        {
           if (reserva.Id == 0)
            {
                await _reservaRepository.AddAsync(reserva);
            }
           else
            {
                _reservaRepository.Update(reserva);
            }
           await _reservaRepository.SaveChangesAsync();
            return reserva;
        }

        public async Task DeleteAsync(long id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);
            if (reserva == null)
            {
                throw new KeyNotFoundException("Reserva não encontrada.");
            }
            _reservaRepository.Delete(reserva);
            await _reservaRepository.SaveChangesAsync();

        }
        public void setValidador(IValidadorDeReservaStrategy validador)
        {
            _strategy = validador;
        }

        public async Task<bool> ValidateAsync(Reserva reserva)
        {
            if (_strategy != null)
            {
              return await _strategy.Validar(reserva);
            }
           throw new InvalidOperationException("Nenhuma estratégia de validação foi definida.");
        }

    }
}
