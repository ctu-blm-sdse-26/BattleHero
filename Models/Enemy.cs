using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;
using HeroBattle.Interfaces;

namespace HeroBattle.Models
{
    public class Enemy : IDamageable, IAttacker, IDescribable
{
    public string Name { get; }
    public int MaxHP { get; }
    public int CurrentHP { get; private set; }
    public int Health => CurrentHP;
    public bool IsAlive => CurrentHP > 0;
    public int Reward { get; }
    public int XP { get; }
    private int _atk;
    private static Random Rng = new Random();

    public Enemy(string name, int hp, int atk, int reward, int xp)
    {
        Name = name;
        MaxHP = hp;
        CurrentHP = hp;
        _atk = atk;
        Reward = reward;
        XP = xp;
    }

    public void TakeDamage(int amount)
    {
        CurrentHP = Math.Max(0, CurrentHP - amount);
        Console.WriteLine($" 💢 {Name} HP: {CurrentHP}/{MaxHP}");
    }

    public void Heal(int amount) => CurrentHP = Math.Min(MaxHP, CurrentHP + amount);

    public int Attack(IDamageable target)
    {
        int dmg = _atk + Rng.Next(-2, 5);
        Console.WriteLine($" 👾 {Name} attacks {target.Name}!");
        target.TakeDamage(dmg);
        return dmg;
    }

    public void Describe()
    {
        Console.WriteLine($" 👾 {Name} HP:{CurrentHP}/{MaxHP} ATK:{_atk} Reward:{Reward}g/{XP}xp");
    }
}}