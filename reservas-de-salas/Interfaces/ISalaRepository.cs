using reservas_de_salas.Models;

namespace reservas_de_salas.Interfaces
{
    public interface ISalaRepository
    {

        Task<IEnumerable<Models.Sala>> GetAllAsync();
        Task<Models.Sala> GetByIdAsync(long id);
        Task AddAsync(Models.Sala sala);
        void Update(Models.Sala sala);
        void Delete(Sala sala);
        Task SaveChangesAsync();
    }
}
