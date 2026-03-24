using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;
using HeroBattle.Models;
using HeroBattle.Utils;

namespace BattleHero.Models
{
    public class Shop
    {
        public void Browse(Character buyer)
        {
            var items = GetStock();
            Utils.PrintHeader($"🛒 SHOP ( You have {buyer.Gold}g)");
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write($" [{i}] ");
                items[i].Describe();
            }
            Console.WriteLine($"\n [{items.Count}] Leave shop");
            Console.Write($"\n Choose item to buy: ");

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice == items.Count)
            {
                return;
            }

            if (choice < 0 || choice >= items.Count)
            {
                Console.WriteLine("❌ Invalid choice.");
                return;
            }

            var selected = items[choice];

            if(buyer.Gold < selected.Value)
            {
                Console.WriteLine($"❌ Not enough gold! Need {selected.Value}g.");
                return;
            }

            typeof(Character).GetProperty("Gold")!.SetValue(buyer,(int)typeof(Character).GetProperty("Gold")!.GetValue(buyer)! - selected.Value);

            buyer.Bag.Add(selected);

            if (selected is Weapon w)
            {
                buyer.Equip(w);
            }
        }

        private List<Item> GetStock() => new List<Item>
        {
                new HealthPotion(25, "Minor Potion", ItemRarity.Common),
                new HealthPotion(60, "Greater Potion", ItemRarity.Uncommon),
                new Weapon("Iron Sword", damage: 8, rarity: ItemRarity.Common),
                new Weapon("Flame Staff", damage: 12, element: Element.Fire,
                rarity: ItemRarity.Rare),
                new Weapon("Shadow Dagger", damage: 10, element: Element.Lightning,
                rarity: ItemRarity.Uncommon),
                new Weapon("Legendary Blade", damage: 20, element: Element.Ice,
                rarity: ItemRarity.Legendary),
        };
    }
}