using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class DecemberBound : IMonthBound
    {
        public DecemberBound()
        {
            Index = 12;
            Month = "December";

            const int loopLength = 34;
            const int loopStartYear = 1919;
            var sequences = new[] {new[] {8, 7, 7, 8}, new[] {7, 7, 8, 7}, new[] {7}, new[] {7, 7, 7, 6}};

            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }

        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }
    }
}