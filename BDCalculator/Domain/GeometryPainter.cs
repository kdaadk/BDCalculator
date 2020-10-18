using System.Collections.Generic;
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

        public Point[] GetPentagramValuesPoints(Point point)
        {
            return new[]
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
        }

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

        private string GetEnergyCircleRightArrowData(Point point)
        {
            return GetEnergyCircleHorizontalArrowData(point, (66, 60));
        }

        private string GetEnergyCircleLeftArrowData(Point point)
        {
            return GetEnergyCircleHorizontalArrowData(point, (-66, -60));
        }

        public string GetEnergyCircleDownArrowData(Point point, int length)
        {
            return GetEnergyCircleVerticalArrowData(point, (length, length - 6));
        }

        public string GetEnergyCircleUpArrowData(Point point, int length)
        {
            return GetEnergyCircleVerticalArrowData(point, (-length, -length + 6));
        }

        public string GetEnergyCircleArrowsData(IReadOnlyList<Point> points)
        {
            var data = new StringBuilder();

            for (var i = 0; i < 3; i++)
            {
                data.Append(
                    $"{GetEnergyCircleLeftArrowData(new Point(points[0 + i * 4].X - 5, points[0 + i * 4].Y + 10))}");
                data.Append(
                    $"{GetEnergyCircleDownArrowData(new Point(points[1 + i * 4].X + 10, points[1 + i * 4].Y + 25), 66)}");
                data.Append(
                    $"{GetEnergyCircleRightArrowData(new Point(points[2 + i * 4].X + 25, points[2 + i * 4].Y + 10))}");
                data.Append(
                    $"{GetEnergyCircleUpArrowData(new Point(points[3 + i * 4].X + 10, points[3 + i * 4].Y - 5), 66)}");
            }

            return data.ToString();
        }

        private static string GetEnergyCircleHorizontalArrowData(Point point, (int, int) offset)
        {
            var data = new StringBuilder();
            var (far, near) = offset;

            data.Append($" M {point.X} {point.Y}");
            data.Append($" L {point.X + far} {point.Y}");
            data.Append($" L {point.X + near} {point.Y - 4}");
            data.Append($" M {point.X + far} {point.Y}");
            data.Append($" L {point.X + near} {point.Y + 4}");

            return data.ToString();
        }

        private string GetEnergyCircleVerticalArrowData(Point point, (int, int) offset)
        {
            var data = new StringBuilder();
            var (far, near) = offset;

            data.Append($" M {point.X} {point.Y}");
            data.Append($" L {point.X} {point.Y + far}");
            data.Append($" L {point.X - 4} {point.Y + near}");
            data.Append($" M {point.X} {point.Y + far}");
            data.Append($" L {point.X + 4} {point.Y + near}");

            return data.ToString();
        }
    }
}