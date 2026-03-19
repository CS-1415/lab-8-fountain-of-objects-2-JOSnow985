// Jaden Olvera, CS-1415, Lab 8 Fountain of Objects
using Lab08;

Printer.MapSelect();

Map map;
while (true)
{
    string userInput = Console.ReadLine()!;
    if (userInput is not null)
    {
        userInput = userInput.ToLowerInvariant();
        if (userInput == "large")
        {
           map = Map.Large;
            break;
        }
        else if (userInput == "medium")
        {
            map = Map.Medium;
            break;
        }
        else if (userInput == "small")
        {
            map = Map.Small;
            break;
        }
        else
        {
            Console.WriteLine("Please enter your map size selection.");
        }
    }
}

Player player = new(map);

Console.Clear();
Printer.PrintList(Printer.openingLines);
Console.WriteLine();
Printer.PrintList(Printer.helpLines);
Printer.ColorPrint("\nPress any key to begin!");
Console.ReadKey(true);

while (true)
{
    Console.Clear();

    Printer.PrintUI(ref map, ref player);
    Console.WriteLine("\nWhat's your next move?");
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.W:
        case ConsoleKey.UpArrow:
        case ConsoleKey.NumPad2:
            player.lastAction = player.Move('N') ? "You walk to the North..." : Printer.wallBonk;
            break;
        case ConsoleKey.D:
        case ConsoleKey.RightArrow:
        case ConsoleKey.NumPad6:
            player.lastAction = player.Move('E') ? "You walk to the East..." : Printer.wallBonk;
            break;
        case ConsoleKey.S:
        case ConsoleKey.DownArrow:
        case ConsoleKey.NumPad8:
            player.lastAction = player.Move('S') ? "You walk to the South..." : Printer.wallBonk;
            break;
        case ConsoleKey.A:
        case ConsoleKey.LeftArrow:
        case ConsoleKey.NumPad4:
            player.lastAction = player.Move('W') ? "You walk to the West..." : Printer.wallBonk;
            break;
        case ConsoleKey.E:
        case ConsoleKey.Enter:
        case ConsoleKey.NumPad5:
            if (player.CurrentRoom is FountainRoom)
            {
                Map.Fountain.ToggleFountain();
                player.lastAction = Map.Fountain.IsFountainEnabled ? "You have activated the Fountain of Objects!" : "You have deactivated the Fountain of Objects... why?";
            }
            else
                player.lastAction = "There's nothing to enable here...";
            break;
        case ConsoleKey.H:
        case ConsoleKey.F1:
            Console.Clear();
            Printer.PrintList(Printer.helpLines);
            Printer.ColorPrint("\nPress any key to return.");
            Console.ReadKey(true);
            break;
        case ConsoleKey.I:
        case ConsoleKey.Tab:
            Printer.ItemMenu(player.Inventory);
            break;
        default:
            break;
    }

    // Check if we've won first
    if (player.CurrentRoom is GateRoom && Map.Fountain.IsFountainEnabled)
        break;
    // Then if something bad is happening
    foreach (Monster monster in map.MonsterList)
    {
        if (player.X == monster.X && player.Y == monster.Y)
        {
            Combat.CombatLoop(player, monster, ref map);
            Console.WriteLine();
            if (!player.IsDead)
            {
                Printer.ColorPrint("Combat has concluded, press any key to continue.");
                Console.ReadKey();
            }
            break;
        }
    }
    
    if (player.IsDead)
        break;
}
if (!player.IsDead)
{
    Console.Clear();
    Printer.ColorPrint("You've done it! The Fountain of Objects has been activated and you've escaped with your life! Consolas will prosper with objects now!");
}
else
{
    Console.Clear();
    Console.WriteLine("Your journey has ended and you're surrounded by darkness... Wait, you see a light ahead!");
    Printer.ColorPrint("You hear a gruff voice from the light, \"Hey, you, you're finally awake...\"");
    Printer.ColorPrint("Maybe you should just go back to Consolas...");
}