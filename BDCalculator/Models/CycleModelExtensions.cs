using BDCalculator.Domain;

namespace BDCalculator.Models
{
    public static class CycleModelExtensions
    {
        public static PeriodsModel[] GetAllCycles(this CycleModel model) =>
            new[]
            {
                model.Big, model.Year, model.Season, model.Month, model.Day
            };
    }
}