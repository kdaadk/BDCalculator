using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BDCalculator.Models
{
    public enum Animal
    {
        [Display(Name = "亥/-в")]
        [EnumMember(Value = "Свинья")]
        Pig = 0,
        
        [Display(Name = "子/+В")]
        [EnumMember(Value = "Крыса")]
        Rat = 1,
        
        [Display(Name = "丑/-п")]
        [EnumMember(Value = "Бык")]
        Ox = 2,
        
        [Display(Name = "寅/+Д")]
        [EnumMember(Value = "Тигр")]
        Tiger = 3,
        
        [Display(Name = "卯/-д")]
        [EnumMember(Value = "Кролик")]
        Rabbit = 4,
        
        [Display(Name = "辰/+П")]
        [EnumMember(Value = "Дракон")]
        Dragon = 5,
        
        [Display(Name = "巳/-о")]
        [EnumMember(Value = "Змея")]
        Snake = 6,
        
        [Display(Name = "午/+О")]
        [EnumMember(Value = "Лошадь")]
        Horse = 7,
        
        [Display(Name = "未/-п'")]
        [EnumMember(Value = "Коза")]
        Goat = 8,
        
        [Display(Name = "申/+М")]
        [EnumMember(Value = "Обезьяна")]
        Monkey = 9,
        
        [Display(Name = "酉/-м")]
        [EnumMember(Value = "Петух")]
        Rooster = 10,
        
        [Display(Name = "戌/+П'")]
        [EnumMember(Value = "Собака")]
        Dog = 11
    }
}