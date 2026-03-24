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
    class Paladin : Character
    {
        public Paladin(string name)
            : base(name, HeroClass.Paladin, maxHp: 110, baseAtk: 9, defense: 4) { }

        public object CurrentHP { get; private set; }

        public override int Attack(IDamageable target)
        {
            Console.WriteLine($" ⚔️ {Name} strikes {target.Name} with holy power!");
            return base.Attack(target);
        }

        public override void UseSpecial(IDamageable target)
        {
            Console.WriteLine($" ✨ {Name} uses DIVINE SMITE!");
            Utils.Pause();

            // Heal self
            Heal(30);
            Console.WriteLine($" 💚 {Name} restores 30 HP. HP: {CurrentHP}/{MaxHP}");

            // Deal damage
            int dmg = 10;
            target.TakeDamage(dmg);
            Console.WriteLine($" 🔥 Divine energy deals {dmg} damage to {target.Name}!");
        }
    }
}