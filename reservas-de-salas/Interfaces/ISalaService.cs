using reservas_de_salas.Models;

namespace reservas_de_salas.Interfaces
{
    public interface ISalaService
    {
        Task<IEnumerable<Sala>> GetAllSalasAsync();
        Task<Sala> GetByIdAsync(long id);
        Task<Sala> SaveSalaAsync(Sala sala);
        Task DeleteSalaAsync(long id);
    }
}
