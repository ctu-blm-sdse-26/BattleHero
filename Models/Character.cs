using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using BattleHero.Models;
using HeroBattle.Enums;
using HeroBattle.Interfaces;
namespace HeroBattle.Models
{
    public abstract class Character : IDamageable, IAttacker
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; private set; } = 1;
        public HeroClass Class { get; set; }
        public int MaxHP { get; protected set; }
        public int CurrentHP { get; private set; }
        public int BaseAttack { get; set; } = 15;
        public int BaseDefense { get; set; } = 10;
        public int Gold { get; private set; } = 0;
        public bool IsAlive => CurrentHP > 0;
        protected Weapon? EquippedWeapon;
        public Inventory<Item> Bag { get; } = new Inventory<Item>();

        protected static Random Rng = new Random();

        private Dictionary<string, int> _stats = new Dictionary<string, int>
        {
        { "Kills", 0 },
        { "Damage", 0 },
        { "Heals", 0 },
        };
        private HeroClass heroClass;
        internal object Kills;

        protected Character(string name, HeroClass heroClass, int maxHp, int baseAtk,
int defense)
        {
            Name = name;
            Class = heroClass;
            MaxHP = maxHp;
            CurrentHP = maxHp;
            BaseAttack = baseAtk;
            BaseDefense = defense;
            Level = 1;
            Gold = 50;
        }

        protected Character(string name, int level, HeroClass heroClass)
        {
            Name = name;
            Level = level;
            this.heroClass = heroClass;
        }

        public void TakeDamage(int amount)
        {
            int reduced = Math.Max(0, amount - BaseDefense);
            Health = Utils.Utils.Clamp(Health - reduced, 0, MaxHP);
            Utils.Utils.PrintWithColor($" {Name} takes {reduced} damage (block {amount - reduced}). HP: {Health}/{MaxHP}\n",
            Health < MaxHP / 4 ? ConsoleColor.Red : ConsoleColor.DarkYellow);
        }
        public void Heal(int amount)
        {
            int before = Health;
            Health = Utils.Utils.Clamp(Health + amount, 0, MaxHP);
            _stats["Heals"] += Health - before;
        }
        public virtual int Attack(IDamageable target)
        {
            int dmg = BaseAttack + (EquippedWeapon?.Damage ?? 0) + Rng.Next(-3, 4);
            dmg = Math.Max(1, dmg);
            target.TakeDamage(dmg);
            _stats["Damage"] += dmg;
            if (!target.IsAlive) _stats["Kills"]++;
            return dmg;
        }
        public abstract void UseSpecial(IDamageable target);
        public void Equip(Weapon w)
        {
            EquippedWeapon = w;
            Console.WriteLine($" 🗡 {Name} equips {w.Name} (+{w.Damage} ATK)");
        }
        public void UseItem(int index)
        {
            var all = Bag.GetAll().ToList();
            if (index < 0 || index >= all.Count) { Console.WriteLine(" ❌ Invalid item."); return; }
            all[index].Apply(this);
            Bag.Remove(all[index]);
        }
        public void EarnGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($" 💰 {Name} earns {amount}g. Total: {Gold}g");
        }
        public void LevelUp()
        {
            Level++;
            MaxHP += 10;
            CurrentHP = MaxHP;
            BaseAttack += 3;
            BaseDefense += 1;
            Utils.Utils.PrintWithColor($"\n 🌟 {Name} reached Level {Level}! Statsincreased.\n", ConsoleColor.Yellow);
        }
        public virtual void Describe()
        {
            Console.WriteLine($"\n ┌─ {Name} ({Class}) – Lv {Level}");
            Console.WriteLine($" │ HP: {CurrentHP}/{MaxHP}");
            Console.WriteLine($" │ ATK: {BaseAttack + (EquippedWeapon?.Damage ?? 0)} DEF: {BaseDefense} Gold: {Gold}g");
            Console.WriteLine($" │ Weapon: {EquippedWeapon?.Name ?? "Bare hands"}");
            Console.WriteLine($" └─ Kills: {_stats["Kills"]} Dmg: {_stats["Damage"]} Healed: {_stats["Heals"]}");
        }

        public int GetScore()
        {
            int kills = _stats["Kills"];
            int damage = _stats["Damage"];
            int heals = _stats["Heals"];

            // Example scoring system
            int score = (kills * 100) + (damage / 10) + (heals / 5) + (Gold / 5) + (Level * 50);
            return score;
        }

        internal object GetStat(string v)
        {
            throw new NotImplementedException();
        }
    }
}