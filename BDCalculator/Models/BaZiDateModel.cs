namespace BDCalculator.Models
{
    public class BaZiDateModel
    {
        public BaZiModel Date { get; set; }
        public BaZiModel Month { get; set; }
        public BaZiModel Year { get; set; }
        public BaZiModel Hour { get; set; }
        public BaZiModel Season { get; set; }
    }

    public class BaZiModel
    {
        public Element Element { get; set; }
        public Animal Animal { get; set; }

        public override string ToString()
        {
            return $"{Animal.ToString()} / {Element.ToString()}";
        }
    }
}