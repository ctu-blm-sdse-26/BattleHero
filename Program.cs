using HeroBattle.Enums;
using HeroBattle.Interfaces;
using HeroBattle.Models;
using HeroBattle.Utils;

Utils.PrintHeader("⚔️ Hero Battle Simulator ⚔️");

Console.Write("""
Choose your hero class: 
1. Warrior - High health and defense, moderate attack.
2. Mage - High attack, low health and defense.
3. Rogue - Balanced attack and defense, moderate health.

choice: 
""");

int choice = int.Parse(Console.ReadLine() ?? "1");

Console.Write("""

Choose your hero Name: 
""");

string heroName = Console.ReadLine() ?? "Hero";

Console.WriteLine("Creating Character..." + heroName);

HeroClass heroClass = choice switch
{
    1 => HeroClass.Warrior,
    2 => HeroClass.Mage,
    3 => HeroClass.Rogue,
    _ => HeroClass.Warrior
};

Hero player = new Hero(heroName, 1, heroClass);
Enemy enemy = new Enemy("Vector", 2, HeroClass.Rogue);

Utils.Pause(1000);

Console.WriteLine($"A wild enemy named {enemy.Name} appears!");
int counter = 1;
while (player.IsAlive && enemy.IsAlive)
{
    Console.WriteLine($"""
    {player.Name} - HP: {player.Health}/{player.MaxHP}
    {enemy.Name} - HP: {enemy.Health}/{enemy.MaxHP}
    """);

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
        Console.Write("""
    ===============================
    Choose an Item to Use
    ===============================
    1. Health Potion - Restores 50 HP
    2. Armour - Increases defense
    3. Weapon - Increases attack

    choice:  
    """);

    choice = int.Parse(Console.ReadLine() ?? "1");
    switch (choice)
        {
            case 1: new HealthPotion().Apply(player);
            break;
            case 2: new Armour(15).Apply(player);
            break;
            case 3: new Weapon("Power Pole", 35).Apply(player);
            break;
        }
    }

    Console.WriteLine("⚔️Player is now Attacking...");
    Utils.Pause(1000);


    if (counter % 3 == 0)
    {
        Console.WriteLine($"{player.Name} uses a DOMAIN EXPANSION!!!! 🧙🏾‍♂️");
        player.SuperAttack(enemy);
    }
    else
    {
        player.Attack(enemy);
    }
    if (enemy.IsAlive && player.IsAlive)
    {
        Console.WriteLine("👹Enemy is now Attacking...");
        Utils.Pause(1000);
        enemy.Attack(player);
    }
    counter++;
}

Console.WriteLine($"""
    {player.Name} - HP: {player.Health}/{player.MaxHP}
    {enemy.Name} - HP: {enemy.Health}/{enemy.MaxHP}
    """);

if (player.IsAlive)
{
    Utils.PrintHeader("🎉 Victory! 🎉");
    Console.WriteLine($"{player.Name} has defeated {enemy.Name}!");
}
else
{
    Utils.PrintHeader("💀 Game Over! 💀");
    Console.WriteLine($"Game Over! {player.Name} was defeated by {enemy.Name}...");
}










