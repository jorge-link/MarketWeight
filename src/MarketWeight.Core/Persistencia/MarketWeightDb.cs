using Microsoft.EntityFrameworkCore;
using MarketWeight.Core;  // Ajustá el namespace según corresponda

namespace MarketWeight.Core.Persistencia
{
    public class MarketWeightDb : DbContext
    {
        public MarketWeightDb(DbContextOptions<MarketWeightDb> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Historial> Historiales { get; set; }
        public DbSet<UsuarioMoneda> UsuarioMonedas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir clave primaria simple para Usuario
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);

            // Definir clave primaria simple para Moneda
            modelBuilder.Entity<Moneda>()
                .HasKey(m => m.IdMoneda);

            // Clave primaria compuesta para Historial
            modelBuilder.Entity<Historial>()
                .HasKey(h => new { h.IdHistorial, h.idMoneda, h.IdUsuario });

            // Clave primaria compuesta para UsuarioMoneda
            modelBuilder.Entity<UsuarioMoneda>()
                .HasKey(um => new { um.idUsuario, um.idMoneda });
        }
    }
}
