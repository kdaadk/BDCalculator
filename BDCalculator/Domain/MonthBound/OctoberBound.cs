using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class OctoberBound : IMonthBound
    {
        public OctoberBound()
        {
            Index = 10;
            Month = "October";

            const int loopLength = 32;
            const int loopStartYear = 1913;
            var sequences = new[] {new[] {9, 9, 9, 8}, new[] {8, 9, 9, 8}, new[] {8, 8, 9, 8}, new[] {8}};

            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }

        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }
    }
}