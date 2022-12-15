using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetApi.Entities
{
    public class Pokemon
    {
        [Key]
        [Required]
        public int numero { get; set; }
        // Forçage du type varchar au lieu de longtext par défaut 
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string name { get; set; }

        [Required]
        // Clée entrangère vers element
        public string element_name { get; set; }
        public Element? element { get; set; }

        public Pokemon? evolution_ { get; set; }
        public Pokemon? sous_evolution_ { get; set; }
    }
}