using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;

namespace HeroBattle.Models
{
    public class HealthPotion(int healAmount = 25, string Name = "Health Potion", ItemRarity rarity = ItemRarity.Common, int value = 5)
    : Item(Name, rarity, value)
    {
        public override void Apply(Character target)
        {
            target.Heal(healAmount);
            Console.WriteLine($"💊 {target.Name} drinks {Name} and restores {healAmount} HP");
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($"- Heals {healAmount} HP");
        }
    }
}