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

         public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHP) Health = MaxHP;
        }

         



    }


}