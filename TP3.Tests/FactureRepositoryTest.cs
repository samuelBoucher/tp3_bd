using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TP3.DataAccessLayer;
using TP3.Entities;
using Xunit;

namespace TP3.Tests
{
    public class FactureRepositoryTest
    {
        private readonly HedgesContextFactory _contextFactory;
        private readonly ClientEntityFramework _repository;

        public FactureRepositoryTest()
        {
            _contextFactory = new HedgesContextFactory();

            var dbContext = _contextFactory.Create();
            ClearAllTables(dbContext);
            _repository = new ClientEntityFramework(dbContext);
        }

        private static void ClearAllTables(HedgesProductionsContext dbContext)
        {
            dbContext.Factures.RemoveRange(dbContext.Factures);
            dbContext.SaveChanges();
        }

        [Fact]
        public void AddFacture_ShouldAddFactureToDatabase()
        {
            var facture = new Facture()
            {
                NoContrat = 1,
                DateFacture = new DateTime(2017, 06, 01),
                DatePaiement = new DateTime(2017, 06, 15),
                Prix = 199.99
            };

            _repository.AddFacture(facture);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Factures.FirstOrDefault().ShouldBeEquivalentTo(facture);
            }
        }

        [Fact]
        public void UpdateFacture_ShouldUpdateFactureToDatabase()
        {
            var facture = new Facture()
            {
                NoFacture = 1,
                NoContrat = 1,
                DateFacture = new DateTime(2017, 06, 01),
                DatePaiement = new DateTime(2017, 06, 15),
                Prix = 199.99
            };

            var updatedFacture = new Facture()
            {
                NoFacture = facture.NoFacture,
                NoContrat = 1,
                DateFacture = new DateTime(2017, 06, 01),
                DatePaiement = new DateTime(2017, 06, 15),
                Prix = 199.99
            };

            _repository.AddFacture(facture);
            _repository.UpdateFacture(updatedFacture);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Factures.FirstOrDefault().ShouldBeEquivalentTo(updatedFacture);
            }
        }

        [Fact]
        public void DeleteFacture_ShouldDeleteFacture()
        {
            var facture = new Facture()
            {
                NoContrat = 1,
                DateFacture = new DateTime(2017, 06, 01),
                DatePaiement = new DateTime(2017, 06, 15),
                Prix = 199.99
            };

            var facture2 = new Facture()
            {
                NoContrat = 1,
                DateFacture = new DateTime(2017, 06, 01),
                DatePaiement = new DateTime(2017, 06, 15),
                Prix = 199.99
            };
            _repository.AddFacture(facture);
            _repository.AddFacture(facture2);

            _repository.DeleteFacture(facture2);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Factures.Should().NotContain(facture2);
            }
        }
    }
}

