using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BDCalculator.Models
{
    public enum Element
    {
        [Display(Name = "癸/-в")] [EnumMember(Value = "Вода Инь")]
        YinWater = 0,

        [Display(Name = "甲/+Д")] [EnumMember(Value = "Дерево Ян")]
        YangWood = 1,

        [Display(Name = "乙/-д")] [EnumMember(Value = "Дерево Инь")]
        YinWood = 2,

        [Display(Name = "丙/+О")] [EnumMember(Value = "Огонь Ян")]
        YangFire = 3,

        [Display(Name = "丁/-о")] [EnumMember(Value = "Огонь Инь")]
        YinFire = 4,

        [Display(Name = "戊/+П")] [EnumMember(Value = "Почва Ян")]
        YangEarth = 5,

        [Display(Name = "己/-п")] [EnumMember(Value = "Почва Инь")]
        YinEarth = 6,

        [Display(Name = "庚/+М")] [EnumMember(Value = "Металл Ян")]
        YangMetal = 7,

        [Display(Name = "辛/-м")] [EnumMember(Value = "Металл Инь")]
        YinMetal = 8,

        [Display(Name = "壬/+В")] [EnumMember(Value = "Вода Ян")]
        YangWater = 9
    }
}