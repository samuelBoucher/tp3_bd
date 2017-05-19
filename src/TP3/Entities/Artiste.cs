using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Remotion.Linq;
using FluentValidation;
using FluentValidation.Attributes;
using TP3.DataAccessLayer;

namespace TP3.Entities
{
    [Validator(typeof(ArtisteValidator))]
    public class Artiste
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdArtiste { get; set; }

        [Required]
        public int Nas { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public int NoTelephone { get; set; }

        public string NomScène { get; set; }
    }

    public class ArtisteValidator : AbstractValidator<Artiste>
    {
        public ArtisteValidator()
        {
            RuleFor(x => x.Nas).Must(UniqueNas).WithMessage("Nas doit être unique");
        }


        public bool UniqueNas(int nas)
        {
            var _db = new HedgesProductionsContext(null);
            if (_db.Artistes.SingleOrDefault(x => x.Nas == nas) == null) return true;
            return false;
        }
    }
}
