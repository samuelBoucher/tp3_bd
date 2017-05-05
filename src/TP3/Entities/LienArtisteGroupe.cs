using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP3.Entities
{
    public class LienArtisteGroupe
    {
        [Key]
        [ForeignKey("IdArtiste")]
        [Required]
        public int IdArtiste { get; set; }

        [Key]
        [ForeignKey("NomGroupe")]
        [Required]
        public string NomGroupe { get; set; }

        public string RoleArtiste { get; set; }
    }
}
