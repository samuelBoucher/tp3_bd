using Microsoft.EntityFrameworkCore;
using TP3.Entities;

namespace TP3.DataAccessLayer
{
    public class HedgesProductionsContext : DbContext
    {
        public DbSet<Artiste> Artistes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contrat> Contrats { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Groupe> Groupes { get; set; }
        public DbSet<LienArtisteGroupe> LienArtisteGroupe { get; set; }


        public HedgesProductionsContext(DbContextOptions<HedgesProductionsContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LienArtisteGroupe>()
                .HasKey(lien => new { lien.IdArtiste, lien.NomGroupe });
        }

    }
}
