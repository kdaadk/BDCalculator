using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class SeptemberBound : IMonthBound
    {
        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }

        public SeptemberBound()
        {
            Index = 9;
            Month = "September";
            
            const int loopLength = 30;
            const int loopStartYear = 1929;
            var sequences = new[] {new[] {8}, new[] {8,7,8,8}, new[] {7,8,8,7}, new[] {8,7,7,7}};
            
            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }
    }
}