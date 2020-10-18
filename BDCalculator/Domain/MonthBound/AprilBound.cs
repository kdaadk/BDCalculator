using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class AprilBound : IMonthBound
    {
        public AprilBound()
        {
            Index = 4;
            Month = "April";

            const int loopLength = 32;
            const int loopStartYear = 1912;
            var sequences = new[] {new[] {5, 5, 5, 6}, new[] {5}, new[] {4, 5, 5, 5}, new[] {4, 4, 5, 5}};

            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }

        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }
    }
}