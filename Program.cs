using HeroBattle.Enums;
using HeroBattle.Interfaces;
using HeroBattle.Models;
using HeroBattle.Utils;

Utils.PrintHeader("⚔️ Hero Battle Simulator ⚔️");

var bag = new Inventory<Item>();
bag.Add(new HealthPotion());
bag.Add(new Weapon("Short Sword", damage: 6));

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
    if(choice >= 1 && choice <= items.Count) 
    {
        player.UseItem(choice - 1);
    } 
    else
    {
        Console.WriteLine("Invalid Choice.");  
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
    player.EarnGold(20);
    player.LevelUp();
}
else
{
    Utils.PrintHeader("💀 Game Over! 💀");
    Console.WriteLine($"Game Over! {player.Name} was defeated by {enemy.Name}...");
}}










