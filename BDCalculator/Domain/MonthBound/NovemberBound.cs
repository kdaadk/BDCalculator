using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class NovemberBound : IMonthBound
    {
        public NovemberBound()
        {
            Index = 11;
            Month = "November";

            const int loopLength = 33;
            const int loopStartYear = 1925;
            var sequences = new[] {new[] {8, 8, 8, 7}, new[] {8, 8, 7, 7}, new[] {8, 7, 7, 7}, new[] {7}};

            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }

        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }
    }
}