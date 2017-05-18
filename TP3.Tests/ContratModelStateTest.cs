using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using TP3.Entities;
using Xunit;

namespace TP3.Tests
{
    public class ContratModelStateTest
    {
        [Fact]
        public void ModelState_GoodContrat_ReturnNoError()
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

            var modelStateValidity = ValidateContrat(contrat);

            modelStateValidity.Should().BeTrue();
        }

        [Fact]
        public void ModelState_ContratWithoutGroupName_ReturnError()
        {
            var contrat = new Contrat()
            {
                NomGroupe = null,
                CodeClient = "code",
                DatePresentation = new DateTime(2017, 06, 01),
                HeureDebut = new DateTime(2017, 06, 01, 21, 00, 00),
                HeureFin = new DateTime(2017, 06, 01, 23, 00, 00),
                Prix = 199.99
            };

            var modelStateValidity = ValidateContrat(contrat);

            modelStateValidity.Should().BeFalse();
        }

        [Fact]
        public void ModelState_ContratWithoutClientCode_ReturnError()
        {
            var contrat = new Contrat()
            {
                NomGroupe = "groupe",
                CodeClient = null,
                DatePresentation = new DateTime(2017, 06, 01),
                HeureDebut = new DateTime(2017, 06, 01, 21, 00, 00),
                HeureFin = new DateTime(2017, 06, 01, 23, 00, 00),
                Prix = 199.99
            };

            var modelStateValidity = ValidateContrat(contrat);

            modelStateValidity.Should().BeFalse();
        }

        private static bool ValidateContrat(Contrat contrat)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(contrat, null, null);
            return Validator.TryValidateObject(contrat, validationContext, validationResults, true);
        }
    }
}