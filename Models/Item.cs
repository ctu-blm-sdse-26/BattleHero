using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;
using HeroBattle.Interfaces;
using HeroBattle.Utils;

namespace HeroBattle.Models
{
    abstract public class Item: IDescribable
    {
        public string Name { get; set; }
        public ItemRarity ItemRarity { get; set; }
        public int Value { get; set; }

        protected Item(string name, ItemRarity rarity, int value)
        {
            Name = name;
            ItemRarity = rarity;
            Value = value;
        } 

// This method will be implemented by specific item types (e.g., weapons, armor, potions) to apply their effects to the target character.
        public abstract void Apply(Character target); 

        public virtual void Describe()
        {
            var colour = ItemRarity switch
            {
                ItemRarity.Common => ConsoleColor.White,
                ItemRarity.Uncommon => ConsoleColor.Green,
                ItemRarity.Rare => ConsoleColor.Blue,
                ItemRarity.Epic => ConsoleColor.Magenta,
                ItemRarity.Legendary => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };

            Utils.Utils.PrintWithColor($"[{ItemRarity}] {Name}", colour); // [Common] Sword
            Console.WriteLine($"- {Value}g"); // - 150g 
        }
    }
}