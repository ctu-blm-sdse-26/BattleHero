using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using HeroBattle.Enums;

namespace HeroBattle.Utils
{
    static public class Utils
    {
        // public static object Utils { get; internal set; }

        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0) return min;
            if (value.CompareTo(max) > 0) return max;
            return value;
        }

        public static void PrintHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string border = new string('=', text.Length + 4);
            Console.WriteLine($"\n╔{border}╗");
            Console.WriteLine($"║ {text} ║");
            Console.WriteLine($"╚{border}╝");
            Console.ResetColor();
        }

        public static void Pause(int ms = 500) => Thread.Sleep(ms);

        public static void PrintWithColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void MainMenu()
        {
            PrintHeader("Main Menu");
            Console.WriteLine("[1] New Game");
            Console.WriteLine("[2] Load Game");
            Console.WriteLine("[0] Exit");
            Console.Write("Choice: ");
        }

        public static HeroClass ChooseHeroClassMenu()
        {
            PrintHeader("Choose Hero Class");
            Console.WriteLine("[1] Warrior - High health and defense, moderate attack.");
            Console.WriteLine("[2] Mage - High attack, low health and defense.");
            Console.WriteLine("[3] Rogue - Balanced attack and defense, moderate health.");
            Console.WriteLine("[0] Exit");
            Console.Write("Choice: ");

            int choice = int.Parse(Console.ReadLine() ?? "1");
            return choice switch
            {
                1 => HeroClass.Warrior,
                2 => HeroClass.Mage,
                3 => HeroClass.Rogue,
                _ => HeroClass.Warrior
            };
        }

        public static string CreateHeroNameMenu()
        {
            PrintHeader("Create A Hero Name:");
            Console.Write("Name: ");
            return Console.ReadLine() ?? "Hero";
        }

    }

}