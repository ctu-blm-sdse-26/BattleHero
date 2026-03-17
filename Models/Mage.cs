using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;
using HeroBattle.Interfaces;


namespace battleHero.Models
{

    public class Mage : HeroBattle.Models.Character
    {
      
        public int Mana { get; set; }
        public int MaxMana => 100;

        public Mage(string name, int level)
            : base(name, level, HeroClass.Mage)
        {
            Mana = MaxMana;
            BaseAttack = 25;   
            BaseDefense = 5;    
        }

        public void UseMana(int amount)
        {
            Mana -= amount;
            if (Mana < 0) Mana = 0;
        }

    }
}

        
    