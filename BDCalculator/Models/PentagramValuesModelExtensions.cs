using System.Collections.Generic;
using System.Linq;

namespace BDCalculator.Models
{
    public static class PentagramValuesModelExtensions
    {
        public static int GetMax(this PentagramValuesModel model)
        {
            return new List<DerivativeElement>
                {model.Heat, model.Humidity, model.Dryness, model.Cold, model.Wind}.Max(p => p.Value);
        }

        public static void SetRedColor(this PentagramValuesModel model)
        {
            var pentagramValues = new List<DerivativeElement>
                {model.Heat, model.Humidity, model.Dryness, model.Cold, model.Wind};
            var position = pentagramValues.First(p => p.Value == pentagramValues.Max(x => x.Value)).Position;
            var nextPosition = position == 4 ? 0 : position + 1;
            var prevPosition = position == 0 ? 4 : position - 1;
            var positions = new[] {prevPosition, position, nextPosition};

            foreach (var element in pentagramValues.Where(x => positions.Contains(x.Position)))
                element.IsRed = true;
        }
    }
}