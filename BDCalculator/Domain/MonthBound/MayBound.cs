using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class MayBound : IMonthBound
    {
        public MayBound()
        {
            Index = 5;
            Month = "May";

            const int loopLength = 30;
            const int loopStartYear = 1913;
            var sequences = new[] {new[] {6}, new[] {6, 5, 6, 6}, new[] {5, 6, 6, 5}, new[] {6, 5, 5, 5}};

            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }

        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }
    }
}