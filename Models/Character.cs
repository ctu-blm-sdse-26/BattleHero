using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;
using HeroBattle.Interfaces;

namespace HeroBattle.Models
{
    public class Inventory<T> where T : IDescribable
    {
        private readonly List<T> _items = new List<T>();

        public int Count => _items.Count;

        public void Add(T item)
        {
            _items.Add(item);
            Console.WriteLine($" [+] Added item to Inventory.");
        }

        public bool Remove(T item) => _items.Remove(item);

        public T? Find(Func<T, bool> predicate) => _items.FirstOrDefault(predicate);

        public IReadOnlyList<T> GetAll() => _items.AsReadOnly();

        public void ListAll()
        {
            if(_items.Count == 0)
            {
                Console.WriteLine("  (empty)");
                return;
            }
            foreach(var item in _items)
                item.Describe();
        }
    }

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

        protected Weapon? EquippedWeapon;

        public Inventory<Item> Bag {get;} = new Inventory<Item>();

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