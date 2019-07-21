using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class JulyBound : IMonthBound
    {
        public JulyBound()
        {
            Index = 7;
            Month = "July";

            const int loopLength = 30;
            const int loopStartYear = 1927;
            var sequences = new[] {new[] {8, 7, 7, 8}, new[] {7, 7, 8, 7}, new[] {7}, new[] {7, 7, 7, 6}};

            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }

        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }
    }
}