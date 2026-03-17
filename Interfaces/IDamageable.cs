using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HeroBattle.Interfaces
{
    public interface IDamageable
    {
        int Health {get;}
        string Name {get;}
        int MaxHP {get;}
        bool IsAlive {get;} 
        void TakeDamage(int amount);
        void Heal(int amount);

    }
}