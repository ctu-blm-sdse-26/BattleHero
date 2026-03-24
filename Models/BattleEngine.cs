using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Models;
using HeroBattle.Utils;

namespace BattleHero.Models
{
    public class BattleEngine
    {
        private static Random Rng = new Random();

        public static int RunBattle(Character hero, Enemy enemy)

        {
            Utils.PrintHeader($"⚔ BATTLE: {hero.Name} vs {enemy.Name}");
            enemy.Describe();
            int round = 1;
            while (hero.IsAlive && enemy.IsAlive)
            {
                Console.WriteLine($"\n ── Round {round} ──");
                Console.WriteLine($" {hero.Name}: {hero.Health}/{hero.MaxHP} HP");
                Console.WriteLine($" {enemy.Name}: {enemy.CurrentHP}/{enemy.MaxHP} HP");
                Console.WriteLine("\n Your move:");
                Console.WriteLine(" [1] Attack [2] Special [3] Use Item [4] Flee");
                Console.Write(" > ");
                string? input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "1":
                        hero.Attack(enemy);
                        break;
                    case "2":
                        hero.UseSpecial(enemy);
                        break;
                    case "3":
                        var items = hero.Bag.GetAll().ToList();
                        if (items.Count == 0)
                        {
                            Console.WriteLine(" ❌ No items!");
                            continue;
                        }
                        for (int i = 0; i < items.Count; i++)
                        {
                            Console.Write($" [{i}] ");
                            items[i].Describe();
                        }
                        Console.Write(" Choose: ");
                        if (int.TryParse(Console.ReadLine(), out int idx))
                            hero.UseItem(idx);
                        break;
                    case "4":
                        int roll = Rng.Next(100);

                        if (roll < 60) 
                        {
                            Console.WriteLine($"{hero.Name} successfully flees from the battle!");
                            return -1; //
                        }
                        else 
                        {
                            Console.WriteLine($"The {enemy.Name} blocks your escape!");
                            break; 
                        }
                    default:
                        Console.WriteLine("Unknown command.");
                        continue;
                }
                Utils.Pause();
                if (enemy.IsAlive)
                    enemy.Attack(hero);
                round++;
            }
            if (hero.IsAlive)
            {
                Utils.PrintWithColor($"\n 🎉 {hero.Name} wins!\n", ConsoleColor.Green);
                hero.EarnGold(enemy.Reward);
                return enemy.XP;
            }
            else
            {
                Utils.PrintWithColor($"\n 💀 {hero.Name} was defeated...\n",
                ConsoleColor.Red);
                return 0;
            }
        }
    }
}