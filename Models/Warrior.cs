using System;
using HeroBattle.data;
using HeroBattle.Enums;
using HeroBattle.Interfaces;
using HeroBattle.Utils;

namespace HeroBattle.Models; // Match the Program.cs namespace

public class Warrior : Character

{

    // Simplified constructor - Character should handle the stats

    public Warrior(string name)

        : base(name, HeroClass.Warrior, maxHp: 120, baseAtk: 12, defense: 5)

    {
        
    }

    public void DealDamage(IDamageable target)
    {

        Console.WriteLine($" 🛡 {Name} uses SHIELD BASH!");

        Utils.Utils.Pause(); // Simplified call

        base.Attack(target);

        if (target.IsAlive)

        {

            Console.WriteLine($" 💫 Target stunned! Bonus strike!");

            Utils.Utils.Pause();

            base.Attack(target);

        }

    }

    public override void UseSpecial(IDamageable target)
    {
        Console.WriteLine($" 🛡 {Name} uses SHIELD BASH!");
        Utils.Utils.Pause();
        base.Attack(target);
        if (target.IsAlive)
        {
            Console.WriteLine($" 💫 Target stunned! Bonus strike!");
            Utils.Utils.Pause();
            base.Attack(target);
        }
    }
    public void SaveCharacter()
        {
            var db = new GameContext();
            db.Warriors.Add(this);
            db.SaveChanges();
        }
}
