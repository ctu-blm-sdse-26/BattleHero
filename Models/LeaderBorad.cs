using System;
using System.Collections.Generic;
using System.Linq;
using HeroBattle.Models;
using HeroBattle.Utils;

namespace BattleHero.Models
{
    public class Leaderboard<T> where T : Character
    {
        // Store Hero + Score together
        private List<(T Hero, int Score)> _entries = new();

        // Add a new entry
        public void Add(T hero, int score)
        {
            _entries.Add((hero, score));
        }

        // Display leaderboard
        public void Display()
        {
            Utils.PrintHeader("🏆 LEADERBOARD");

            var sorted = _entries
                .OrderByDescending(e => e.Score)
                .ThenByDescending(e => e.Hero.Level) // tie-breaker
                .ToList();

            for (int i = 0; i < sorted.Count; i++)
            {
                Console.WriteLine(
                    $" #{i + 1} {sorted[i].Hero.Name,-15} ({sorted[i].Hero.Class}) " +
                    $"Score: {sorted[i].Score}");
            }}

            //Clear leaderboard
        public void Clear()
        {
            _entries.Clear();
        }
 
        // Get top N entries
        public List<(T Hero, int Score)> GetTopN(int n)
        {
            return _entries
                .OrderByDescending(e => e.Score)
                .ThenByDescending(e => e.Hero.Level)
                .Take(n)
                .ToList();
        }

        
}}

