using reservas_de_salas.Interfaces;
using reservas_de_salas.Models;

namespace reservas_de_salas.Services
{
    public class SalaService : ISalaService
    {

        private readonly ISalaRepository _salaRepository;

        public SalaService(ISalaRepository salaRepository)
        {
         _salaRepository = salaRepository;
        }

        public async Task<IEnumerable<Sala>> GetAllSalasAsync()
        {
            return await _salaRepository.GetAllAsync();
        }
        public async Task<Sala> GetByIdAsync(long id)
        {
            return await _salaRepository.GetByIdAsync(id);
        }

        public async Task<Sala> SaveSalaAsync(Sala sala)
        {
            if(sala.Id == 0) // Sala nova (ID 0 indica que não foi persistida ainda)
            {
                 await _salaRepository.AddAsync(sala);
            }
            else // Sala existente, atualizar
            {
                _salaRepository.Update(sala);
            }
           
            await _salaRepository.SaveChangesAsync();
            return sala;
        }
        public async Task DeleteSalaAsync(long id)
        {
            var salaToDelete = await _salaRepository.GetByIdAsync(id);
            if (salaToDelete != null)
            {
                throw new InvalidOperationException("Sala não encontrada para exclusão.");
            }
                _salaRepository.Delete(salaToDelete);
            await _salaRepository.SaveChangesAsync();
        }

    }
}
