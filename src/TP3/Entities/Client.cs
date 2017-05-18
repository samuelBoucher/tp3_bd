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
        [Range(1000000000, 9999999999)]
        public long NoTelephone { get; set; }

        [Required]
        public bool DepotNecessaire { get; set; }

        [ForeignKey("CodeClient")]
        public string Reference { get; set; }
    }
}
