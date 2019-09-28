namespace BDCalculator.Models
{
    public class SetUpPentagramModel
    {
        public PentagramValuesModel InnerValues { get; set; }
        public PentagramValuesModel OuterValues { get; set; }

        public static SetUpPentagramModel operator +(SetUpPentagramModel a, SetUpPentagramModel b)
        {
            return new SetUpPentagramModel
            {
                InnerValues = a.InnerValues + b.InnerValues,
                OuterValues = a.OuterValues + b.OuterValues
            };
        }
    }
}