using System.ComponentModel.DataAnnotations;

namespace BDCalculator.Models
{
    public enum Element
    {
        [Display(Name = "癸")] YinWater = 0,
        [Display(Name = "甲")] YangWood = 1,
        [Display(Name = "乙")] YinWood = 2,
        [Display(Name = "丙")] YangFire = 3,
        [Display(Name = "丁")] YinFire = 4,
        [Display(Name = "戊")] YangEarth = 5,
        [Display(Name = "己")] YinEarth = 6,
        [Display(Name = "庚")] YangMetal = 7,
        [Display(Name = "辛")] YinMetal = 8,
        [Display(Name = "壬")] YangWater = 9
    }
}