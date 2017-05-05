using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP3.Entities
{
    public class Groupe
    {
        [Key]
        [Required]
        public string Nom { get; set; }

        public string CachetVoulu { get; set; }
    }
}
