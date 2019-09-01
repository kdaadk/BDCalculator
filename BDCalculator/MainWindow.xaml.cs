using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private readonly CycleModelBuilder cycleModelBuilder = new CycleModelBuilder();
        private readonly GeometryPainter geometryPainter = new GeometryPainter();
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

        private IEnumerable<ComboBoxItemModel> GetComboBoxItems(int firstElement, int firstAnimal)
        {
            var items = new List<ComboBoxItemModel>();
            var elementNumber = firstElement;
            var animalNumber = firstAnimal;
            
            items.Add(new ComboBoxItemModel());
            

            for (var i = 0; i < 13; i++)
            {
                var element = (Element) elementNumber;
                var animal = (Animal) animalNumber;
                items.Add(new ComboBoxItemModel
                {
                    Text =
                        $"{EnumHelper<Element>.GetDisplayValue(element)} {EnumHelper<Element>.GetEnumMemberValue(element)};" +
                        $" {EnumHelper<Animal>.GetDisplayValue(animal)} {EnumHelper<Animal>.GetEnumMemberValue(animal)}",
                    Value = new BaZiModel {Animal = animal, Element = element}
                });
                elementNumber++;
                elementNumber %= 10;
                animalNumber++;
                animalNumber %= 12;
            }

            return items;
        }

        private void RenderAnimalsLine(PentagramModel model)
        {
            var arrowsData = $"{geometryPainter.GetBigArrowData(new Point(10, 14))}" +
                         $"{geometryPainter.GetBigArrowData(new Point(10, 124))}" +
                         $"{geometryPainter.GetBigArrowData(new Point(10, 234))}" +
                         $"{geometryPainter.GetSmallArrowData(new Point(65, 40))}" +
                         $"{geometryPainter.GetSmallArrowData(new Point(65, 150))}" +
                         $"{geometryPainter.GetSmallArrowData(new Point(65, 260))}";
            canvas.Children.Add(new Path
            {
                Data = Geometry.Parse(arrowsData),
                Stroke = Brushes.Black
            });
            
            var yPositions = new[] { 5, 31, 59, 85, 115, 141, 169, 195, 225, 251, 279, 305};
            var animalAbbreviations = new[] { "MC", "TR", "VB", "F", "P", "GI", "E", "RP", "C", "IG", "V", "R"};
            var valueModels = model.GetAllValueModels();
            var starData = "";

            for (var a = 0; a < 12; a++)
            {
                AddTextToCanvas(30, yPositions[a], animalAbbreviations[a], valueModels[a].Color);
                if (valueModels[a].IsWeak && valueModels[a].Name != null)
                {
                    var index = animalAbbreviations.Select((abbr, i) => new {abbr, i})
                        .Where(x => x.abbr == valueModels[a].Name).Select(x => x.i).First();
                    starData += geometryPainter.GetStarData(new Point(30, yPositions[index]));
                }
                if (valueModels[a].IsWeak && valueModels[a].Name == null)
                    starData += geometryPainter.GetStarData(new Point(30, yPositions[a]));
            }
            
            canvas.Children.Add(new Path
            {
                Data = Geometry.Parse(starData),
                Stroke = Brushes.Blue
            });
        }

        private void RenderBaZiBirthModel(BaZiDateModel baZiModel)
        {
            AddTextToCanvas(290, 10, "час");
            if (baZiModel.Hour != null)
                RenderBaZiPeriodModel("час", 290, baZiModel.Hour);

            RenderBaZiPeriodModel("день", 350, baZiModel.Date);
            RenderBaZiPeriodModel("месяц", 410, baZiModel.Month);
            RenderBaZiPeriodModel("год", 470, baZiModel.Year);
            geometryPainter.PaintRectangle(canvas, new Point(285, 50), 250, 40);
        }
        
        private void RenderBaZiEventModel(DateTime? eventDate)
        {
            if (!eventDate.HasValue)
                return;
            
            var baZiEventModel = calculator.GetBaZiDateModel(eventDate.Value);
            RenderBaZiPeriodModel("день", 650, baZiEventModel.Date);
            RenderBaZiPeriodModel("месяц", 710, baZiEventModel.Month);
            RenderBaZiPeriodModel("год", 770, baZiEventModel.Year);
            geometryPainter.PaintRectangle(canvas, new Point(645, 50), 190, 40 );
            
            RenderCycles(new Point(350, 120));
        }

        private void RenderCycles(Point point)
        {
            void RenderHeader(Point startPoint, string header) => AddTextToCanvas(startPoint.X, startPoint.Y, header);
            void RenderNamePeriod(Point startPoint, string name) => AddTextToCanvas(startPoint.X, startPoint.Y, name);

            void RenderElement(Point startPoint, DateTime date, SolidColorBrush fillColor = null)
            {
                AddTextToCanvas(startPoint.X - 35, startPoint.Y + 20, $"{date:dd.MM}");
                AddTextToCanvas(startPoint.X - 35, startPoint.Y + 35, $"{date.Year}");
                geometryPainter.PaintSmallPentagram(canvas, new Point(startPoint.X + 15, startPoint.Y + 20), fillColor);
            }

            var cycleModel = cycleModelBuilder.Build(birthDate, eventDatePicker.SelectedDate.Value);
            var cycles = cycleModel.GetAllCycles();
            var points = new[]
            {
                point, new Point(point.X + 90, point.Y), new Point(point.X + 180, point.Y),
                new Point(point.X + 270, point.Y), new Point(point.X + 360, point.Y), new Point(point.X + 450, point.Y)
            };
            var headers = new[] {"Ветер", "Тепло", "Жар", "Влажность", "Сухость", "Холод"};
            var names = new[] {"БЦ", "ГЦ", "СЦ", "МЦ", "ДЦ"};
            var colors = new[] {Brushes.Green, Brushes.Red, Brushes.Orange, Brushes.Yellow, Brushes.Gray, Brushes.Blue};

            for (var i = 0; i < 6; i++)
            {
                RenderHeader(points[i], headers[i]);
                if (i < 5)
                    RenderNamePeriod(new Point(point.X - 70, point.Y + i * 40 + 40), names[i]);

                for (var j = 0; j < 5; j++)
                {
                    var periodColor = cycles[j].EventPeriod.Number == i ? colors[i] : null;
                    RenderElement(new Point(points[i].X, points[i].Y + 40 * j + 10), cycles[j].Periods[i].Start, periodColor);
                }
            }
        }

        private void RenderBaZiPeriodModel(string period, int x, BaZiModel model)
        {
            var elementColor = ColorExtractor.Extract(model.Element);
            var animalColor = ColorExtractor.Extract(model.Animal);
            AddTextToCanvas(x, 10, period);
            AddTextToCanvas(x, 30, EnumHelper<Element>.GetEnumMemberValue(model.Element), elementColor);
            AddTextToCanvas(x, 50, EnumHelper<Element>.GetDisplayValue(model.Element), elementColor);
            AddTextToCanvas(x, 70, EnumHelper<Animal>.GetDisplayValue(model.Animal), animalColor);
            AddTextToCanvas(x, 90, EnumHelper<Animal>.GetEnumMemberValue(model.Animal), animalColor);
        }

        private void RenderPentagram(PentagramModel pentagram)
        {
            geometryPainter.PaintPentagram(canvas, new Point(170, 30));
            var points = geometryPainter.GetPentagramValuesPoints(new Point(163, 10));
            var pentagramValueModels = new[]
            {
                pentagram.OuterValues.Heat, pentagram.InnerValues.Heat,
                pentagram.OuterValues.Humidity, pentagram.InnerValues.Humidity,
                pentagram.OuterValues.Dryness, pentagram.InnerValues.Dryness,
                pentagram.OuterValues.Cold, pentagram.InnerValues.Cold,
                pentagram.OuterValues.Wind, pentagram.InnerValues.Wind
            };

            for (var i = 0; i < 10; i++)
                AddNumberToCanvas(points[i].X, points[i].Y, pentagramValueModels[i]);
            
            geometryPainter.PaintPentagram(canvas, new Point(170, 130));
            geometryPainter.PaintPentagram(canvas, new Point(170, 230));
            geometryPainter.PaintPentagram(canvas, new Point(170, 330));

        }

        private void AddNumberToCanvas(double x, double y, PentagramValueModel element) =>
            AddTextToCanvas(x, y, element.Value.ToString(), element.Color);

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
                MessageBox.Show("Выберите дату рождения", "Дата рождения не выбрана", MessageBoxButton.OK, MessageBoxImage.Warning);
                seasonCheckBox.IsChecked = false;
                return;
            }
            
            var season = calculator.GetSeason(birthDate);
            baZiModel.Season = season;
            Render();
        }

        private void seasonCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (baZiModel == null)
                return;
            baZiModel.Season = null;
            Render();
        }

        private void Render()
        {
            canvas.Children.Clear();
            pentagramModel = calculator.GetPentagram(baZiModel);
            
            RenderPentagram(pentagramModel);
            RenderBaZiBirthModel(baZiModel);
            RenderBaZiEventModel(eventDatePicker.SelectedDate);
            RenderAnimalsLine(pentagramModel);
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
            var selectedItem = (ComboBoxItemModel) hourComboBox.SelectedItem;
            if (selectedItem.Value == null)
            {
                baZiModel.Hour = null;
                Render();
            }
            else
            {
                baZiModel.Hour = new BaZiModel {Animal = selectedItem.Value.Animal, Element = selectedItem.Value.Element};
                Render();
            }
        }

        private void EventDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (eventDatePicker.SelectedDate.HasValue)
                Render();
        }
    }
}