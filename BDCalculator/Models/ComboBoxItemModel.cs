namespace BDCalculator.Models
{
    public class ComboBoxItemModel
    {
        public string Text { get; set; }

        public BaZiModel Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}