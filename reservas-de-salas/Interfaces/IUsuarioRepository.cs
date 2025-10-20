using reservas_de_salas.Models;

namespace reservas_de_salas.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();// Recupera todos os usuários do banco de dados
        Task<Usuario> GetByIdAsync(long id);// Recupera um usuário pelo seu ID
        Task<Usuario> GetByEmailAsync(string email);// Recupera um usuário pelo seu e-mail
        Task AddAsync(Usuario usuario);// Adiciona um novo usuário ao banco de dados
        void Update(Usuario usuario);// Atualiza um usuário existente no banco de dados
        void Delete(Usuario usuario);// Remove um usuário do banco de dados
        Task SaveChangesAsync();// Salva as alterações feitas no banco de dados
    }
}
