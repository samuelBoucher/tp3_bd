using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP3.Entities
{
    public class Client
    {
        [Key]
        [Required]
        public string CodeClient { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public int NoTelephone { get; set; }

        [Required]
        public bool DepotNecessaire { get; set; }

        [ForeignKey("CodeClient")]
        public string Reference { get; set; }
    }
}
