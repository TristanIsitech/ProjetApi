using System.ComponentModel.DataAnnotations;

namespace ProjetApi.Entities
{
    public class Element
    {
        public Element(string sname, List<Pokemon> spokemon){
            name = sname;
            pokemon = spokemon;
        }

        [Key]
        [Required]
        public string name { get; set; }
        // List des pokemons de cette Element
        public List<Pokemon>? pokemon { get; set; }

        // SI UN ELEMENT1 EST LA FAIBLESSE D'UN ELEMENT2 ALORS ELEMENT2 A POUR RESISTANCE ELEMENT1
        // C'EST DONC UNE RELATION PLUSIEURS VERS PLUSIEURS, IL FAUT DONC UNE TABLE INTERMEDIAIRE APPELER FAIBLESSE.

        // List des Elements ayant pour resistance cette Element
        public List<Faiblesse>? resistance {get; set;}
        // List des Elements ayant pour faiblesse cette Element
        public List<Faiblesse>? faiblesse {get; set;}
    }
}