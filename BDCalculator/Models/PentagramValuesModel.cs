using System.Windows.Media;

namespace BDCalculator.Models
{
    public class PentagramValuesModel
    {
        public PentagramValuesModel(int heat, int humidity, int dryness, int cold, int wind)
        {
            Heat = new PentagramValueModel {Value = heat, Position = 0};
            Humidity = new PentagramValueModel {Value = humidity, Position = 1};
            Dryness = new PentagramValueModel {Value = dryness, Position = 2};
            Cold = new PentagramValueModel {Value = cold, Position = 3};
            Wind = new PentagramValueModel {Value = wind, Position = 4};
        }

        public PentagramValuesModel()
        {
            Heat = new PentagramValueModel {Position = 0};
            Humidity = new PentagramValueModel {Position = 1};
            Dryness = new PentagramValueModel {Position = 2};
            Cold = new PentagramValueModel {Position = 3};
            Wind = new PentagramValueModel {Position = 4};
        }

        public PentagramValueModel Heat { get; }
        public PentagramValueModel Humidity { get; }
        public PentagramValueModel Dryness { get; }
        public PentagramValueModel Cold { get; }
        public PentagramValueModel Wind { get; }

        public static PentagramValuesModel operator +(PentagramValuesModel a, PentagramValuesModel b)
        {
            return new PentagramValuesModel
            (
                a.Heat.Value + b.Heat.Value,
                a.Humidity.Value + b.Humidity.Value,
                a.Dryness.Value + b.Dryness.Value,
                a.Cold.Value + b.Cold.Value,
                a.Wind.Value + b.Wind.Value
            );
        }
    }

    public class PentagramValueModel
    {
        public int Value { get; set; }
        public int Position { get; set; }
        public Color Color { get; set; }
        public bool IsWeak { get; set; }
        public string Name { get; set; }
    }
}