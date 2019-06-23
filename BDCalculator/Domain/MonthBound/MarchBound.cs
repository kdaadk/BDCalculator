using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class MarchBound :IMonthBound
    {
        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }

        public MarchBound()
        {
            Index = 3;
            Month = "March";
            
            const int loopLength = 35;
            const int loopStartYear = 1912;
            var sequences = new[] {new[] {6}, new[] {6,5,6,6}, new[] {6,6,5,5}, new[] {5,5,6,5}};
            
            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }
    }
}