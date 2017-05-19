using System;
using System.Linq;
using FluentAssertions;
using TP3.DataAccessLayer;
using TP3.Entities;
using Xunit;

namespace TP3.Tests
{
    public class ContratRepositoryTest
    {
        private readonly HedgesContextFactory _contextFactory;
        private readonly ClientEntityFramework _repository;

        public ContratRepositoryTest()
        {
            _contextFactory = new HedgesContextFactory();

            var dbContext = _contextFactory.Create();
            ClearAllTables(dbContext);
            _repository = new ClientEntityFramework(dbContext);
        }

        [Fact]
        public void AddContrat_ShouldAddContratToDatabase()
        {
            var contrat = new Contrat()
            {
                NoContrat = 1,
                NomGroupe = "groupe",
                CodeClient = "code",
                DatePresentation = new DateTime(2017, 06, 01),
                HeureDebut = new DateTime(2017, 06, 01, 21, 00, 00),
                HeureFin = new DateTime(2017, 06, 01, 23, 00, 00),
                Prix = 199.99
            };

            _repository.AddContrat(contrat);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Contrats.FirstOrDefault().ShouldBeEquivalentTo(contrat);
            }
        }

        [Fact]
        public void UpdateContrat_ShouldUpdateContratToDatabase()
        {
            var contrat = new Contrat()
            {
                NoContrat = 2,
                NomGroupe = "groupe",
                CodeClient = "code",
                DatePresentation = new DateTime(2017, 06, 01),
                HeureDebut = new DateTime(2017, 06, 01, 21, 00, 00),
                HeureFin = new DateTime(2017, 06, 01, 23, 00, 00),
                Prix = 199.99
            };

            var updatedContrat = new Contrat()
            {
                NoContrat = contrat.NoContrat,
                NomGroupe = "groupe1",
                CodeClient = "code1",
                DatePresentation = new DateTime(2017, 06, 01),
                HeureDebut = new DateTime(2017, 06, 01, 21, 00, 00),
                HeureFin = new DateTime(2017, 06, 01, 23, 00, 00),
                Prix = 199.99
            };

            _repository.AddContrat(contrat);
            _repository.UpdateContrat(updatedContrat);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Contrats.FirstOrDefault().ShouldBeEquivalentTo(updatedContrat);
            }
        }

        [Fact]
        public void DeleteContrat_ShouldDeleteContrat()
        {
            var contrat = new Contrat()
            {
                NoContrat = 3,
                NomGroupe = "groupe",
                CodeClient = "code",
                DatePresentation = new DateTime(2017, 06, 01),
                HeureDebut = new DateTime(2017, 06, 01, 21, 00, 00),
                HeureFin = new DateTime(2017, 06, 01, 23, 00, 00),
                Prix = 199.99
            };

            var contrat2 = new Contrat()
            {
                NoContrat = 4,
                NomGroupe = "groupe1",
                CodeClient = "code1",
                DatePresentation = new DateTime(2017, 06, 01),
                HeureDebut = new DateTime(2017, 06, 01, 21, 00, 00),
                HeureFin = new DateTime(2017, 06, 01, 23, 00, 00),
                Prix = 199.99
            };
            _repository.AddContrat(contrat);
            _repository.AddContrat(contrat2);

            _repository.DeleteContrat(contrat2);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Contrats.Should().NotContain(contrat2);
            }
        }

        [Fact]
        public void DeleteContrat_ShouldDeleteFactureOfContrat()
        {
            var contrat = new Contrat()
            {
                NoContrat = 5,
                NomGroupe = "groupe",
                CodeClient = "code",
                DatePresentation = new DateTime(2017, 06, 01),
                HeureDebut = new DateTime(2017, 06, 01, 21, 00, 00),
                HeureFin = new DateTime(2017, 06, 01, 23, 00, 00),
                Prix = 199.99
            };
            var facture = new Facture()
            {
                NoContrat = 5,
                DateFacture = new DateTime(2017, 06, 01),
                DatePaiement = new DateTime(2017, 06, 15),
                Prix = 199.99
            };

            _repository.AddContrat(contrat);
            _repository.AddFacture(facture);

            _repository.DeleteContrat(contrat);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Factures.Should().NotContain(facture);
            }
        }

        private static void ClearAllTables(HedgesProductionsContext dbContext)
        {
            dbContext.Contrats.RemoveRange(dbContext.Contrats);
            dbContext.Factures.RemoveRange(dbContext.Factures);
            dbContext.SaveChanges();
        }
    }
}
