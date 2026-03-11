using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroBattle.Interfaces
{
    public interface IAttacker
    {
       int Attack(IDamageable target); 
    }
}