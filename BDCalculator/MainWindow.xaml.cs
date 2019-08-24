using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using BDCalculator.Domain;
using BDCalculator.Models;

namespace BDCalculator
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly BaZiCalculator calculator = new BaZiCalculator();
        private BaZiDateModel baZiModel;
        private DateTime birthDate;
        private PentagramModel pentagramModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddItemsToComboBox()
        {
            hourComboBox.Items.Clear();

            if (baZiModel.Date == null)
                return;

            if (baZiModel.Date.Element == Element.YangWood || baZiModel.Date.Element == Element.YinEarth)
                foreach (var item in GetComboBoxItems(1, 1))
                    hourComboBox.Items.Add(item);

            if (baZiModel.Date.Element == Element.YinWood || baZiModel.Date.Element == Element.YangMetal)
                foreach (var item in GetComboBoxItems(3, 1))
                    hourComboBox.Items.Add(item);

            if (baZiModel.Date.Element == Element.YangFire || baZiModel.Date.Element == Element.YinMetal)
                foreach (var item in GetComboBoxItems(5, 1))
                    hourComboBox.Items.Add(item);

            if (baZiModel.Date.Element == Element.YinFire || baZiModel.Date.Element == Element.YangWater)
                foreach (var item in GetComboBoxItems(7, 1))
                    hourComboBox.Items.Add(item);

            if (baZiModel.Date.Element == Element.YangEarth || baZiModel.Date.Element == Element.YinWater)
                foreach (var item in GetComboBoxItems(9, 1))
                    hourComboBox.Items.Add(item);
        }

        private List<ComboBoxItemModel> GetComboBoxItems(int firstElement, int firstAnimal)
        {
            var items = new List<ComboBoxItemModel>();
            var elementNumber = firstElement;
            var animalNumber = firstAnimal;

            for (var i = 0; i < 13; i++)
            {
                var element = (Element) elementNumber;
                var animal = (Animal) animalNumber;
                items.Add(new ComboBoxItemModel
                {
                    Text =
                        $"{EnumHelper<Element>.GetDisplayValue(element)}{element}; {EnumHelper<Animal>.GetDisplayValue(animal)}{animal}",
                    Value = new BaZiModel {Animal = animal, Element = element}
                });
                elementNumber++;
                elementNumber %= 10;
                animalNumber++;
                animalNumber %= 12;
            }

            return items;
        }

        private void AddPentagram()
        {
            var pentagramPolygon = new Polygon
            {
                Points = new PointCollection
                {
                    new Point(150, 50), new Point(250, 125),
                    new Point(200, 225), new Point(200, 225),
                    new Point(100, 225), new Point(50, 125)
                },
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            canvas.Children.Add(pentagramPolygon);
        }

        private void AddBaZiModel(BaZiDateModel baZiModel)
        {
            var dateBaZiModel = $"date: {baZiModel.Date}";
            var monthBaZiModel = $"month: {baZiModel.Month}";
            var yearBaZiModel = $"year: {baZiModel.Year}";

            AddTextToCanvas(300, 10, dateBaZiModel);
            AddTextToCanvas(300, 30, monthBaZiModel);
            AddTextToCanvas(300, 50, yearBaZiModel);

            if (baZiModel.Hour != null)
            {
                AddTextToCanvas(300, 70, $"hour: {baZiModel.Hour}");
                if (baZiModel.Season != null)
                    AddTextToCanvas(300, 90, $"season: {baZiModel.Season}");
            }
            else if (baZiModel.Season != null)
            {
                AddTextToCanvas(300, 70, $"season: {baZiModel.Season}");
            }
        }

        private void AddPentagramValues(PentagramModel pentagram)
        {
            var innerMax = pentagram.InnerValues.GetMax();
            var outerMax = pentagram.OuterValues.GetMax();
            if (innerMax > outerMax)
                pentagram.InnerValues.SetRedColor();
            else if (outerMax > innerMax)
                pentagram.OuterValues.SetRedColor();
            else 
                pentagram.OuterValues.SetRedColor();

            AddNumberToCanvas(145, 25, pentagram.OuterValues.Heat); //heat
            AddNumberToCanvas(145, 75, pentagram.InnerValues.Heat);

            AddNumberToCanvas(265, 115, pentagram.OuterValues.Humidity); //humidity
            AddNumberToCanvas(215, 115, pentagram.InnerValues.Humidity);

            AddNumberToCanvas(190, 235, pentagram.OuterValues.Dryness); //dryness
            AddNumberToCanvas(190, 195, pentagram.InnerValues.Dryness);

            AddNumberToCanvas(105, 235, pentagram.OuterValues.Cold); //cold
            AddNumberToCanvas(105, 195, pentagram.InnerValues.Cold);

            AddNumberToCanvas(25, 115, pentagram.OuterValues.Wind); //wind
            AddNumberToCanvas(75, 115, pentagram.InnerValues.Wind);
        }

        private void AddNumberToCanvas(double x, double y, DerivativeElement element)
        {
            AddTextToCanvas(x, y, element.Value.ToString(), element.IsRed ? Colors.Red : Colors.Black);
        }

        private void AddTextToCanvas(double x, double y, string text, Color? color = null)
        {
            color = color ?? Colors.Black;
            var textBlock = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush(color.Value)
            };
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvas.Children.Add(textBlock);
        }

        private void seasonCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (baZiModel == null)
            {
                MessageBox.Show("Select a birthday", "No date selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var season = calculator.GetSeason(birthDate);
            baZiModel.Season = season;
            Render();
        }

        private void seasonCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            baZiModel.Season = null;
            Render();
        }

        private void Render()
        {
            canvas.Children.Clear();

            AddPentagram();
            AddBaZiModel(baZiModel);
            pentagramModel = calculator.GetPentagram(baZiModel);
            AddPentagramValues(pentagramModel);
        }

        private void BirthDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            baZiModel = calculator.GetBaZiDateModel(birthDatePicker.SelectedDate.Value);
            Render();

            AddItemsToComboBox();
            birthDate = birthDatePicker.SelectedDate.Value;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (hourComboBox.SelectedItem != null)
            {
                var item = (ComboBoxItemModel) hourComboBox.SelectedItem;
                baZiModel.Hour = new BaZiModel {Animal = item.Value.Animal, Element = item.Value.Element};

                Render();
            }
        }

        private void EventDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            void AddPeriodToCanvas(Period period, int y)
            {
                AddTextToCanvas(300, y,
                    period.Start.ToShortDateString() + " - " + period.End.ToShortDateString() + " / " + period.Energy);
            }

            var cycleModel = GetCycleModel();
            AddPeriodToCanvas(cycleModel.Big.EventPeriod, 170);
            AddPeriodToCanvas(cycleModel.Year.EventPeriod, 190);
            AddPeriodToCanvas(cycleModel.Season.EventPeriod, 210);
            AddPeriodToCanvas(cycleModel.Month.EventPeriod, 230);
            AddPeriodToCanvas(cycleModel.Day.EventPeriod, 250);
        }

        private CycleModel GetCycleModel()
        {
            var eventDate = eventDatePicker.SelectedDate.Value;
            var builder = new CycleModelBuilder();

            return builder.Build(birthDate, eventDate);
        }
    }
}