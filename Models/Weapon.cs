using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;

namespace HeroBattle.Models
{
    public class Weapon: Item
    {
      public int Damage {get; set;}
      public Element Element {get;}

      public Weapon(string name, int damage, ItemRarity rarity = ItemRarity.Common, Element element = Element.None) : base(name, rarity, damage * 3)
      {
        Damage = damage;
        Element = element;
      }

      public override void Apply(Character target)
        {
            Console.WriteLine($"⚔️ {Name} is equipped!");
        }

        public override void Describe()
        {
            base.Describe();
            var elem = Element == Element.None ? "" : $"[{Element}]";
            Console.WriteLine($"- {Damage} damage {elem}"); // - 50 damage
        }

    }
}