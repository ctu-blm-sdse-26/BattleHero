using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;
using HeroBattle.Interfaces;

namespace HeroBattle.Models
{
    public class Enemy : Character, IDamageable, IAttacker
    {
        public Enemy(string name, int level, HeroClass heroClass) : base(name, level, heroClass)
        {
           
        }

        public int Attack(IDamageable target)
        {
            int damage = (int) (BaseAttack * 1.15); // Enemies deal 15% more damage
            target.TakeDamage(damage);
            return damage;
        }

        public void Heal(int amount)
        {
            Health += (int)Math.Ceiling(amount * 0.8); // Enemies heal 20% less effectively
            if (Health > MaxHP) Health = MaxHP;
        }

        public void TakeDamage(int amount)
        {
            int effectiveDamage = Math.Max(0, amount - BaseDefense);
            Health -= effectiveDamage;
            if (Health < 0) Health = 0;
        }
    }
}