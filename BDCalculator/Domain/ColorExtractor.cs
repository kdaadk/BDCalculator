using System;
using System.Windows.Media;
using BDCalculator.Models;

namespace BDCalculator.Domain
{
    public static class ColorExtractor
    {
        public static Color Extract(Element element)
        {
            if (element == Element.YangFire || element == Element.YinFire)
                return Colors.Red;
            
            if (element == Element.YangMetal || element == Element.YinMetal)
                return Colors.Gray;
            
            if (element == Element.YangWater || element == Element.YinWater)
                return Colors.Blue;
            
            if (element == Element.YangWood || element == Element.YinWood)
                return Colors.LimeGreen;
            
            if (element == Element.YangEarth || element == Element.YinEarth)
                return Colors.SaddleBrown;

            throw new ArgumentException();
        }
        
        public static Color Extract(Animal animal)
        {
            if (animal == Animal.Dog || animal == Animal.Goat || animal == Animal.Dragon || animal == Animal.Ox)
                return Colors.SaddleBrown;
            
            if (animal == Animal.Horse || animal == Animal.Snake)
                return Colors.Red;
            
            if (animal == Animal.Tiger || animal == Animal.Rabbit)
                return Colors.LimeGreen;
            
            if (animal == Animal.Rat || animal == Animal.Pig)
                return Colors.Blue;
            
            if (animal == Animal.Monkey || animal == Animal.Rooster)
                return Colors.Gray;

            throw new ArgumentException();
        }
    }
}