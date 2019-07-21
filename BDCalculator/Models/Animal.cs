using System.ComponentModel.DataAnnotations;

namespace BDCalculator.Models
{
    public enum Animal
    {
        [Display(Name = "亥")] Pig = 0,
        [Display(Name = "子")] Rat = 1,
        [Display(Name = "丑")] Ox = 2,
        [Display(Name = "寅")] Tiger = 3,
        [Display(Name = "卯")] Rabbit = 4,
        [Display(Name = "辰")] Dragon = 5,
        [Display(Name = "巳")] Snake = 6,
        [Display(Name = "午")] Horse = 7,
        [Display(Name = "未")] Goat = 8,
        [Display(Name = "申")] Monkey = 9,
        [Display(Name = "酉")] Rooster = 10,
        [Display(Name = "戌")] Dog = 11
    }
}