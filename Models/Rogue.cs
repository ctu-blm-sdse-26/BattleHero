using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;
using HeroBattle.Interfaces;
using HeroBattle.Models;
using HeroBattle.Utils;

namespace BattleHero.Models
{
    public class Rogue : Character
    {
        public Rogue(string name)
        : base(name, HeroClass.Rogue, maxHp: 90, baseAtk: 10, defense: 2) { }

        public object CurrentHP { get; private set; }
        public int BaseAtk { get; private set; }

public override int Attack(IDamageable target)
{
    bool crit = Rng.Next(100) < 25; // 25% crit chance
    if (crit)
        Console.WriteLine($" 🗡🗡 {Name} lands a CRITICAL STRIKE on {target.Name}!");
    else
        Console.WriteLine($" 🗡 {Name} stabs {target.Name}!");
    int dmg = base.Attack(target);
    if (crit) target.TakeDamage(dmg); // double damage on crit
    return dmg;
}
        public override void UseSpecial(IDamageable target)
        {
            Console.WriteLine($" 💨 {Name} throws a SMOKE BOMB and vanishes!");
            Utils.Pause();
            Heal(25);
            Console.WriteLine($" 💚 {Name} recovers 25 HP. HP: {CurrentHP}/{MaxHP}");
            Console.WriteLine($" 👁 Reappearing for a backstab!");
            Utils.Pause();
            int dmg = BaseAtk * 2 + Rng.Next(0, 10);
            target.TakeDamage(dmg);
            Console.WriteLine($" Backstab deals {dmg} damage!");
        }
    }
}