using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;
using HeroBattle.Interfaces;

namespace HeroBattle.Models
{
    public class Hero : Character, IDamageable, IAttacker
    {
        public Hero(string name, int level, HeroClass heroClass) : base(name, level, heroClass)
        {
            
        }

         public void TakeDamage(int damage)
        {
            int effectiveDamage = Math.Max(0, damage - BaseDefense);
            Health -= effectiveDamage;
            if (Health < 0) Health = 0;
        }

        public int Attack(IDamageable target)
        {
            int damage = BaseAttack;
            target.TakeDamage(damage);
            return damage;
        }

        public int SuperAttack(IDamageable target)
        {
            int damage = BaseAttack * 5;
            target.TakeDamage(damage);
            return damage;
        }

        public void UseItem(int index)
        {
            var all = Bag.GetAll().ToList();
            if(index < 0 || index >= all.Count)
            {
                Console.WriteLine(" ❌ Invalid item");
                return;
            }
            all[index].Apply(this);
            Bag.Remove(all[index]);
        }

        public override void UseSpecial(IDamageable target)
        {
            throw new NotImplementedException();
        }
    }

}