using System;
using System.Collections.Generic;
using System.Linq;

namespace BDCalculator.Domain
{
    public class CycleModelBuilder
    {
        public CycleModel Build(DateTime birthDate, DateTime eventDate)
        {
            var bigCyclePeriods = GetCyclePeriods(birthDate, 144);
            var bigCycleEventPeriod = GetEventPeriod(bigCyclePeriods, eventDate);
            var bigCycle = new PeriodsModel {Periods = bigCyclePeriods, EventPeriod = bigCycleEventPeriod};

            var yearCyclePeriods = GetCyclePeriods(bigCycle.EventPeriod.Start, 24);
            var yearCycleEventPeriod = GetEventPeriod(yearCyclePeriods, eventDate);
            var yearCycle = new PeriodsModel{Periods = yearCyclePeriods, EventPeriod = yearCycleEventPeriod};
            
            var seasonCyclePeriods = GetCyclePeriods(yearCycle.EventPeriod.Start, 4);
            var seasonCycleEventPeriod = GetEventPeriod(seasonCyclePeriods, eventDate);
            var seasonCycle = new PeriodsModel{Periods = seasonCyclePeriods, EventPeriod = seasonCycleEventPeriod};

            var monthCyclePeriods = GetCycleMonthPeriods(seasonCycle.EventPeriod.Start);
            var monthCycleEventPeriod = GetEventPeriod(monthCyclePeriods, eventDate);
            var monthCycle = new PeriodsModel {Periods = monthCyclePeriods, EventPeriod = monthCycleEventPeriod};
            
            var dayCyclePeriods = GetCycleDayPeriods(monthCycle.EventPeriod.Start);
            var dayCycleEventPeriod = GetEventPeriod(dayCyclePeriods, eventDate);
            var dayCycle = new PeriodsModel {Periods = dayCyclePeriods, EventPeriod = dayCycleEventPeriod};

            var cycleModel = new CycleModel
                {Big = bigCycle, Year = yearCycle, Season = seasonCycle, Month = monthCycle, Day = dayCycle};

            return cycleModel;
        }
        
        private List<Period> GetCycleDayPeriods(DateTime startPeriod)
        {
            var lengths = new[] {3, 3, 4, 4, 3, 3};
            var periods = new List<Period>();
            var start = startPeriod;
            var daysInMonth = DateTime.DaysInMonth(start.Year, start.Month);
            for (var i = 0; i < 6; i++)
            {
                var end = start.AddDays(lengths[i]);
                
                if (daysInMonth == 31 && end.Month != start.Month)
                    end = start.AddDays(lengths[i] + 1);
                if (daysInMonth == 29 && end.Month != start.Month)
                    end = start.AddDays(lengths[i] - 1);
                if (daysInMonth == 28 && end.Month != start.Month)
                    end = start.AddDays(lengths[i] - 2);
                
                periods.Add(new Period
                {
                    Start = start,
                    End = end,
                    RawNumber = i,
                    Number = i,
                    Energy = (Energy) i
                });
                start = periods.Last().End;
            }
            return periods;
        }

        private List<Period> GetCyclePeriods(DateTime startPeriod, int monthCount)
        {
            var periods = new List<Period>();
            var start = startPeriod;
            for (var i = 0; i < 6; i++)
            {
                periods.Add(new Period
                {
                    Start = start,
                    End = start.AddMonths(monthCount),
                    RawNumber = i,
                    Number = i,
                    Energy = (Energy) i
                });
                start = periods.Last().End;
            }
            return periods;
        }
        
        private List<Period> GetCycleMonthPeriods(DateTime startPeriod)
        {
            var periods = new List<Period>();
            var start = startPeriod;
            for (var i = 0; i < 6; i++)
            {
                var end = start.AddDays(20);
                if (start.Month != end.Month)
                    end = start.AddMonths(1).AddDays(-10);
                
                periods.Add(new Period
                {
                    Start = start,
                    End = end,
                    RawNumber = i,
                    Number = i,
                    Energy = (Energy) i
                });
                start = periods.Last().End;
            }
            return periods;
        }


        private Period GetEventPeriod(List<Period> periods, DateTime eventDate) =>
            periods.First(x => eventDate >= x.Start && eventDate <= x.End);
    }
    
    public class CycleModel
    {
        public PeriodsModel Big { get; set; }
        public PeriodsModel Year { get; set; }
        public PeriodsModel Season { get; set; }
        public PeriodsModel Month { get; set; }
        public PeriodsModel Day { get; set; }
    }
    
    public class PeriodsModel
    {
        public List<Period> Periods { get; set; }
        public Period EventPeriod { get; set; }
    }

    public class Period
    {
        public int RawNumber { get; set; }
        public int Number { get; set; }
        public Energy Energy { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public enum Energy
    {
        Wind = 0,
        Warmth = 1,
        Heat = 2,
        Humidity = 3,
        Dryness = 4,
        Cold = 5
    }
}