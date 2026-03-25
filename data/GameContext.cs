using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleHero.Models;
using HeroBattle.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroBattle.data
{
    public class GameContext: DbContext
    {
        public DbSet<Inventory<Item>> Inventory { get; set; }
        public DbSet<Mage> Mages { get; set; }
        public DbSet<Warrior> Warriors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=game.db");
        }
        
    }
}