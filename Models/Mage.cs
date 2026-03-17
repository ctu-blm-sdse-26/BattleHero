using System;
using HeroBattle.Enums;
using HeroBattle.Interfaces;
using HeroBattle.Utils;

namespace HeroBattle.Models;

public class Mage : Character
{
    private int _mana;  // Track current mana separately

    public Mage(string name)
        : base(name, HeroClass.Mage, maxHp: 80, baseAtk: 8, defense: 1)
    {
        // Set the base class MaxMana
        MaxMana = 120;  // Mages have more mana
        _mana = MaxMana;
        // Can't set CurrentMana directly - it's private set
        // Instead, we'll use RestoreMana to set initial mana
        RestoreMana(MaxMana);
    }

    public override int Attack(IDamageable target)
    {
        Console.WriteLine($" 🔮 {Name} casts Magic Missile at {target.Name}!");
        return base.Attack(target);
    }

    public override void UseSpecial(IDamageable target)
    {
        if (_mana < 30) 
        { 
            Console.WriteLine($" ❌ {Name} is out of mana!"); 
            return; 
        }
        
        _mana -= 30;
        UseMana(30);  // Use the base class mana system
        
        Console.WriteLine($" 🔥 {Name} casts FIREBALL! (Mana: {_mana}/{MaxMana})");
        Utils.Utils.Pause();
        
        int dmg = BaseAtk * 3 + Rng.Next(5, 20);
        target.TakeDamage(dmg);
        Console.WriteLine($" Fireball deals {dmg} fire damage!");
    }

    public override void Describe()
    {
        base.Describe();
        Console.WriteLine($" Mana: {_mana}/{MaxMana}");
    }
}