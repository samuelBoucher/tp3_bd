using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP3.Entities
{
    public class Groupe
    {
        [Required]
        [Key, Column(Order = 1)]
        public string Nom { get; set; }

        public string CachetVoulu { get; set; }
    }
}
