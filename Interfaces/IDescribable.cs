using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroBattle.Interfaces
{
    public interface IDescribable
    {
        string Name {get;}
        public void Describe();
    }
}