using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;
using HeroBattle.Interfaces;

namespace HeroBattle.Models
{
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; set; } = 1;
        public HeroClass Class {get; set;}
        public int MaxHP => Level * 100;
        public int BaseAttack { get; set; } = 15;
        public int BaseDefense { get; set; } = 10;
        public int Gold { get; set; } = 0;
        public bool IsAlive => Health > 0;

        public Character(string name, int level, HeroClass heroClass)
        {
            Name = name;
            Level = level;
            Class = heroClass;
            Health = MaxHP; 
        }

        // --IDamageable--
        public void TakeDamage(int amount)
        {
            int reduced = Math.Max(0, amount - BaseDefense);
            Health = Utils.Utils.Clamp(Health - reduced, 0, MaxHP);
            Utils.Utils.PrintWithColor($" {Name} takes {reduced} damage (block {amount - reduced}). HP: {Health}/{MaxHP}\n", 
            Health < MaxHP / 4 ? ConsoleColor.Red : ConsoleColor.DarkYellow);
        }

         public void Heal(int amount)
        {
            int before = Health;
            Health = Utils.Utils.Clamp(Health + amount, 0, MaxHP);
            _stats["Heals"] += Health - before;
        }

        // --IAtacker--
        public virtual int Attack(IDamageable target)
        {
            int dmg = BaseAttack + (EquippedWeapon?.Damage ?? 0) + Rng.Next(-3, 4);
            dmg = Math.Max(1, dmg);
            target.TakeDamage(dmg);
            _stats["Damage"] += dmg;
            if (!target.IsAlive) _stats["Kills"]++;
            return dmg;
        }

        // --Abstract Special--
        public abstract void UseSpecial(IDamageable target);
    }


}