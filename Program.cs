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
            HeroClass _heroClass = Utils.ChooseHeroClassMenu();
            string _heroName = Utils.CreateHeroNameMenu();
 
            Character _player = _heroClass switch
            {
                HeroClass.Warrior => new Warrior(_heroName),
                HeroClass.Mage => new Mage(_heroName),
                HeroClass.Rogue => new Rogue(_heroName),
                _ => new Warrior(_heroName)
            };
 
            _player.Bag.Add(new HealthPotion());
            _player.Bag.Add(new Weapon("Power Pole", 15));
            _player.Bag.Add(new Armour(20, "Shield"));
 
            var shop = new Shop();
            var rng = new Random();
            int totalXP = 0;
            int score = 0;
 
            List<Enemy> enemyPool = new List<Enemy>
            {
                new Enemy("Slime", 30, 5, 10, 15),
                new Enemy("Goblin", 45, 8, 18, 25),
                new Enemy("Orc Warrior", 70, 12, 30, 40),
                new Enemy("Dragon Boss", 150, 20, 100, 100)
            };
 
            bool playing = true;
 
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
                    Enemy enemy = enemyPool[rng.Next(enemyPool.Count)];
 
                    while (_player.IsAlive && enemy.IsAlive)
                    {
                        _player.Attack(enemy);
                        if (enemy.IsAlive)
                            enemy.Attack(_player);
                    }
 
                    if (_player.IsAlive)
                    {
                        Console.WriteLine("Victory!");
                        _player.EarnGold(enemy.Reward);
                        totalXP += enemy.XP;
                        score += enemy.XP;
 
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
                    var items = _player.Bag.GetAll().ToList();
                    for (int i = 0; i < items.Count; i++)
                        Console.WriteLine($"[{i + 1}] {items[i].Name}");
 
                    int pick = int.Parse(Console.ReadLine() ?? "1");
                    if (pick >= 1 && pick <= items.Count)
                        _player.UseItem(pick - 1);
                }
                else if (choice == 3)
                {
                    shop.Browse(_player);
                }
                else if (choice == 4)
                {
                    playing = false;
                }
            }
 
            Console.WriteLine("\n🎮 GAME OVER");
            Console.WriteLine($"Hero: {_player.Name}");
            Console.WriteLine($"Level: {_player.Level}");
            Console.WriteLine($"XP: {totalXP}");
            Console.WriteLine($"Score: {score}");
            Console.ReadKey();
        }
        break;
 
        case 0:
            return;
 
        default:
            Console.WriteLine("Invalid Choice.");
            break;
    }
 
} while (true);