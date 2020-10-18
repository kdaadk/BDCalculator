using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BDCalculator.Domain
{
    public static class RenderBlueBackgroundPentagramHelper
    {

        private static Point? Helper(Dictionary<int, int>.KeyCollection redIndexes, Point[] points, List<Func<int, bool>> func)
        {
            for (var i = 0; i < 5; i++)
            {
                if (func.Any(x => x.Invoke(i)))
                    return points[(i * 2 + 8) % 10];
            }

            return null;
        }
        
        public static Point? GetPoint(Dictionary<int, int>.KeyCollection redIndexes, Point[] points)
        {
            bool GetPoint(List<Func<int, int, bool>> conditionFunctions, Func<int, int> resultFunction, out Point point)
            {
                for (var i = 0; i < 5; i++)
                    if (conditionFunctions.All(condition => redIndexes.Any(x => condition.Invoke(i, x))))
                    {
                        point = points[resultFunction(i)];
                        return true;
                    }

                point = new Point();
                return false;
            }
            
            var outerRedIndexesCount = redIndexes.Count(x => x % 2 == 0);
            var innerRedIndexesCount = redIndexes.Count(x => x % 2 == 1);
            var maxCount = Math.Max(outerRedIndexesCount, innerRedIndexesCount);
            List<Func<int, int, bool>> conditions;
            Point result;
            
            if (maxCount == 3)
            {
                conditions = new List<Func<int, int, bool>>
                    {(x, y) => y == 2 * x, (x, y) => y == (2 * x + 2) % 10, (x, y) => y == (2 * x + 4) % 10};
                if (GetPoint(conditions,x => (x * 2 + 8) % 10, out result))
                    return result;
                
                conditions = new List<Func<int, int, bool>>
                    {(x, y) => y == 2 * x + 1, (x, y) => y == (2 * x + 3) % 10, (x, y) => y == (2 * x + 5) % 10};
                if (GetPoint(conditions,x => (x * 2 + 9) % 10, out result))
                    return result;
            }

            conditions = new List<Func<int, int, bool>> {(x, y) => y == 2 * x, (x, y) => y == (2 * x + 2) % 10};
            if (GetPoint(conditions,x => (x * 2 + 6) % 10, out result))
                return result;
            
            conditions = new List<Func<int, int, bool>> {(x, y) => y == 2 * x + 1, (x, y) => y == (2 * x + 3) % 10};
            if (GetPoint(conditions,x => (x * 2 + 7) % 10, out result))
                return result;
            
            conditions = new List<Func<int, int, bool>> {(x, y) => y == 2 * x, (x, y) => y == (2 * x + 4) % 10};
            if (GetPoint(conditions,x => (x * 2 + 8) % 10, out result))
                return result;
            
            conditions = new List<Func<int, int, bool>> {(x, y) => y == 2 * x + 1, (x, y) => y == (2 * x + 5) % 10};
            if (GetPoint(conditions,x => (x * 2 + 9) % 10, out result))
                return result;
            
            conditions = new List<Func<int, int, bool>> {(x, y) => y == 2 * x, (x, y) => y == (2 * x + 5) % 10};
            if (GetPoint(conditions,x => (x * 2 + 4) % 10, out result))
                return result;
            
            conditions = new List<Func<int, int, bool>> {(x, y) => y == 2 * x + 1, (x, y) => y == (2 * x + 4) % 10};
            if (GetPoint(conditions,x => (x * 2 + 5) % 10, out result))
                return result;
            
            conditions = new List<Func<int, int, bool>> {(x, y) => y == 2 * x, (x, y) => y == (2 * x + 3) % 10};
            if (GetPoint(conditions,x => (x * 2 + 4) % 10, out result))
                return result;
            
            conditions = new List<Func<int, int, bool>> {(x, y) => y == 2 * x + 1, (x, y) => y == (2 * x + 2) % 10};
            if (GetPoint(conditions,x => (x * 2 + 5) % 10, out result))
                return result;

            return null;
        }
    }
}