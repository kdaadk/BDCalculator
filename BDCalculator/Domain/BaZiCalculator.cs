using System;
using System.Collections.Generic;
using BDCalculator.Models;

namespace BDCalculator.Domain
{
    public class BaZiCalculator
    {
        private const int SumElements = 6;

        public BaZiDateModel GetBaZiDateModel(DateTime bDate)
        {
            var date = CalculateDate(bDate.Date);
            var month = CalculateMonth(bDate.Date);
            var year = CalculateYear(bDate.Date);
            return new BaZiDateModel
            {
                Date = new BaZiModel {Animal = date.animal, Element = date.element},
                Month = new BaZiModel {Animal = month.animal, Element = month.element},
                Year = new BaZiModel {Animal = year.animal, Element = year.element}
            };
        }

        public PentagramModel GetPentagram(BaZiDateModel baZiDate)
        {
            var yearPentagramModel = GetPentagramModelBy(baZiDate.Year);
            var monthPentagramModel = GetPentagramModelBy(baZiDate.Month);
            var datePentagramModel = GetPentagramModelBy(baZiDate.Date);
            var hourPentagramModel = GetPentagramModelBy(baZiDate.Hour);
            var seasonPentagramModel = GetPentagramModelBy(baZiDate.Season);

            return yearPentagramModel + monthPentagramModel + datePentagramModel + hourPentagramModel +
                   seasonPentagramModel;
        }

        private PentagramModel GetPentagramModelBy(BaZiModel baZiModel)
        {
            if (baZiModel == null)
                return new PentagramModel
                    {InnerValues = new PentagramValuesModel(), OuterValues = new PentagramValuesModel()};

            var pentagramValues = GetValuesBy(baZiModel);

            var outerAnimal = pentagramValues.animalsPentagram.OuterValues;
            var innerAnimal = pentagramValues.animalsPentagram.InnerValues;
            var outerElement = pentagramValues.elementsPentagram.OuterValues;
            var innerElement = pentagramValues.elementsPentagram.InnerValues;

            return new PentagramModel
                {InnerValues = innerAnimal + innerElement, OuterValues = outerAnimal + outerElement};
        }

        private (Element element, Animal animal) CalculateDate(DateTime bDate)
        {
            var minDate = new DateTime(1900, 04, 20);
            var diff = (bDate.Date - minDate).TotalDays;
            var loopLength = (int) diff % 60;
            var element = loopLength % 10;
            var animal = loopLength % 12;

            return ((Element) element, (Animal) animal);
        }

        private (Element element, Animal animal) CalculateMonth(DateTime bDate)
        {
            var minDate = new DateTime(1903, 11, 17);
            var chineseBirthDate = GetChineseBirthDate(bDate);
            var diff = bDate.Year * 12 + chineseBirthDate.Month - (minDate.Year * 12 + minDate.Month);
            var loopLength = diff % 60;
            var element = loopLength % 10;
            var animal = loopLength % 12;

            return ((Element) element, (Animal) animal);
        }

        public DateTime GetChineseBirthDate(DateTime bDate)
        {
            var transformer = new ChineseDateTransformer();
            return transformer.Transform(bDate);
        }

        private (Element element, Animal animal) CalculateYear(DateTime bDate)
        {
            var minYear = 1863;
            var beforeBorder = 0;
            if (bDate.Month == 1 || bDate.Day < 4 && bDate.Month == 2)
                beforeBorder = -1;

            var diff = bDate.Year - minYear + beforeBorder;
            var loopLength = diff % 60;
            var element = loopLength % 10;
            var animal = loopLength % 12;

            return ((Element) element, (Animal) animal);
        }

        private (PentagramModel elementsPentagram, PentagramModel animalsPentagram) GetValuesBy(BaZiModel source)
        {
            var elementsPentagram = GetPentagramValuesByElements(source.Element);
            var animalsPentagram = GetPentagramValuesByAnimals(source.Animal);

            return (elementsPentagram, animalsPentagram);
        }

