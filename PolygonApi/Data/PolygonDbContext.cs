using Microsoft.EntityFrameworkCore;
using PolygonApi.Models;

namespace PolygonApi.Data
{
    public class PolygonDbContext : DbContext
    {
        public PolygonDbContext(DbContextOptions<PolygonDbContext> options) : base(options)
        {
        }

        public DbSet<Lagerort> Lagerorte { get; set; }
        public DbSet<Kreditor> Kreditoren { get; set; }
        public DbSet<Artikel> Artikel { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Artikelvariante> Artikelvarianten { get; set; }
        public DbSet<Artikeleinheit> Artikeleinheiten { get; set; }
        public DbSet<Zustaendigkeitseinheitencod> Zustaendigkeitseinheitencodes { get; set; }
        public DbSet<Artikelreferenz> Artikelreferenzen { get; set; }
        public DbSet<Projekt> Projekte { get; set; }
        public DbSet<ZuordnungVerkaeuferZustaendigkeitseinheit> ZuordnungVerkaeuferZustaendigkeitseinheiten { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite keys
            modelBuilder.Entity<Artikelvariante>()
                .HasKey(a => new { a.ArticleNo, a.ArtCode });

            modelBuilder.Entity<Artikeleinheit>()
                .HasKey(a => new { a.ArticleNo, a.ArtCode });

            modelBuilder.Entity<ZuordnungVerkaeuferZustaendigkeitseinheit>()
                .HasKey(z => new { z.SellerBuyerId, z.RespUnitCode });
        }
    }
}

