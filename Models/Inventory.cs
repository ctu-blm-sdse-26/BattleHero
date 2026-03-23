using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
}