        private PentagramModel GetPentagramValuesByElements(Element element)
        {
            switch (element)
            {
                case (Element) 1:
                    return SetValues(new[] {4, 2, 1, 3, 5});
                case (Element) 3:
                    return SetValues(new[] {5, 4, 2, 1, 3});
                case (Element) 5:
                    return SetValues(new[] {3, 5, 4, 2, 1});
                case (Element) 7:
                    return SetValues(new[] {1, 3, 5, 4, 2});
                case (Element) 9:
                    return SetValues(new[] {2, 1, 3, 5, 4});
            }

            switch (element)
            {
                case 0:
                    return SetValues(new[] {4, 5, 3, 1, 2});
                case (Element) 2:
                    return SetValues(new[] {2, 4, 5, 3, 1});
                case (Element) 4:
                    return SetValues(new[] {1, 2, 4, 5, 3});
                case (Element) 6:
                    return SetValues(new[] {3, 1, 2, 4, 5});
                case (Element) 8:
                    return SetValues(new[] {5, 3, 1, 2, 4});
            }

            throw new ArgumentException();
        }

        private PentagramModel GetPentagramValuesByAnimals(Animal animal)
        {
            switch (animal)
            {
                case 0:
                    return SetValues(new[] {5, 4, 2, 1, 3});
                case (Animal) 1:
                    return SetValues(new[] {4, 2, 1, 3, 5});
                case (Animal) 4:
                    return SetValues(new[] {1, 3, 5, 4, 2});
                case (Animal) 5:
                    return SetValues(new[] {3, 5, 4, 2, 1});
                case (Animal) 8:
                    return SetValues(new[] {5, 4, 2, 1, 3});
                case (Animal) 9:
                    return SetValues(new[] {2, 1, 3, 5, 4});
            }

            switch (animal)
            {
                case (Animal) 2:
                    return SetValues(new[] {2, 4, 5, 3, 1});
                case (Animal) 3:
                    return SetValues(new[] {5, 3, 1, 2, 4});
                case (Animal) 6:
                    return SetValues(new[] {3, 1, 2, 4, 5});
                case (Animal) 7:
                    return SetValues(new[] {1, 2, 4, 5, 3});
                case (Animal) 10:
                    return SetValues(new[] {4, 5, 3, 1, 2});
                case (Animal) 11:
                    return SetValues(new[] {1, 2, 4, 5, 3});
            }

            throw new ArgumentException();
        }

        private PentagramModel SetValues(IReadOnlyList<int> values)
        {
            var outer = new PentagramValuesModel
            (
                values[0],
                values[1],
                values[2],
                values[3],
                values[4]
            );
            var inner = new PentagramValuesModel
            (
                SumElements - values[0],
                SumElements - values[1],
                SumElements - values[2],
                SumElements - values[3],
                SumElements - values[4]
            );
            return new PentagramModel {OuterValues = outer, InnerValues = inner};
        }

        public BaZiModel GetSeason(DateTime dateTime)
        {
            DateTime DateTime(int month, int day)
            {
                return new DateTime(dateTime.Year, month, day);
            }

            bool DoesDateInclude(int beginBorderMonth, int beginBorderDay, int endBorderMonth, int endBorderDay)
            {
                return dateTime >= DateTime(beginBorderMonth, beginBorderDay) &&
                       dateTime <= DateTime(endBorderMonth, endBorderDay);
            }

            Animal animal;

            if (DoesDateInclude(2, 4, 4, 3))
                animal = Animal.Ox;

            else if (DoesDateInclude(4, 4, 6, 5))
                animal = Animal.Horse;

            else if (DoesDateInclude(6, 6, 8, 6))
                animal = Animal.Dog;

            else if (DoesDateInclude(8, 7, 10, 7))
                animal = Animal.Snake;

            else if (DoesDateInclude(10, 8, 12, 6))
                animal = Animal.Tiger;

            else
                animal = Animal.Rooster;

            Element element;

            if (DoesDateInclude(3, 11, 5, 21))
                element = Element.YinWood;

            else if (DoesDateInclude(5, 22, 6, 8))
                element = Element.YinEarth;

            else if (DoesDateInclude(6, 9, 8, 20))
                element = Element.YinFire;

            else if (DoesDateInclude(8, 21, 9, 7))
                element = Element.YinEarth;

            else if (DoesDateInclude(9, 8, 11, 19))
                element = Element.YinMetal;

            else if (DoesDateInclude(11, 20, 12, 6))
                element = Element.YinEarth;

            else if (DoesDateInclude(2, 20, 3, 10))
                element = Element.YinEarth;

            else
                element = Element.YinWater;

            return new BaZiModel {Element = element, Animal = animal};
        }
    }
}