
namespace ProjetApi.Entities
{
    public class Faiblesse
    {
        public string element_resistance { get; set; }
        public Element? resistance { get; set; }

        public string element_faiblesse { get; set; }
        public Element faiblesse { get; set; }
    }
}