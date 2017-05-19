using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TP3.DataAccessLayer;
using TP3.Entities;
using Xunit;
using Xunit.Sdk;

namespace TP3.Tests
{
    public class LienTestSetup
    {
        public readonly HedgesContextFactory _contextFactory;
        public readonly ArtisteGroupeRepositoryEntityFramework _repository;

        public LienTestSetup()
        {
            _contextFactory = new HedgesContextFactory();
            var dbContext = _contextFactory.Create();
            ClearAllTables(dbContext);
            _repository = new ArtisteGroupeRepositoryEntityFramework(dbContext);

        }

        private static void ClearAllTables(HedgesProductionsContext dbContext)
        {
            dbContext.Artistes.RemoveRange(dbContext.Artistes);
            dbContext.Groupes.RemoveRange(dbContext.Groupes);
            dbContext.LienArtisteGroupe.RemoveRange(dbContext.LienArtisteGroupe);
            dbContext.SaveChanges();
        }
    }


    public class LienTests : LienTestSetup
    {
        [Fact]
        public void JoindreGroupe_ShouldAddLinkToDatabase()
        {
            var anyGroupe = new Groupe()
            {
                Nom = "wow",
                CachetVoulu = "CASH"
            };

            Artiste anyArtiste = new Artiste()
            {
                IdArtiste = 1043851,
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 123456789,
                Nas = 123456,
                NomScène = "NomScene"
            };
            _repository.AddArtiste(anyArtiste);
            _repository.AddGroupe(anyGroupe, anyArtiste.IdArtiste, "GOOD BOY");


            Artiste newArtiste = new Artiste()
            {
                IdArtiste = 12452,
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 123456789,
                Nas = 123456,
                NomScène = "NomScene"
            };

            _repository.AddArtiste(newArtiste);


            _repository.JoindreGroupe(newArtiste.IdArtiste, anyGroupe.Nom, "GOOD BOY");

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.LienArtisteGroupe.ToList().Count().Should().Be(2);
            }
        }

        [Fact]
        public void QuitterGroupe_ShouldRemoveLinkFromDatabase()
        {
            var anyGroupe = new Groupe()
            {
                Nom = "wow",
                CachetVoulu = "CASH"
            };

            Artiste anyArtiste = new Artiste()
            {
                IdArtiste = 1043851,
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 123456789,
                Nas = 123456,
                NomScène = "NomScene"
            };
            _repository.AddArtiste(anyArtiste);
            _repository.AddGroupe(anyGroupe, anyArtiste.IdArtiste, "GOOD BOY");


            _repository.QuitterGroupe(anyArtiste.IdArtiste, anyGroupe.Nom);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.LienArtisteGroupe.ToList().Count().Should().Be(0);
            }
        }

        [Fact]
        public void QuitterGroupe_ShouldRemoveGroupFromDatabaseIfItHasNoLink()
        {
            var anyGroupe = new Groupe()
            {
                Nom = "wow",
                CachetVoulu = "CASH"
            };

            Artiste anyArtiste = new Artiste()
            {
                IdArtiste = 1043851,
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 123456789,
                Nas = 123456,
                NomScène = "NomScene"
            };
            _repository.AddArtiste(anyArtiste);
            _repository.AddGroupe(anyGroupe, anyArtiste.IdArtiste, "GOOD BOY");


            _repository.QuitterGroupe(anyArtiste.IdArtiste, anyGroupe.Nom);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Groupes.ToList().Count().Should().Be(0);
            }
        }

        //-------------MODEL STATE---------------------------------------------------------------

        private bool ValidateLien(LienArtisteGroupe lien)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(lien, null, null);
            return Validator.TryValidateObject(lien, validationContext, validationResults, true);
        }

        [Fact]
        public void ModelState_GoodItem_ReturnNoError()
        {
            LienArtisteGroupe lien = new LienArtisteGroupe
            {
                IdArtiste = 1,
                NomGroupe = "amazing",
                RoleArtiste = "magicien"
            };

            var modelStateValidity = ValidateLien(lien);

            modelStateValidity.Should().BeTrue();
        }

    }
}
