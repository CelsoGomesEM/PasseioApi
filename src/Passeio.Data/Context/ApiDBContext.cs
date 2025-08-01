using Microsoft.EntityFrameworkCore;
using Passeio.Negocio.Models;

namespace Passeio.Data.Context
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options){}

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lugar> Lugares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Esta linha faz com que o EF Core encontre todas as classes de mapping (como CategoriaMapping)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDBContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
