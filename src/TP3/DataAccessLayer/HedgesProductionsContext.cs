using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public HedgesProductionsContext(DbContextOptions<HedgesProductionsContext> options)
            : base(options)
        {
        }
    }
}