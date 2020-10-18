using System;
using System.Collections.Generic;
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
        private readonly CycleModelBuilder cycleModelBuilder = new CycleModelBuilder();
        private readonly GeometryPainter geometryPainter = new GeometryPainter();
        private BaZiDateModel birthBaZiModel;
        private DateTime birthDate;
        private BaZiDateModel eventBaZiModel;
        private BaZiDateModel receptionBaZiModel;
        private SetUpPentagramModel setUpPentagramModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddItemsToComboBox(BaZiDateModel baZiDateModel, ItemsControl hourComboBox)
        {
            hourComboBox.Items.Clear();

            if (baZiDateModel.Date == null)
                return;

            if (baZiDateModel.Date.Element == Element.YangWood || baZiDateModel.Date.Element == Element.YinEarth)
                foreach (var item in GetComboBoxItems(1, 1))
                    hourComboBox.Items.Add(item);

            if (baZiDateModel.Date.Element == Element.YinWood || baZiDateModel.Date.Element == Element.YangMetal)
                foreach (var item in GetComboBoxItems(3, 1))
                    hourComboBox.Items.Add(item);

            if (baZiDateModel.Date.Element == Element.YangFire || baZiDateModel.Date.Element == Element.YinMetal)
                foreach (var item in GetComboBoxItems(5, 1))
                    hourComboBox.Items.Add(item);

            if (baZiDateModel.Date.Element == Element.YinFire || baZiDateModel.Date.Element == Element.YangWater)
                foreach (var item in GetComboBoxItems(7, 1))
                    hourComboBox.Items.Add(item);

            if (baZiDateModel.Date.Element == Element.YangEarth || baZiDateModel.Date.Element == Element.YinWater)
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

        private void RenderAnimalsLine(SetUpPentagramModel model)
        {
            var arrowsData = $"{geometryPainter.GetBigArrowData(new Point(10, 144))}" +
                             $"{geometryPainter.GetBigArrowData(new Point(10, 254))}" +
                             $"{geometryPainter.GetBigArrowData(new Point(10, 364))}" +
                             $"{geometryPainter.GetSmallArrowData(new Point(65, 170))}" +
                             $"{geometryPainter.GetSmallArrowData(new Point(65, 280))}" +
                             $"{geometryPainter.GetSmallArrowData(new Point(65, 390))}";
            canvas.Children.Add(new Path
            {
                Data = Geometry.Parse(arrowsData),
                Stroke = Brushes.Black
            });

            var yPositions = new[] {135, 161, 189, 215, 245, 271, 299, 325, 355, 381, 409, 435};
            var animalAbbreviations = new[] {"MC", "TR", "VB", "F", "P", "GI", "E", "RP", "C", "IG", "V", "R"};
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
            AddTextToCanvas(40, 10, "час");
            if (baZiModel.Hour != null)
                RenderBaZiPeriodModel("час", 40, baZiModel.Hour);

            RenderBaZiPeriodModel("день", 90, baZiModel.Date);
            RenderBaZiPeriodModel("месяц", 150, baZiModel.Month);
            RenderBaZiPeriodModel("год", 220, baZiModel.Year);
            geometryPainter.PaintRectangle(canvas, new Point(35, 50), 250, 40);
        }

        private void RenderBaZiEventModel(DateTime? eventDate)
        {
            if (!eventDate.HasValue)
                return;

            eventBaZiModel = calculator.GetBaZiDateModel(eventDate.Value);
            RenderBaZiPeriodModel("день", 380, eventBaZiModel.Date);
            RenderBaZiPeriodModel("месяц", 440, eventBaZiModel.Month);
            RenderBaZiPeriodModel("год", 500, eventBaZiModel.Year);
            geometryPainter.PaintRectangle(canvas, new Point(375, 50), 190, 40);
        }

        private void RenderCycles(Point point)
        {
            if (eventBaZiModel == null)
                return;

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
            var names = new[] {"БЦ", "ГЦ", "СЦ", "МЦ", "СуЦ"};
            var colors = new[] {Brushes.Green, Brushes.Red, Brushes.Orange, Brushes.Yellow, Brushes.Gray, Brushes.Blue};

            for (var i = 0; i < 6; i++)
            {
                RenderHeader(points[i], headers[i]);
                for (var j = 0; j < 5; j++)
                {
                    var periodColor = cycles[j].EventPeriod.Number == i ? colors[i] : null;
                    RenderElement(new Point(points[i].X, points[i].Y + 40 * j + 10), cycles[j].Periods[i].Start,
                        periodColor);
                }
            }

            for (var i = 0; i < 5; i++)
            {
                RenderNamePeriod(new Point(point.X - 70, point.Y + i * 40 + 40), names[i]);
                var date = cycles[i].Periods[cycles[i].Periods.Count - 1].End.AddDays(-1);
                AddTextToCanvas(point.X + 500, point.Y + i * 40 + 30, $"{date:dd.MM}");
                AddTextToCanvas(point.X + 500, point.Y + i * 40 + 45, $"{date.Year}");
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

        private void RenderSetupPentagram(SetUpPentagramModel setUpPentagram)
        {
            geometryPainter.PaintPentagram(canvas, new Point(170, 160));
            var points = geometryPainter.GetPentagramValuesPoints(new Point(163, 140));
            var pentagramValueModels = setUpPentagram.GetAllValueModelsOrderBy();
            for (var i = 0; i < 10; i++)
                AddNumberToCanvas(points[i].X, points[i].Y, pentagramValueModels[i]);
        }

        private void AddNumberToCanvas(double x, double y, PentagramValueModel element)
        {
            AddTextToCanvas(x, y, element.Value.ToString(), element.Color);
        }

        private void AddTextToCanvas(double x, double y, string text, Color? color = null, double? fontSize = null)
        {
            color = color ?? Colors.Black;
            var textBlock = new TextBlock
            {
                Text = text,
                FontSize = fontSize ?? 12,
                Foreground = new SolidColorBrush(color.Value)
            };
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvas.Children.Add(textBlock);
        }

        private void seasonCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (birthBaZiModel == null)
            {
                MessageBox.Show("Выберите дату рождения", "Дата рождения не выбрана", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                seasonCheckBox.IsChecked = false;
                return;
            }

            var season = calculator.GetSeason(birthDate);
            birthBaZiModel.Season = season;
            Render();
        }

        private void seasonCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (birthBaZiModel == null)
                return;
            birthBaZiModel.Season = null;
            Render();
        }

        private void Render()
        {
            canvas.Children.Clear();
            setUpPentagramModel = calculator.GetPentagram(birthBaZiModel);

            RenderBaZiBirthModel(birthBaZiModel);
            RenderBaZiEventModel(eventDatePicker.SelectedDate);
            RenderBaZiReceptionModel(receptionDatePicker.SelectedDate);

            RenderSetupPentagram(setUpPentagramModel);
            RenderBlueBackgroundPentagram(setUpPentagramModel);

            RenderAnimalsLine(setUpPentagramModel);
            RenderCycles(new Point(350, 120));
            RenderEnergyCircle(setUpPentagramModel);
        }

        private void RenderBlueBackgroundPentagram(SetUpPentagramModel setUpPentagramModel)
        {
            if (eventBaZiModel == null)
                return;
            
            var redPointIndexesCount = new Dictionary<int, int>();
            var points = geometryPainter.GetPentagramValuesPoints(new Point(163, 280));
            var pentagramValueModels = setUpPentagramModel.GetAllValueModelsOrderBy();
            var redColorIndexes = pentagramValueModels.Select((x, i) => new {i, x})
                .Where(t => t.x.Color.IsRed()).Select(t => t.i).ToList();

            void RenderRedPoint(PeriodsModel period)
            {
                var energyIndex = MapEnergyToDefaultIndex(period.EventPeriod.Energy);
                var redPointIndex = redColorIndexes.First(x => x == energyIndex * 2 || x == energyIndex * 2 + 1);

                if (!redPointIndexesCount.ContainsKey(redPointIndex))
                {
                    if (redPointIndexesCount.Count >= 3)
                        return;
                    
                    AddTextToCanvas(points[redPointIndex].X, points[redPointIndex].Y, "o", Colors.Red);
                    redPointIndexesCount.Add(redPointIndex, 1);
                }
                else
                {
                    AddTextToCanvas(points[redPointIndex].X + redPointIndexesCount[redPointIndex] * 7,
                        points[redPointIndex].Y, "o", Colors.Red);
                    redPointIndexesCount[redPointIndex]++;
                }
            }

            geometryPainter.PaintPentagram(canvas, new Point(170, 300));
            var cycleModel = cycleModelBuilder.Build(birthDate, eventDatePicker.SelectedDate.Value);
            RenderRedPoint(cycleModel.Day);
            RenderRedPoint(cycleModel.Month);
            RenderRedPoint(cycleModel.Season);
            RenderRedPoint(cycleModel.Year);
            RenderRedPoint(cycleModel.Big);

            var point = RenderBlueBackgroundPentagramHelper.GetPoint(redPointIndexesCount.Keys, points);
            if (point.HasValue)
                AddTextToCanvas(point.Value.X, point.Value.Y, "O", Colors.Blue);
        }

        private int MapEnergyToDefaultIndex(Energy energy)
        {
            if (energy == Energy.Heat || energy == Energy.Warmth)
                return 0;
            if (energy == Energy.Humidity)
                return 1;
            if (energy == Energy.Dryness)
                return 2;
            if (energy == Energy.Cold)
                return 3;
            if (energy == Energy.Wind)
                return 4;
            throw new ArgumentException();
        }

        private void RenderEnergyCircle(SetUpPentagramModel model)
        {
            if (eventBaZiModel == null)
                return;

            var animalAbbreviations = new[] {"MC", "TR", "VB", "F", "P", "GI", "E", "RP", "C", "IG", "V", "R"};
            var valueModels = model.GetAllValueModels();

            var start = new Point(380, 400);
            var points = new List<Point>
            {
                start, new Point(start.X - 100, start.Y),
                new Point(start.X - 100, start.Y + 100), new Point(start.X, start.Y + 100),
                new Point(start.X + 150, start.Y), new Point(start.X + 50, start.Y),
                new Point(start.X + 50, start.Y + 100), new Point(start.X + 150, start.Y + 100),
                new Point(start.X + 300, start.Y), new Point(start.X + 200, start.Y),
                new Point(start.X + 200, start.Y + 100), new Point(start.X + 300, start.Y + 100)
            };

            for (var i = 0; i < points.Count; i++)
                AddTextToCanvas(points[i].X, points[i].Y, animalAbbreviations[i], valueModels[i].Color, 16);

            var arrowsData = geometryPainter.GetEnergyCircleArrowsData(points);

            canvas.Children.Add(new Path
            {
                Data = Geometry.Parse(arrowsData),
                Stroke = Brushes.Black
            });

            RenderEnergyCircleInnerText(points);

            var animalEnergyAbbrWithColor = new[]
            {
                GetAnimalAbbrWithColor(eventBaZiModel.Year), GetAnimalAbbrWithColor(eventBaZiModel.Month),
                GetAnimalAbbrWithColor(eventBaZiModel.Date)
            };
            var entryPoints = EnergyCircle.EntryPoints.Select(x => (x, new List<string>()))
                .ToDictionary(x => x.Item1, x => x.Item2);

            RenderColoredArrows(animalEnergyAbbrWithColor, animalAbbreviations, points, entryPoints);
        }

        private void RenderColoredArrows(IList<(string abbr, SolidColorBrush color)> animalEnergyAbbrWithColor,
            IReadOnlyList<string> animalAbbreviations, IReadOnlyList<Point> points,
            IReadOnlyDictionary<string, List<string>> entryPoints)
        {
            for (var i = 0; i < 3; i++)
            {
                var currentAbbr = animalEnergyAbbrWithColor[i].abbr;
                var index = animalAbbreviations.TakeWhile(x => x != currentAbbr).Count();
                var color = animalEnergyAbbrWithColor.FirstOrDefault(x => x.abbr == animalAbbreviations[index]).color;
                var coloredArrowData = "";

                if (new[] {1, 5, 9}.Contains(index))
                {
                    coloredArrowData = geometryPainter.GetEnergyCircleDownArrowData(
                        new Point(points[index].X + 10, points[index].Y + 25), 66);
                    entryPoints[currentAbbr].Add(geometryPainter.GetEnergyCircleDownArrowData(
                        new Point(points[index].X + 10 + entryPoints[currentAbbr].Count * 10, points[index].Y - 25),
                        20));
                }

                if (new[] {3, 7, 11}.Contains(index))
                {
                    coloredArrowData = geometryPainter.GetEnergyCircleUpArrowData(
                        new Point(points[index].X + 10, points[index].Y - 5), 66);
                    entryPoints[currentAbbr].Add(geometryPainter.GetEnergyCircleUpArrowData(
                        new Point(points[index].X + entryPoints[currentAbbr].Count * 10, points[index].Y + 45), 20));
                }

                canvas.Children.Add(new Path
                {
                    Data = Geometry.Parse(coloredArrowData),
                    Stroke = color,
                    StrokeThickness = 3
                });
            }

            foreach (var entryPoint in entryPoints)
                canvas.Children.Add(new Path
                {
                    Data = Geometry.Parse(string.Join("", entryPoint.Value)),
                    Stroke = Brushes.Red,
                    StrokeThickness = 1
                });
        }

        private void RenderEnergyCircleInnerText(IReadOnlyList<Point> points)
        {
            void Render(Point point, double x, double y, char letter)
            {
                AddTextToCanvas(point.X + x, point.Y + y, letter.ToString());
            }

            var names = new[] {"Цзюэ инь", "Шао ян", "Тай инь", "Ян мин", "Шао инь", "Тай ян"};
            var diff = new[]
            {
                new Point(-10, 15), new Point(20, 25), new Point(-10, 20),
                new Point(20, 25), new Point(-10, 20), new Point(20, 25)
            };

            for (var i = 0; i < 6; i++)
            {
                var point = i % 2 == 1 ? points[i * 2 - 1] : points[i * 2];
                for (var j = 0; j < names[i].Length; j++)
                    Render(point, diff[i].X, diff[i].Y + 10 * j, names[i][j]);
            }
        }

        private (string abbr, SolidColorBrush color) GetAnimalAbbrWithColor(BaZiModel year)
        {
            if (year.Animal == Animal.Tiger || year.Animal == Animal.Monkey)
                return ("TR", Brushes.Orange);
            if (year.Animal == Animal.Snake || year.Animal == Animal.Pig)
                return ("F", Brushes.LawnGreen);
            if (year.Animal == Animal.Rabbit || year.Animal == Animal.Rooster)
                return ("GI", Brushes.Gray);
            if (year.Animal == Animal.Ox || year.Animal == Animal.Goat)
                return ("RP", Brushes.Yellow);
            if (year.Animal == Animal.Dragon || year.Animal == Animal.Dog)
                return ("IG", Brushes.Blue);
            if (year.Animal == Animal.Rat || year.Animal == Animal.Horse)
                return ("R", Brushes.Red);
            throw new ArgumentException();
        }

        private void BirthDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            birthBaZiModel = calculator.GetBaZiDateModel(birthDatePicker.SelectedDate.Value);
            Render();

            AddItemsToComboBox(birthBaZiModel, birthHourComboBox);
            birthDate = birthDatePicker.SelectedDate.Value;
        }

        private void BirthHourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ComboBoxItemModel) birthHourComboBox.SelectedItem;
            if (selectedItem?.Value == null)
            {
                birthBaZiModel.Hour = null;
                Render();
            }
            else
            {
                birthBaZiModel.Hour = new BaZiModel
                    {Animal = selectedItem.Value.Animal, Element = selectedItem.Value.Element};
                Render();
            }
        }

        private void EventDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (eventDatePicker.SelectedDate.HasValue)
                Render();
        }

        private void ReceptionDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!receptionDatePicker.SelectedDate.HasValue)
                return;

            receptionBaZiModel = calculator.GetBaZiDateModel(receptionDatePicker.SelectedDate.Value);
            AddItemsToComboBox(receptionBaZiModel, receptionHourComboBox);
            Render();
        }

        private void RenderBaZiReceptionModel(DateTime? receptionDate)
        {
            if (!receptionDate.HasValue)
                return;

            if (receptionBaZiModel.Hour != null)
                RenderBaZiPeriodModel("час", 650, receptionBaZiModel.Hour);

            RenderBaZiPeriodModel("день", 710, receptionBaZiModel.Date);
            AddTextToCanvas(650, 10, "час");
            geometryPainter.PaintRectangle(canvas, new Point(645, 50), 130, 40);
        }

        private void ReceptionHourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ComboBoxItemModel) receptionHourComboBox.SelectedItem;
            if (selectedItem?.Value == null)
            {
                receptionBaZiModel.Hour = null;
                Render();
            }
            else
            {
                receptionBaZiModel.Hour = new BaZiModel
                    {Animal = selectedItem.Value.Animal, Element = selectedItem.Value.Element};
                Render();
            }
        }
    }
}