using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class AugustBound : IMonthBound
    {
        public AugustBound()
        {
            Index = 8;
            Month = "August";

            const int loopLength = 30;
            const int loopStartYear = 1915;
            var sequences = new[] {new[] {8}, new[] {8, 8, 8, 7}, new[] {8, 7, 7, 8}, new[] {7, 7, 8, 7}};

            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }

        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }
    }
}