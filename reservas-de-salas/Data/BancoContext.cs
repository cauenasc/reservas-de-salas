using Microsoft.EntityFrameworkCore;

namespace reservas_de_salas.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }
        public DbSet<Models.Usuario> Usuarios { get; set; }
        public DbSet<Models.Sala> Salas { get; set; }
        public DbSet<Models.Reserva> Reservas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
