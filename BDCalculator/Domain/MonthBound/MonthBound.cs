using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public interface IMonthBound
    {
        int Index { get; set; }

        string Month { get; set; }

        Dictionary<int, int> BoundDates { get; set; }
    }
}