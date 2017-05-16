using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP3.Entities
{
    public class Contrat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int NoContrat { get; set; }

        [ForeignKey("Nom")]
        [Required]
        public string NomGroupe { get; set; }

        [ForeignKey("CodeClient")]
        [Required]
        public string CodeClient { get; set; }

        [Required]
        public DateTime DatePresentation { get; set; }

        [Required]
        public DateTime HeureDebut { get; set; }

        [Required]
        public DateTime HeureFin { get; set; }

        [Required]
        public double Prix { get; set; }
    }
}
