using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using TP3.Entities;
using Xunit;

namespace TP3.Tests
{
    public class ClientModelStateTest
    {
        [Fact]
        public void ModelState_GoodClient_ReturnNoError()
        {
            var client = new Client()
            {
                CodeClient = "Code",
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 1234567890,
                DepotNecessaire = false
            };

            var modelStateValidity = ValidateClient(client);

            modelStateValidity.Should().BeTrue();
        }

        [Fact]
        public void ModelState_ClientWithoutCode_ReturnError()
        {
            var client = new Client()
            {
                CodeClient = null,
                Prenom = "Prenom",
                Nom = "Nom",
                NoTelephone = 1234567890,
                DepotNecessaire = false
            };

            var modelStateValidity = ValidateClient(client);

            modelStateValidity.Should().BeFalse();
        }

        [Fact]
        public void ModelState_ClientWithoutFirstName_ReturnError()
        {
            var client = new Client()
            {
                CodeClient = "Code",
                Prenom = null,
                Nom = "Nom",
                NoTelephone = 1234567890,
                DepotNecessaire = false
            };

            var modelStateValidity = ValidateClient(client);

            modelStateValidity.Should().BeFalse();
        }

        [Fact]
        public void ModelState_ClientWithoutLastName_ReturnError()
        {
            var client = new Client()
            {
                CodeClient = "Code",
                Prenom = "Prenom",
                Nom = null,
                NoTelephone = 1234567890,
                DepotNecessaire = false
            };

            var modelStateValidity = ValidateClient(client);

            modelStateValidity.Should().BeFalse();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99999999999)]
        public void ModelState_ClientWithBadPhoneNumber_ReturnError(long phoneNumber)
        {
            var client = new Client()
            {
                CodeClient = "Code",
                Prenom = null,
                Nom = "Nom",
                NoTelephone = phoneNumber,
                DepotNecessaire = false
            };

            var modelStateValidity = ValidateClient(client);

            modelStateValidity.Should().BeFalse();
        }

        private static bool ValidateClient(Client client)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(client, null, null);
            return Validator.TryValidateObject(client, validationContext, validationResults, true);
        }
    }
}