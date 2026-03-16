// Jaden Olvera     CS-1410    Fountain of Objects for HW:23
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

Printer.OpeningCrawl();
Console.ReadKey(true);

while (true)
{
    Console.Clear();
    Console.WriteLine($"Currently at: ({player.X},{player.Y}) | HP: {player.Health} |  Exits sensed: ({map.RoomData[player.Y][player.X].exits})");

    Printer.ColorPrint(player.lastAction);

    Printer.ColorPrint(player.Sense());

    Console.WriteLine("\nWhat's your next move?");

    // Player input will be Cyan
    Console.ForegroundColor = ConsoleColor.Cyan;
    switch (Console.ReadLine()?.ToLowerInvariant())
    {
        case "walk north":
        case "move north":
        case "north":
        case "n":
        case "up":
            player.lastAction = player.Move('N') ? "You walk to the North..." : Printer.wallBonk;
            break;
        case "walk east":
        case "move east":
        case "east":
        case "e":
        case "right":
            player.lastAction = player.Move('E') ? "You walk to the East..." : Printer.wallBonk;
            break;
        case "walk south":
        case "move south":
        case "south":
        case "s":
        case "down":
            player.lastAction = player.Move('S') ? "You walk to the South..." : Printer.wallBonk;
            break;
        case "walk west":
        case "move west":
        case "west":
        case "w":
        case "left":
            player.lastAction = player.Move('W') ? "You walk to the West..." : Printer.wallBonk;
            break;
        case "enable":
        case "toggle":
        case "turn on":
        case "activate":
        case "use":
        case "fountain":
            if (player.CurrentRoom is FountainRoom)
            {
                Map.Fountain.ToggleFountain();
                player.lastAction = Map.Fountain.IsFountainEnabled ? "You have activated the Fountain of Objects!" : "You have deactivated the Fountain of Objects... why?";
            }
            else
                player.lastAction = "There's nothing to enable here...";
            break;
        case "help":
            Printer.HelpMenu();
            break;
        case "leave":
        case "exit":
        case "bail":
        case "quit":
            if (player.CurrentRoom is GateRoom)
            {
                Console.Clear();
                Printer.ColorPrint("You decide this is too much and ascend out of the Cavern of Objects. The Uncoded One will probably win now. Nice.");
                return;
            }
            else
                player.lastAction = "You can't leave unless you're at the entrance...";
            break;
        default:
            player.lastAction = "The Rules of the Cavern prevent you from doing... that.";
            break;
    }
    // Return text color to white just in case
    Console.ForegroundColor = ConsoleColor.White;

    // Check if we've won first
    if (player.CurrentRoom is GateRoom && Map.Fountain.IsFountainEnabled)
        break;
    // Then if something bad is happening
    foreach (Monster monster in map.MonsterList)
    {
        if (player.X == monster.X && player.Y == monster.Y)
        {
            Combat.CombatLoop(player, monster, ref map);
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