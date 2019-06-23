namespace BDCalculator.Models
{
    public class BaZiDateModel
    {
        public BaZiModel Date { get; set; }
        public BaZiModel Month { get; set; }
        public BaZiModel Year { get; set; }

        public PentagramModel Pentagram { get; set; }
    }

    public class BaZiModel
    {
        public Elements Element { get; set; }
        public Animals Animal { get; set; }
    }
}