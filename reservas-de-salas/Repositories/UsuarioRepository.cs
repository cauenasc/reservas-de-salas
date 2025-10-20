using Microsoft.EntityFrameworkCore;
using reservas_de_salas.Data;
using reservas_de_salas.Interfaces;
using reservas_de_salas.Models;

namespace reservas_de_salas.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BancoContext _context;

        public UsuarioRepository(BancoContext bancoContext)
        {
            _context = bancoContext;
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public void Delete(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> GetByIdAsync(long id)
        {
           return await _context.Usuarios.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }
    }
}
