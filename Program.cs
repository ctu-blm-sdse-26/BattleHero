using NAudio.Wave;
using BattleHero.Models;
using HeroBattle.Enums;
using HeroBattle.Models;
using HeroBattle.Utils;

Utils.PrintHeader("⚔️ Hero Battle Simulator ⚔️");

// 🎵 Start background music, YOW U CAN TOGGLE M to stop/play music !!!! 😈
WaveOutEvent? outputDevice = null;
AudioFileReader? audioFile = null;
bool musicPlaying = false;

try
{
    audioFile = new AudioFileReader("Assets/battle-theme.mp3");
    outputDevice = new WaveOutEvent();

    outputDevice.Init(audioFile);
    audioFile.Volume = 0.3f;

    // 🔁 Loop music
    outputDevice.PlaybackStopped += (s, e) =>
    {
        if (audioFile != null && outputDevice != null && musicPlaying)
        {
            audioFile.Position = 0;
            outputDevice.Play();
        }
    };

    outputDevice.Play();
    musicPlaying = true;

    Console.WriteLine("🎵 Music playing... (Press M anytime to toggle)");
}
catch
{
    Console.WriteLine("🔇 Audio could not be played.");
}

var bag = new Inventory<Item>();
bag.Add(new HealthPotion());
bag.Add(new Weapon("Short Sword", damage: 6));

// var slime = new Enemy("Slime", hp: 30, atk: 5, reward: 10, xp: 15);
// slime.Describe();
// slime.TakeDamage(12);
// slime.Describe();

Console.Write("""

Choose your hero class: 
1. Warrior
2. Mage
3. Rogue

choice: 
""");

    var input = Console.ReadLine();

    // 🎧 Handle M toggle
    if (input?.ToLower() == "m" && outputDevice != null)
    {
        if (musicPlaying)
        {
            outputDevice.Stop();
            Console.WriteLine("🔇 Music stopped");
        }
        else
        {
            outputDevice.Play();
            Console.WriteLine("🎵 Music resumed");
        }

        musicPlaying = !musicPlaying;
        continue;
    }

    if (int.TryParse(input, out choice) && choice >= 1 && choice <= 3)
        break;

    Console.WriteLine("❌ Invalid input. Enter 1, 2, or 3 (or M to toggle music).");
}

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
Enemy enemy = new Enemy("Vector", hp: 40, atk: 8, reward: 20, xp: 30);
Utils.Pause(1000);

Console.WriteLine($"A wild enemy named {enemy.Name} appears!");
int counter = 1;

while (player.IsAlive && enemy.IsAlive)
{
    // 🎧 Non-blocking toggle
    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.M && outputDevice != null)
        {
            if (musicPlaying)
            {
                outputDevice.Stop();
                Console.WriteLine("🔇 Music stopped");
            }
            else
            {
                outputDevice.Play();
                Console.WriteLine("🎵 Music resumed");
            }

            musicPlaying = !musicPlaying;
        }
    }

    Console.WriteLine($"""
    {player.Name} - HP: {player.Health}/{player.MaxHP}
    {enemy.Name} - HP: {enemy.Health}/{enemy.MaxHP}
    """);

    // 🎮 MAIN MENU INPUT (SAFE)
    while (true)
    {
        Console.Write("""
    ===============================
    Choose Your Action
    ===============================
    1. Attack
    2. Use Item

    choice:  
    """);

        var input = Console.ReadLine();

        if (input?.ToLower() == "m" && outputDevice != null)
        {
            if (musicPlaying)
            {
                outputDevice.Stop();
                Console.WriteLine("🔇 Music stopped");
            }
            else
            {
                outputDevice.Play();
                Console.WriteLine("🎵 Music resumed");
            }

            musicPlaying = !musicPlaying;
            continue;
        }

        if (int.TryParse(input, out choice) && (choice == 1 || choice == 2))
            break;

        Console.WriteLine("❌ Invalid input. Enter 1 or 2 (or M to toggle music).");
    }

    if (choice == 2)
    {
        var items = bag.GetAll().ToList();

        while (true)
        {
            Console.WriteLine("""
    ===============================
    Choose an Item
    ===============================
    """);

            for (int i = 0; i < items.Count; i++)
                Console.WriteLine($"{i + 1}. {items[i].Name}");

            Console.Write("Choice: ");

            var input = Console.ReadLine();

            if (input?.ToLower() == "m" && outputDevice != null)
            {
                if (musicPlaying)
                {
                    outputDevice.Stop();
                    Console.WriteLine("🔇 Music stopped");
                }
                else
                {
                    outputDevice.Play();
                    Console.WriteLine("🎵 Music resumed");
                }

                musicPlaying = !musicPlaying;
                continue;
            }

            if (int.TryParse(input, out int itemChoice) &&
                itemChoice >= 1 && itemChoice <= items.Count)
            {
                player.UseItem(itemChoice - 1);
                break;
            }

            Console.WriteLine("❌ Invalid item choice.");
        }
    }

    Console.WriteLine("⚔️ Player is attacking...");
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
        Console.WriteLine("👹 Enemy is attacking...");
        Utils.Pause(1000);
        enemy.Attack(player);
    }

    counter++;
}

// 🛑 Stop music
outputDevice?.Stop();
audioFile?.Dispose();
outputDevice?.Dispose();

Console.WriteLine($"""
{player.Name} - HP: {player.Health}/{player.MaxHP}
{enemy.Name} - HP: {enemy.Health}/{enemy.MaxHP}
""");

if (player.IsAlive)
{
    Utils.PrintHeader("🎉 Victory! 🎉");
    Console.WriteLine($"{player.Name} defeated {enemy.Name}!");
    player.EarnGold(20);
    player.LevelUp();
}
else
{
    Utils.PrintHeader("💀 Game Over! 💀");
    Console.WriteLine($"{player.Name} was defeated...");
}
