using System;
using System.Linq;
using FluentAssertions;
using TP3.DataAccessLayer;
using TP3.Entities;
using Xunit;

namespace TP3.Tests
{
    public class ClientTest
    {
        private readonly HedgesContextFactory _contextFactory;
        private readonly ClientEntityFramework _repository;

        public ClientTest()
        {
            _contextFactory = new HedgesContextFactory();

            var dbContext = _contextFactory.Create();
            ClearAllTables(dbContext);
            _repository = new ClientEntityFramework(dbContext);
        }

        private static void ClearAllTables(HedgesProductionsContext dbContext)
        {
            dbContext.Clients.RemoveRange(dbContext.Clients);
            dbContext.SaveChanges();
        }


        [Fact]
        public void AddClient_ShouldAddClientToDatabase()
        {
            var client = new Client()
            {
                CodeClient = "Code",
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 123456789,
                DepotNecessaire = false
            };

            _repository.AddClient(client);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Clients.FirstOrDefault().ShouldBeEquivalentTo(client);
            }
        }

        [Fact]
        public void UpdateClient_ShouldUpdateClient()
        {
            var client = new Client()
            {
                CodeClient = "Code",
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 123456789,
                DepotNecessaire = false
            };
            var updatedClient = new Client()
            {
                CodeClient = "Code",
                Prenom = "Prenom1",
                Nom = "Nom1",
                NoTelephone = 123456789,
                DepotNecessaire = false
            };

            _repository.AddClient(client);
            _repository.UpdateClient(updatedClient);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Clients.FirstOrDefault().ShouldBeEquivalentTo(updatedClient);
            }
        }

        [Fact]
        public void DeleteClient_ShouldDeleteClient()
        {
            var client = new Client()
            {
                CodeClient = "Code",
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 123456789,
                DepotNecessaire = false
            };
            var client2 = new Client()
            {
                CodeClient = "Code1",
                Prenom = "Prenom1",
                Nom = "Nom1",
                NoTelephone = 123456789,
                DepotNecessaire = false
            };
            _repository.AddClient(client);
            _repository.AddClient(client2);

            _repository.DeleteClient(client2);

            using (var apiDbContext = _contextFactory.Create())
            {
                apiDbContext.Clients.Should().NotContain(client2);
            }
        }

        [Fact]
        public void AddContrat_ShouldAddContratToDatabase()
        {
            var contrat = new Contrat()
            {
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
    }
}
