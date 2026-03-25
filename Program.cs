using NAudio.Wave;
using BattleHero.Models;
using HeroBattle.Enums;
using HeroBattle.Models;
using HeroBattle.Utils;
using HeroBattle.data;

// Print game title at the top
Utils.PrintHeader("⚔️ Hero Battle Simulator ⚔️");

int choice = -1; // stores menu choice

do
{
    Utils.MainMenu(); // show main menu
    choice = int.Parse(Console.ReadLine() ?? "1"); // read user input (default 1 if null)

    switch (choice)
    {
        case 2:
            {
                var db = new GameContext();
                List<Warrior> warriors = db.Warriors.ToList();
                Utils.PrintHeader("Load From Database:");
                for (int i = 0; i < warriors.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {warriors[i].Name}");
                }
                Console.WriteLine("[0] Cancel");
                Console.Write("Choice: ");
                choice = int.Parse(Console.ReadLine() ?? "0");

                if(choice == 0)
                {
                    break;
                } 
                else
                {
                    _player = warriors[choice - 1];
                    if (_player.IsAlive)
                    {
                        Utils.PrintWithColor("Player Loaded from database", ConsoleColor.Green);
                        _player.Describe();
                        Utils.Pause(5000);
                    } 
                }
            }
            break;
        case 1:
            {
                // choose hero class and name
                HeroClass _heroClass = Utils.ChooseHeroClassMenu();
                string _heroName = Utils.CreateHeroNameMenu();

                // create player depending on class selected
                Character _player = _heroClass switch
                {
                    HeroClass.Warrior => new Warrior(_heroName),
                    HeroClass.Mage => new Mage(_heroName),
                    HeroClass.Rogue => new Rogue(_heroName),
                    _ => new Warrior(_heroName) // fallback just in case
                };

                // give player some starting items
                _player.Bag.Add(new HealthPotion());
                _player.Bag.Add(new Weapon("Power Pole", 15));
                _player.Bag.Add(new Armour(20, "Shield"));

                var shop = new Shop(); // shop instance
                var rng = new Random(); // random generator for enemies
                int totalXP = 0; // total xp earned
                int score = 0; // score tracker (same as xp for now)

                // list of possible enemies
                List<Enemy> enemyPool = new List<Enemy>
            {
                new Enemy("Slime", 30, 5, 10, 15),
                new Enemy("Goblin", 45, 8, 18, 25),
                new Enemy("Orc Warrior", 70, 12, 30, 40),
                new Enemy("Dragon Boss", 150, 20, 100, 100)
            };

                bool playing = true; // controls game loop

                // main gameplay loop
                while (playing && _player.IsAlive)
                {
                    Console.WriteLine("""
                    1. Fight
                    2. Inventory
                    3. Shop
                    4. Quit
                    """);

                    choice = int.Parse(Console.ReadLine() ?? "1");

                    if (choice == 1)
                    {
                        // pick random enemy
                        Enemy enemy = enemyPool[rng.Next(enemyPool.Count)];

                        // fight loop until someone dies
                        while (_player.IsAlive && enemy.IsAlive)
                        {
                            _player.Attack(enemy); // player attacks first
                            if (enemy.IsAlive)
                                enemy.Attack(_player); // enemy attacks back
                        }

                        if (_player.IsAlive)
                        {
                            Console.WriteLine("Victory!");

                            // rewards
                            _player.EarnGold(enemy.Reward);
                            totalXP += enemy.XP;
                            score += enemy.XP;

                            // level up check (kinda simple formula)
                            if (totalXP / 60 >= _player.Level)
                                _player.LevelUp();
                        }
                        else
                        {
                            Console.WriteLine("Game Over!");
                        }
                    }
                    else if (choice == 2)
                    {
                        // show inventory items
                        var items = _player.Bag.GetAll().ToList();
                        for (int i = 0; i < items.Count; i++)
                            Console.WriteLine($"[{i + 1}] {items[i].Name}");

                        // choose item to use
                        int pick = int.Parse(Console.ReadLine() ?? "1");
                        if (pick >= 1 && pick <= items.Count)
                            _player.UseItem(pick - 1); // index starts at 0
                    }
                    else if (choice == 3)
                    {
                        // open shop menu
                        shop.Browse(_player);
                    }
                    else if (choice == 4)
                    {
                        // exit game loop
                        playing = false;
                    }
                }

                // end game
                Console.WriteLine("\n🎮 GAME OVER");
                Console.WriteLine($"Hero: {_player.Name}");
                Console.WriteLine($"Level: {_player.Level}");
                Console.WriteLine($"XP: {totalXP}");
                Console.WriteLine($"Score: {score}");
                Console.ReadKey(); // pause before returning to main menu
            }
            break;

        case 0:
            return; // exit program completely

        default:
            Console.WriteLine("Invalid Choice.");
            break;
    }

} while (true); // keeps main menu running