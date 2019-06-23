using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public class JuneBound :IMonthBound
    {
        public int Index { get; set; }
        public string Month { get; set; }
        public Dictionary<int, int> BoundDates { get; set; }

        public JuneBound()
        {
            Index = 6;
            Month = "June";
            
            const int loopLength = 30;
            const int loopStartYear = 1905;
            var sequences = new[] {new[] {6,6,7,6}, new[] {6}, new[] {6,6,6,5}, new[] {6,5,5,6}};
            
            BoundDates = BoundsProvider.GetBoundDates(sequences, loopLength, loopStartYear);
        }
    }
}