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
            dbContext.Contrats.RemoveRange(dbContext.Contrats);
            dbContext.Factures.RemoveRange(dbContext.Factures);
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

        [Fact]
        public void UpdateContrat_ShouldUpdateContratToDatabase()
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
                NomGroupe = "groupe",
                CodeClient = "code",
                DatePresentation = new DateTime(2017, 06, 01),
                HeureDebut = new DateTime(2017, 06, 01, 21, 00, 00),
                HeureFin = new DateTime(2017, 06, 01, 23, 00, 00),
                Prix = 199.99
            };

            var contrat2 = new Contrat()
            {
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
