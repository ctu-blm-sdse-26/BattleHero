using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;

namespace HeroBattle.Models
{
    public class Armour : Item
    {
        /*
        Add a third item type: Armour. It should have a DefenseBoost property and its Apply() should increase the target character's Defense by that amount.
        */

        public int DefenseBoost { get; set; }

        public Armour(int defenseBoost, string name = "Armour", ItemRarity rarity = ItemRarity.Common): base(name, rarity, defenseBoost * 2)
        {
            DefenseBoost = defenseBoost;
        } 

        public override void Apply(Character target)
        {
            target.BaseDefense += DefenseBoost;
            Console.WriteLine($"🛡️ {Name} is equipped! Defense increased by {DefenseBoost}.");
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($"- {DefenseBoost} defense boost"); // - 20 defense boost
         }
    }
}