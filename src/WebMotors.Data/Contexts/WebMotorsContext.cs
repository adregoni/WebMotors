using Microsoft.EntityFrameworkCore;
using WebMotors.Domain.Entities;

namespace WebMotors.Data.Contexts
{
    public sealed class WebMotorsContext : DbContext
    {
        public WebMotorsContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Anuncio> Anuncios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebMotorsContext).Assembly);
        }
    }
}
