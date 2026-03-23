using NAudio.Wave;
using BattleHero.Models;
using HeroBattle.Enums;
using HeroBattle.Models;
using HeroBattle.Utils;


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
                Console.WriteLine();

                string _heroName = Utils.CreateHeroNameMenu();
                Console.WriteLine();

                // Create character --------------------
                Console.WriteLine("Creating Character..." + _heroName);
                Character _player = _heroClass switch
                {
                    HeroClass.Warrior => new Warrior(_heroName),
                    HeroClass.Mage => new Mage(_heroName),
                    HeroClass.Rogue => new Mage(_heroName),
                    _ => new Warrior(_heroName)
                };
                _player.Bag.Add(new HealthPotion());
                _player.Bag.Add(new Weapon("Power Pole", 15));
                _player.Bag.Add(new Armour(20, "Shield"));

                Console.WriteLine();

                // Creating Enemy
                Enemy enemy = new Enemy("Bad Guy", 200, 2, 300, 50);
                Utils.Pause(1000);
                Console.WriteLine();
                Console.WriteLine($"A wild enemy named {enemy.Name} appears!\n");

                Utils.PrintHeader("Current Health Stats:");
                Utils.PrintWithColor($"{_player.Name} - HP: {_player.Health}/{_player.MaxHP}", ConsoleColor.Green);
                Console.WriteLine($"{enemy.Name} - HP: {enemy.Health}/{enemy.MaxHP}");
                Console.WriteLine();

                // Start Battle
                int counter = 1;
                while (_player.IsAlive && enemy.IsAlive)
                {
                    Utils.PrintHeader("Choose Your Action");
                    Console.Write("""
    [1] Attack
    [2] Use Item

    choice:  
    """);

                    choice = int.Parse(Console.ReadLine() ?? "1");

                    if (choice == 1)
                    {
                        Console.WriteLine("⚔️Player is now Attacking...");
                        Utils.Pause(1000);


                        if (counter % 3 == 0)
                        {
                            Console.WriteLine($"{_player.Name} uses a DOMAIN EXPANSION!!!! 🧙🏾‍♂️");
                            _player.UseSpecial(enemy);
                        }
                        else
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

                    if (choice == 2)
                    {
                        Utils.PrintHeader("Pick Item: ");

                        var items = _player.Bag.GetAll().ToList();
                        for (int i = 0; i < items.Count; i++)
                            Console.WriteLine($"[{i + 1}] {items[i].Name}");

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
                    }

                    if(choice != 1 && choice != 2)
                    {
                        Console.WriteLine("Invalid Choice.\n");
                    }

                    Utils.PrintHeader("Current Health Stats:");
                    Utils.PrintWithColor($"{_player.Name} - HP: {_player.Health}/{_player.MaxHP}", ConsoleColor.Green);
                    Console.WriteLine($"{enemy.Name} - HP: {enemy.Health}/{enemy.MaxHP} \n");

                    if (!enemy.IsAlive)
                    {
                        Utils.PrintHeader("🎉 Victory! 🎉");
                        Console.WriteLine($"{_player.Name} has defeated {enemy.Name}!");
                        _player.EarnGold(enemy.Reward);
                        _player.EarnXP(enemy.XP);
                        _player.LevelUp();

                    }
                    
                    if(!_player.IsAlive)
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














