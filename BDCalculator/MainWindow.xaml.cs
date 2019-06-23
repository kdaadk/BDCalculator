using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BDCalculator.Domain;
using BDCalculator.Models;

namespace BDCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate_OnClick(object sender, RoutedEventArgs e)
        {
            var calculator = new BaZiCalculator();
            var selectedDate = datePicker.SelectedDate;
            
            if (selectedDate == null)
            {
                MessageBox.Show("Select a birthday", "No date selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var pentagram = calculator.GetPentagram(selectedDate.Value);
                AddPentagramValues(pentagram);
            }
        }

        private void AddPentagramValues(PentagramModel pentagram)
        {
            Text(145, 25, pentagram.OuterValues.Heat, Colors.Black); //heat
            Text(145, 75, pentagram.InnerValues.Heat, Colors.Black);

            Text(265, 115, pentagram.OuterValues.Humidity, Colors.Black); //humidity
            Text(215, 115, pentagram.InnerValues.Humidity, Colors.Black);

            Text(190, 235, pentagram.OuterValues.Dryness, Colors.Black); //dryness
            Text(190, 195, pentagram.InnerValues.Dryness, Colors.Black);

            Text(105, 235, pentagram.OuterValues.Cold, Colors.Black); //cold
            Text(105, 195, pentagram.InnerValues.Cold, Colors.Black);

            Text(25, 115, pentagram.OuterValues.Wind, Colors.Black); //wind
            Text(75, 115, pentagram.InnerValues.Wind, Colors.Black);
        }

        private void Text(double x, double y, int text, Color color) 
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text.ToString();
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvas.Children.Add(textBlock);
        }
    }
}