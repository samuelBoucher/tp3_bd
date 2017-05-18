using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TP3.DataAccessLayer;
using TP3.Entities;
using Xunit;
using Xunit.Sdk;

namespace TP3.Tests
{
    public class ArtisteGroupeTestSetup
    {
        public Artiste anyArtiste;
        public Groupe anyGroupe;
        public readonly HedgesContextFactory _contextFactory;
        public readonly ArtisteGroupeRepositoryEntityFramework _repository;

        public ArtisteGroupeTestSetup()
        {
            _contextFactory = new HedgesContextFactory();
            var dbContext = _contextFactory.Create();
            ClearAllTables(dbContext);
            _repository = new ArtisteGroupeRepositoryEntityFramework(dbContext);

            anyArtiste = new Artiste()
            {
                IdArtiste = 123456,
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 123456789,
                Nas = 123456,
                NomScène = "NomScene"
            };

            _repository.AddArtiste(anyArtiste);

            anyGroupe = new Groupe()
            {
                Nom = "Nom",
                CachetVoulu = "CachetVoulu"
            };

            _repository.AddGroupe(anyGroupe, anyArtiste.IdArtiste, "role");

        }

        private static void ClearAllTables(HedgesProductionsContext dbContext)
        {
            dbContext.Artistes.RemoveRange(dbContext.Artistes);
            dbContext.Groupes.RemoveRange(dbContext.Groupes);
            dbContext.LienArtisteGroupe.RemoveRange(dbContext.LienArtisteGroupe);
            dbContext.SaveChanges();
        }
    }


    public class ArtisteGroupeTests : ArtisteGroupeTestSetup
    {
//--------------------------- ARTISTE -----------------------------------------------------------------------------------------------------------

        [Fact]
        public void Add_ShouldAddArtisteToDatabase()
        {
            var artiste = new Artiste()
            {
                IdArtiste = 101234136,
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 123456789,
                Nas = 123456,
                NomScène = "NomScene"
            };

            _repository.AddArtiste(artiste);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Artistes.Find(artiste.IdArtiste).ShouldBeEquivalentTo(artiste);
            }
        }

        [Fact]
        public void Get_ShouldReturnTheRightArtiste()
        {

            Artiste returnedArtiste = _repository.GetArtiste(anyArtiste.IdArtiste);

            this.anyArtiste.ShouldBeEquivalentTo(returnedArtiste);
        }

        [Fact]
        public void Delete_ShouldRemoveArtisteFromDatabase()
        {

            _repository.DeleteArtiste(anyArtiste.IdArtiste);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Artistes.ToList().Count.Should().Be(0);
            }
        }

        [Fact]
        public void Delete_ShouldRemoveGroupeFromDatabaseIfItIsEmpty()
        {

            _repository.DeleteArtiste(anyArtiste.IdArtiste);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Groupes.ToList().Count.Should().Be(0);
            }
        }

        [Fact]
        public void Update_ShouldUpdateArtisteInDatabase()
        {
            var newArtiste = new Artiste()
            {
                IdArtiste = anyArtiste.IdArtiste,
                Prenom = "NouveauPrenom",
                Nom = "NouveauNom",
                NoTelephone = 987654321,
                Nas = 652451,
                NomScène = "NouveauNomScene"
            };

            _repository.UpdateArtiste(newArtiste);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Artistes.Find(anyArtiste.IdArtiste).ShouldBeEquivalentTo(newArtiste);
            }
        }
    }
}
