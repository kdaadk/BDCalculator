using System;
using System.Collections.Generic;
using BDCalculator.Models;

namespace BDCalculator.Domain
{
    public class BaZiCalculator
    {
        private const int SumElements = 6;

        public PentagramModel GetPentagram(DateTime bDate)
        {
            var date = CalculateDate(bDate.Date);
            var month = CalculateMonth(bDate.Date);
            var year = CalculateYear(bDate.Date);
            var baZiDate = new BaZiDateModel
            {
                Date = new BaZiModel {Animal = date.animal, Element = date.element},
                Month = new BaZiModel {Animal = month.animal, Element = month.element},
                Year = new BaZiModel {Animal = year.animal, Element = year.element}
            };
            
            return GetPentagram(baZiDate);
        }

        private PentagramModel GetPentagram(BaZiDateModel baZiDate)
        {
            var yearPentagramValues = GetValuesBy(baZiDate.Year);
            var monthPentagramValues = GetValuesBy(baZiDate.Month);
            var datePentagramValues = GetValuesBy(baZiDate.Date);

            var outerAnimalYear = yearPentagramValues.animalsPentagram.OuterValues;
            var innerAnimalYear = yearPentagramValues.animalsPentagram.InnerValues;
            var outerElementYear = yearPentagramValues.elementsPentagram.OuterValues;
            var innerElementYear = yearPentagramValues.elementsPentagram.InnerValues;

            var outerAnimalMonth = monthPentagramValues.animalsPentagram.OuterValues;
            var innerAnimalMonth = monthPentagramValues.animalsPentagram.InnerValues;
            var outerElementMonth = monthPentagramValues.elementsPentagram.OuterValues;
            var innerElementMonth = monthPentagramValues.elementsPentagram.InnerValues;

            var outerAnimalDate = datePentagramValues.animalsPentagram.OuterValues;
            var innerAnimalDate = datePentagramValues.animalsPentagram.InnerValues;
            var outerElementDate = datePentagramValues.elementsPentagram.OuterValues;
            var innerElementDate = datePentagramValues.elementsPentagram.InnerValues;

            var outerSum = outerAnimalYear + outerElementYear
                                           + outerAnimalMonth + outerElementMonth
                                           + outerAnimalDate + outerElementDate;
            var innerSum = innerAnimalYear + innerElementYear
                                           + innerAnimalMonth + innerElementMonth
                                           + innerAnimalDate + innerElementDate;

            return new PentagramModel {InnerValues = innerSum, OuterValues = outerSum};
        }

        private (Elements element, Animals animal) CalculateDate(DateTime bDate)
        {
            var minDate = new DateTime(1900, 04, 20);
            var diff = (bDate.Date - minDate).TotalDays;
            var loopLength = (int) diff % 60;
            var element = loopLength % 10;
            var animal = loopLength % 12;

            return ((Elements) element, (Animals) animal);
        }

        private (Elements element, Animals animal) CalculateMonth(DateTime bDate)
        {
            var minDate = new DateTime(1903, 11, 17);
            var chineseBirthDate = GetChineseBirthDate(bDate);
            var diff = chineseBirthDate.Year * 12 + chineseBirthDate.Month - (minDate.Year * 12 + minDate.Month);
            var loopLength = diff % 60;
            var element = loopLength % 10;
            var animal = loopLength % 12;

            return ((Elements) element, (Animals) animal);
        }

        private DateTime GetChineseBirthDate(DateTime bDate)
        {
            var transformer = new ChineseDateTransformer();
            return transformer.Transform(bDate);
        }

        private (Elements element, Animals animal) CalculateYear(DateTime bDate)
        {
            var minYear = 1863;
            var beforeBorder = 0;
            if (bDate.Day < 4 && bDate.Month == 2)
                beforeBorder = -1;

            var diff = bDate.Year - minYear + beforeBorder;
            var loopLength = diff % 60;
            var element = loopLength % 10;
            var animal = loopLength % 12;

            return ((Elements) element, (Animals) animal);
        }

        private (PentagramModel elementsPentagram, PentagramModel animalsPentagram) GetValuesBy(BaZiModel source)
        {
            var elementsPentagram = GetPentagramValuesByElements(source);
            var animalsPentagram = GetPentagramValuesByAnimals(source);

            return (elementsPentagram, animalsPentagram);
        }

        private PentagramModel GetPentagramValuesByElements(BaZiModel year)
        {
            switch (year.Element)
            {
                case (Elements) 1:
                    return SetValues(new[] {4, 2, 1, 3, 5});
                case (Elements) 3:
                    return SetValues(new[] {5, 4, 2, 1, 3});
                case (Elements) 5:
                    return SetValues(new[] {3, 5, 4, 2, 1});
                case (Elements) 7:
                    return SetValues(new[] {1, 3, 5, 4, 2});
                case (Elements) 9:
                    return SetValues(new[] {2, 1, 3, 5, 4});
            }

            switch (year.Element)
            {
                case (Elements) 0:
                    return SetValues(new[] {4, 5, 3, 1, 2});
                case (Elements) 2:
                    return SetValues(new[] {2, 4, 5, 3, 1});
                case (Elements) 4:
                    return SetValues(new[] {1, 2, 4, 5, 3});
                case (Elements) 6:
                    return SetValues(new[] {4, 5, 3, 1, 2});
                case (Elements) 8:
                    return SetValues(new[] {5, 3, 1, 2, 4});
            }

            throw new ArgumentException();
        }

        private PentagramModel GetPentagramValuesByAnimals(BaZiModel year)
        {
            switch (year.Animal)
            {
                case (Animals) 0:
                    return SetValues(new[] {5, 4, 2, 1, 3});
                case (Animals) 1:
                    return SetValues(new[] {4, 2, 1, 3, 5});
                case (Animals) 4:
                    return SetValues(new[] {1, 3, 5, 4, 2});
                case (Animals) 5:
                    return SetValues(new[] {3, 5, 4, 2, 1});
                case (Animals) 8:
                    return SetValues(new[] {5, 4, 2, 1, 3});
                case (Animals) 9:
                    return SetValues(new[] {2, 1, 3, 5, 4});
            }

            switch (year.Animal)
            {
                case (Animals) 2:
                    return SetValues(new[] {2, 4, 5, 3, 1});
                case (Animals) 3:
                    return SetValues(new[] {5, 3, 1, 2, 4});
                case (Animals) 6:
                    return SetValues(new[] {3, 1, 2, 4, 5});
                case (Animals) 7:
                    return SetValues(new[] {1, 2, 4, 5, 3});
                case (Animals) 10:
                    return SetValues(new[] {4, 5, 3, 1, 2});
                case (Animals) 11:
                    return SetValues(new[] {1, 2, 4, 5, 3});
            }

            throw new ArgumentException();
        }

        private PentagramModel SetValues(IReadOnlyList<int> values)
        {
            var outer = new PentagramValuesModel
            {
                Heat = values[0],
                Humidity = values[1],
                Dryness = values[2],
                Cold = values[3],
                Wind = values[4]
            };
            var inner = new PentagramValuesModel
            {
                Heat = SumElements - values[0],
                Humidity = SumElements - values[1],
                Dryness = SumElements - values[2],
                Cold = SumElements - values[3],
                Wind = SumElements - values[4]
            };
            return new PentagramModel {OuterValues = outer, InnerValues = inner};
        }
    }
}