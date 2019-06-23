using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class FebruaryBound : IMonthBound
    {
        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }

        public FebruaryBound()
        {
            Index = 2;
            Month = "February";
            
            const int loopLength = 34;
            const int loopStartYear = 1913;
            var sequences = new[] {new[] {4,4,5,5}, new[] {4,5,4,4}, new[] {4}, new[] {4,4,3,4}};
            
            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }
    }
}