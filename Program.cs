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
 
            Console.WriteLine($"Hero {_player.Name} created!");
        }
        break;
 
        case 0:
            return;
 
        default:
            Console.WriteLine("Invalid Choice.");
            break;
    }
 
} while (true);