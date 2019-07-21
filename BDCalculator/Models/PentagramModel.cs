namespace BDCalculator.Models
{
    public class PentagramModel
    {
        public PentagramValuesModel InnerValues { get; set; }
        public PentagramValuesModel OuterValues { get; set; }

        public static PentagramModel operator +(PentagramModel a, PentagramModel b)
        {
            return new PentagramModel
            {
                InnerValues = a.InnerValues + b.InnerValues,
                OuterValues = a.OuterValues + b.OuterValues
            };
        }
    }
}