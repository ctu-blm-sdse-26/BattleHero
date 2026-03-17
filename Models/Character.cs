using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;
using HeroBattle.Interfaces;
using HeroBattle.Utils;

namespace HeroBattle.Models
{
    public class Character : IDescribable
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; private set; } = 1;
        public HeroClass Class {get; set;}
        public int MaxHP { get; protected set; }
        public int CurrentHP { get; private set; }
        public int BaseAttack { get; protected set; } = 15;
        public int BaseDefense { get; protected set; } = 10;
        public int Gold { get; private set; } = 0;
        public bool IsAlive => CurrentHP > 0;
        protected Weapon? EquippedWeapon;
        public Inventory<Item> Bag { get; } = new Inventory<Item>();
        private Dictionary<string, int> _stats = new Dictionary<string, int>
        {
        { "Kills", 0 },
        { "Damage", 0 },
        { "Heals", 0 },
        };
        public Character(string name, int level, HeroClass heroClass)
        {
            Name = name;
            Level = level;
            Class = heroClass;
            Health = MaxHP; 
        }
        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHP) Health = MaxHP;
        }
        public void Equip(Weapon w)
        {
        EquippedWeapon = w;
        Console.WriteLine($" 🗡 {Name} equips {w.Name} (+{w.Damage} ATK)");
        }
        public void UseItem(int index)
        {
        var all = Bag.GetAll().ToList();
        if (index < 0 || index >= all.Count) { Console.WriteLine(" ❌ Invalid item."); return; }
        all[index].Apply(this);
        Bag.Remove(all[index]);
        }
        public void EarnGold(int amount)
        {
        Gold += amount;
        Console.WriteLine($" 💰 {Name} earns {amount}g. Total: {Gold}g");
        }
        public void LevelUp()
        {
        Level++;
        MaxHP += 10;
        CurrentHP = MaxHP;
        BaseAttack += 3;
        BaseDefense += 1;
        Utils.Utils.PrintWithColor($"\n 🌟 {Name} reached Level {Level}! Statsincreased.\n", ConsoleColor.Yellow);
        }
        public virtual void Describe()
        {
        Console.WriteLine($"\n ┌─ {Name} ({Class}) – Lv {Level}");
        Console.WriteLine($" │ HP: {CurrentHP}/{MaxHP}");
        Console.WriteLine($" │ ATK: {BaseAttack + (EquippedWeapon?.Damage ?? 0)} DEF: {BaseDefense} Gold: {Gold}g");
        Console.WriteLine($" │ Weapon: {EquippedWeapon?.Name ?? "Bare hands"}");
        Console.WriteLine($" └─ Kills: {_stats["Kills"]} Dmg: {_stats["Damage"]} Healed: {_stats["Heals"]}");
        }
}
}