using BattleHero.Models;
using HeroBattle.Enums;
using HeroBattle.Interfaces;
using HeroBattle.Models;
using HeroBattle.Utils;

// DEFAULT INVENTORY
var bag = new Inventory<Item>();
bag.Add(new HealthPotion());
bag.Add(new Weapon("Power Pole", 15));
bag.Add(new Armour(20, "Shield"));

Utils.PrintHeader("⚔️ Hero Battle Simulator ⚔️");
int choice = -1;

do
{
    Utils.MainMenu();
    choice = int.Parse(Console.ReadLine() ?? "1");

    switch (choice)
    {
        case 1:
            {
                // NEW GAME SETUP --------------------------
                HeroClass _heroClass = Utils.ChooseHeroClassMenu();
                string _heroName = Utils.CreateHeroNameMenu();

                // Create character --------------------
                Console.WriteLine("Creating Character..." + _heroName);
                Character _player = _heroClass switch
                {
                    HeroClass.Warrior => new Warrior(_heroName),
                    HeroClass.Mage => new Mage(_heroName),
                    HeroClass.Rogue => new Mage(_heroName),
                    _ => new Warrior(_heroName)
                };

                // Creating Enemy
                Enemy enemy = new Enemy("Vector", 2, HeroClass.Rogue);
                Utils.Pause(1000);
                Console.WriteLine($"A wild enemy named {enemy.Name} appears!");

                Utils.PrintWithColor($"{_player.Name} - HP: {_player.Health}/{_player.MaxHP}", ConsoleColor.Green);
                Console.WriteLine($"{enemy.Name} - HP: {enemy.Health}/{enemy.MaxHP}");

                // Start Battle
                int counter = 1;
                while (_player.IsAlive && enemy.IsAlive)
                {

                    Console.Write("""
    ===============================
    Choose Your Action
    ===============================
    1. Attack
    2. Use Item From Inventory

    choice:  
    """);

                    choice = int.Parse(Console.ReadLine() ?? "1");

                    if (choice == 2)
                    {
                        Console.Write($"""
    ===============================
    Choose an Item to Use
    =============================== 
    
    """);

                        var items = bag.GetAll().ToList();
                        for (int i = 0; i < items.Count; i++)
                            Console.WriteLine($"{i + 1}. {items[i].Name}");

                        Console.Write("Choice: ");

                        choice = int.Parse(Console.ReadLine() ?? "1");
                        if (choice >= 1 && choice <= items.Count)
                        {
                            _player.UseItem(choice - 1);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice.");
                        }

                        Console.WriteLine("⚔️Player is now Attacking...");
                        Utils.Pause(1000);


                        if (counter % 3 == 0)
                        {
                            Console.WriteLine($"{_player.Name} uses a DOMAIN EXPANSION!!!! 🧙🏾‍♂️");
                            _player.UseSpecial(enemy);
                        }
                        else if (choice == 1)
                        {
                            _player.Attack(enemy);
                        }
                        if (enemy.IsAlive && _player.IsAlive)
                        {
                            Console.WriteLine("👹Enemy is now Attacking...");
                            Utils.Pause(1000);
                            enemy.Attack(_player);
                        }
                        counter++;
                    }

                    Utils.PrintWithColor($"{_player.Name} - HP: {_player.Health}/{_player.MaxHP}", ConsoleColor.Green);
                    Console.WriteLine($"{enemy.Name} - HP: {enemy.Health}/{enemy.MaxHP}");

                    if (_player.IsAlive)
                    {
                        Utils.PrintHeader("🎉 Victory! 🎉");
                        Console.WriteLine($"{_player.Name} has defeated {enemy.Name}!");
                        _player.EarnGold(20);
                        _player.LevelUp();
                    }
                    else
                    {
                        Utils.PrintHeader("💀 Game Over! 💀");
                        Console.WriteLine($"Game Over! {_player.Name} was defeated by {enemy.Name}...");
                    }
                }

            }
            break;
        case 0:
            { return; }
        _: { Console.WriteLine("Invalid Choice."); Utils.Pause(); }
    }

} while (true);














