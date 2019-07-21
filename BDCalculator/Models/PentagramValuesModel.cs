using System.Collections.Generic;
using System.Linq;

namespace BDCalculator.Models
{
    public class PentagramValuesModel
    {
        public PentagramValuesModel(int heat, int humidity, int dryness, int cold, int wind)
        {
            Heat = new DerivativeElement {Value = heat, Position = 0};
            Humidity = new DerivativeElement {Value = humidity, Position = 1};
            Dryness = new DerivativeElement {Value = dryness, Position = 2};
            Cold = new DerivativeElement {Value = cold, Position = 3};
            Wind = new DerivativeElement {Value = wind, Position = 4};
        }

        public PentagramValuesModel()
        {
            Heat = new DerivativeElement {Position = 0};
            Humidity = new DerivativeElement {Position = 1};
            Dryness = new DerivativeElement {Position = 2};
            Cold = new DerivativeElement {Position = 3};
            Wind = new DerivativeElement {Position = 4};
        }

        public DerivativeElement Heat { get; set; }
        public DerivativeElement Humidity { get; set; }
        public DerivativeElement Dryness { get; set; }
        public DerivativeElement Cold { get; set; }
        public DerivativeElement Wind { get; set; }

        public static PentagramValuesModel operator +(PentagramValuesModel a, PentagramValuesModel b) =>
            new PentagramValuesModel
            (
                a.Heat.Value + b.Heat.Value,
                a.Humidity.Value + b.Humidity.Value,
                a.Dryness.Value + b.Dryness.Value,
                a.Cold.Value + b.Cold.Value,
                a.Wind.Value + b.Wind.Value
            );
    }

    public class DerivativeElement
    {
        public int Value { get; set; }
        
        public int Position { get; set; }
        
        public bool IsRed { get; set; }
    }
}