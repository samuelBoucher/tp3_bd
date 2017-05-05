using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP3.Entities
{
    public class Facture
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int NoFacture { get; set; }

        [ForeignKey("NoContrat")]
        [Required]
        public int NoContrat { get; set; }

        [Required]
        public DateTime DateFacture { get; set; }

        [Required]
        public double Prix { get; set; }

        public DateTime DatePaiement { get; set; }
    }
}
