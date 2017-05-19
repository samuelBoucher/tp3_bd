using System;
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
    public class GroupeTestSetup
    {
        public Artiste anyArtiste;
        public Groupe anyGroupe;
        public readonly HedgesContextFactory _contextFactory;
        public readonly ArtisteGroupeRepositoryEntityFramework _repository;

        public GroupeTestSetup()
        {
            _contextFactory = new HedgesContextFactory();
            var dbContext = _contextFactory.Create();
            ClearAllTables(dbContext);
            _repository = new ArtisteGroupeRepositoryEntityFramework(dbContext);

            anyArtiste = new Artiste()
            {
                IdArtiste = 651451,
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 123456789,
                Nas = 542515335,
                NomScène = "NomScene"
            };

            _repository.AddArtiste(anyArtiste);

            anyGroupe = new Groupe()
            {
                Nom = "WOWZERS",
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


    public class GroupeTests : GroupeTestSetup
    {
        [Fact]
        public void Add_ShouldAddGroupeToDatabase()
        {
            var groupe = new Groupe()
            {
                Nom = "wow",
                CachetVoulu = "CASH"
            };

            _repository.AddGroupe(groupe, anyArtiste.IdArtiste, "COOL GUY");

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Groupes.FirstOrDefault(x => x.Nom == groupe.Nom).ShouldBeEquivalentTo(groupe);
            }
        }

        [Fact]
        public void Add_ShouldLinkArtisteToGroupe()
        {
            var groupe = new Groupe()
            {
                Nom = "wow",
                CachetVoulu = "CASH"
            };

            var expectedLink = new LienArtisteGroupe
            {
                IdArtiste = anyArtiste.IdArtiste,
                NomGroupe = groupe.Nom,
                RoleArtiste = "COOL GUY"
            };

            _repository.AddGroupe(groupe, anyArtiste.IdArtiste, "COOL GUY");

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.LienArtisteGroupe.FirstOrDefault(x => x.IdArtiste == anyArtiste.IdArtiste && x.NomGroupe == groupe.Nom).ShouldBeEquivalentTo(expectedLink);
            }
        }

        [Fact]
        public void Get_ShouldReturnTheRightGroupe()
        {

            Groupe returnedGroupe = _repository.GetGroupe(anyGroupe.Nom);

            this.anyGroupe.ShouldBeEquivalentTo(returnedGroupe);
        }

        [Fact]
        public void Delete_ShouldRemoveGroupeFromDatabase()
        {

            _repository.DeleteGroupe(anyGroupe.Nom);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Groupes.Should().NotContain(anyGroupe);
            }
        }

        [Fact]
        public void Delete_ShouldRemoveLiensArtisteGroupeFromDatabase()
        {
            using (var apiDbContext = _contextFactory.Create())
            {
                LienArtisteGroupe lien = apiDbContext.LienArtisteGroupe
                    .FirstOrDefault(x => x.NomGroupe == anyGroupe.Nom
                                    && x.IdArtiste == anyArtiste.IdArtiste);

                _repository.DeleteGroupe(anyGroupe.Nom);

            
                apiDbContext.LienArtisteGroupe.Should().NotContain(lien);

            }
        }

        [Fact]
        public void Delete_ShouldRemoveContratFromDatabase()
        {
            using (var apiDbContext = _contextFactory.Create())
            {
                Contrat contrat = new Contrat
                {
                    CodeClient = "wow",
                    DatePresentation = DateTime.MaxValue,
                    HeureDebut = DateTime.MaxValue,
                    HeureFin = DateTime.MaxValue,
                    NoContrat = 1234,
                    NomGroupe = anyGroupe.Nom,
                    Prix = 293
                };

                apiDbContext.Contrats.Add(contrat);

                _repository.DeleteGroupe(anyGroupe.Nom);


                apiDbContext.Contrats.ToList().Count().Should().Be(0);

            }
        }

        [Fact]
        public void Update_ShouldUpdateGroupeInDatabase()
        {
            var newgroupe = new Groupe()
            {
                Nom = anyGroupe.Nom,
                CachetVoulu = "NOUVO COCHET"
            };

            _repository.UpdateGroupe(newgroupe);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Groupes.FirstOrDefault(x => x.Nom == anyGroupe.Nom).ShouldBeEquivalentTo(newgroupe);
            }
        }

        //-------------MODEL STATE---------------------------------------------------------------

        private bool validateGroupe(Groupe groupe)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(groupe, null, null);
            return Validator.TryValidateObject(groupe, validationContext, validationResults, true);
        }

        [Fact]
        public void ModelState_GoodItem_ReturnNoError()
        {
            var modelStateValidity = validateGroupe(anyGroupe);

            modelStateValidity.Should().BeTrue();
        }

        [Fact]
        public void ModelState_GroupeWithoutName_ReturnAnError()
        {
            anyGroupe.Nom = null;

            var modelStateValidity = validateGroupe(anyGroupe);

            modelStateValidity.Should().BeFalse();
        }

    }
}
