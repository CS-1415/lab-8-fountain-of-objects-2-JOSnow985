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

Printer.PrintList(Printer.openingLines);
Console.ReadKey(true);

while (true)
{
    Console.Clear();
    Console.WriteLine($"Currently at: ({player.X},{player.Y}) | HP: {player.Health} |  Exits sensed: ({map.RoomData[player.Y][player.X].exits})");

    Printer.ColorPrint(player.lastAction);

    Printer.ColorPrint(player.Sense(ref map));

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
            Printer.PrintList(Printer.helpLines);
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
            Printer.ColorPrint("Combat has concluded, press any key to continue.");
            Console.ReadKey();
            break;
        }
    }
    
    if (player.IsDead)
        break;
}
if (!player.IsDead)
{
    Console.Clear();
    Printer.ColorPrint("You've done it! The Fountain of Objects has been activated and you've escaped with your life! Good job!");
}