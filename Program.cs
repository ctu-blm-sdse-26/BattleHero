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
            Console.WriteLine();
 
            string _heroName = Utils.CreateHeroNameMenu();
            Console.WriteLine();
 
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
 
            // GAME SYSTEMS
            
            var shop = new Shop();
            var rng = new Random();
            int totalXP = 0;
 
            List<Enemy> enemyPool = new List<Enemy>
            {
                new Enemy("Slime", 30, 5, 10, 15),
                new Enemy("Goblin", 45, 8, 18, 25),
                new Enemy("Orc Warrior", 70, 12, 30, 40)
            };
 
            Console.WriteLine("Game systems ready!");
        }
        break;
 
        case 0:
            return;
 
        default:
            Console.WriteLine("Invalid Choice.");
            break;
    }
 
} while (true);