using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace HeroBattle.Utils
{
    static public class Utils
    {
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
    }

}