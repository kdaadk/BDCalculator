using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BDCalculator.Domain
{
    public class GeometryPainter
    {
        public string GetBigArrowData(Point point)
        {
            var data = new StringBuilder();

            data.Append($" M {point.X} {point.Y}");
            data.Append($" L {point.X + 16} {point.Y}");
            data.Append($" L {point.X + 10} {point.Y - 4}");
            data.Append($" M {point.X + 16} {point.Y}");
            data.Append($" L {point.X + 10} {point.Y + 4}");

            data.Append($" M {point.X} {point.Y}");
            data.Append($" L {point.X} {point.Y + 80}");

            data.Append($" L {point.X + 16} {point.Y + 80}");
            data.Append($" L {point.X + 10} {point.Y + 76}");
            data.Append($" M {point.X + 16} {point.Y + 80}");
            data.Append($" L {point.X + 10} {point.Y + 84}");

            return data.ToString();
        }
            
        public string GetSmallArrowData(Point point)
        {
            var data = new StringBuilder();

            data.Append($" M {point.X} {point.Y}");
            data.Append($" L {point.X - 16} {point.Y}");
            data.Append($" L {point.X - 10} {point.Y - 4}");
            data.Append($" M {point.X - 16} {point.Y}");
            data.Append($" L {point.X - 10} {point.Y + 4}");

            data.Append($" M {point.X} {point.Y}");
            data.Append($" L {point.X} {point.Y + 28}");

            data.Append($" L {point.X - 16} {point.Y + 28}");
            data.Append($" L {point.X - 10} {point.Y + 24}");
            data.Append($" M {point.X - 16} {point.Y + 28}");
            data.Append($" L {point.X - 10} {point.Y + 32}");

            return data.ToString();
        }

        public string GetStarData(Point point)
        {
            var data = new StringBuilder();
            data.Append($" M {point.X} {point.Y}");
            data.Append($" L {point.X - 5} {point.Y - 5}");
            data.Append($" M {point.X + 10} {point.Y}");
            data.Append($" L {point.X + 15} {point.Y - 5}");
            data.Append($" M {point.X} {point.Y + 15}");
            data.Append($" L {point.X - 5} {point.Y + 20}");
            data.Append($" M {point.X + 10} {point.Y + 15}");
            data.Append($" L {point.X + 15} {point.Y + 20}");
            return data.ToString();
        }
        
        public void PaintPentagram(Canvas canvas, Point point)
        {
            canvas.Children.Add(new Polygon
            {
                Points = new PointCollection
                {
                    point,
                    new Point(point.X - 50, point.Y + 37),
                    new Point(point.X - 25, point.Y + 87),
                    new Point(point.X + 25, point.Y + 87),
                    new Point(point.X + 50, point.Y + 37)
                },
                Stroke = Brushes.Black,
                StrokeThickness = 2
            });
        }
        
        public void PaintSmallPentagram(Canvas canvas, Point point, SolidColorBrush fillColor)
        {
            canvas.Children.Add(new Polygon
            {
                Points = new PointCollection
                {
                    point,
                    new Point(point.X - 15, point.Y + 9),
                    new Point(point.X - 9, point.Y + 29),
                    new Point(point.X + 9, point.Y + 29),
                    new Point(point.X + 15, point.Y + 9)
                },
                Stroke = Brushes.Black,
                Fill = fillColor,
                StrokeThickness = 2
            });
        }
        
        public Point[] GetPentagramValuesPoints(Point point) =>
            new[]
            {
                point, //heat
                new Point(point.X, point.Y + 27),
                new Point(point.X + 60, point.Y + 48), //humidity
                new Point(point.X + 35, point.Y + 48),
                new Point(point.X + 26, point.Y + 107), //dryness
                new Point(point.X + 20, point.Y + 88),
                new Point(point.X - 25, point.Y + 107), //cold
                new Point(point.X - 20, point.Y + 88),
                new Point(point.X - 60, point.Y + 48), //wind
                new Point(point.X - 35, point.Y + 48)
            };

        public void PaintRectangle(Canvas canvas, Point start, int width, int height)
        {
            canvas.Children.Add(new Polygon
                        {
                            Points = new PointCollection
                            {
                                start,
                                new Point(start.X + width, start.Y),
                                new Point(start.X + width, start.Y + height),
                                new Point(start.X, start.Y + height),
                                new Point(start.X, start.Y)
                            },
                            Stroke = Brushes.Black,
                            StrokeThickness = 1
                        });
        }
    }
}