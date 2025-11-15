using Microsoft.EntityFrameworkCore;
using reservas_de_salas.Data;
using reservas_de_salas.Interfaces;
using reservas_de_salas.Models;

namespace reservas_de_salas.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly BancoContext _context;
        public ReservaRepository(BancoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Sala)
                .ToListAsync();
        }

        public async Task<Reserva> GetByIdAsync(long id)
        {
            return await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Sala)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task AddAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
        }
        public void Update(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
        }
        public void Delete(Reserva reserva)
        {
            _context.Reservas.Remove(reserva);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Reserva>> FindBySalaIdAndDataAsync(long salaId, DateTime data)
        {
            return await _context.Reservas
                .Where(r => r.SalaId == salaId && r.Data.Date == data.Date)
                .ToListAsync();
        }

    }
}
