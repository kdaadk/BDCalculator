namespace BDCalculator.Models
{
    public class PentagramValuesModel
    {
        public int Heat { get; set; }
        public int Humidity { get; set; }
        public int Dryness { get; set; }
        public int Cold { get; set; }
        public int Wind { get; set; }

        public static PentagramValuesModel operator+(PentagramValuesModel a, PentagramValuesModel b)
        {
            return new PentagramValuesModel
            {
                Heat = a.Heat + b.Heat,
                Humidity = a.Humidity + b.Humidity,
                Dryness = a.Dryness + b.Dryness,
                Cold = a.Cold + b.Cold,
                Wind = a.Wind + b.Wind
            };
        }
    }
}