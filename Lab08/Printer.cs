namespace Lab08;

public static class Printer
{
    // I don't like all these public static strings, probably better to read from a file
    public static readonly string wallBonk = "You bump into a wall of the cavern, you can't go that way.";
    public static readonly List<string> blueWords = ["fountain", "rushing", "dripping", "activate", "enable"];
    public static readonly List<string> yellowWords = ["sunlit", "light", "mote", "shimmering", "inventory", "player", "power", "defense"];
    public static readonly List<string> redWords = ["wizzrobe", "wizzrobes", "rodent", "rodents", "r.o.u.s.", "\"elite\"", "elite", "knight", "knights", "drgn", "dangers", "100", "flame"];
    public static readonly List<string> magentaWords = ["north", "east", "south", "west", "walk", "bump", "feel", "hear", "smell", "see", "hp:"];
    public static readonly List<string> cyanWords = ["small", "medium", "large", "commands", "press", "help"];
    public static readonly List<string> openingLines = [
        "You descend into the Cavern of Objects in search of the Fountain of Objects.",
        "A maze with dangerous enemies awaits you.",
        "You have a dull sword and a locket, these items can be upgraded with motes of power from enemies.",
        "Find the Fountain of Objects, activate it, and return to the entrance."
    ];
    public static readonly List<string> helpLines = [
        "--- Commands ---",
        "[ Arrow Keys ] or [ W / A / S / D ]: Attempt to walk that direction.",
        "[ E / Enter ] : Attempt to activate the Fountain of Objects.",
        "[ H / F1 ]  : Open this help menu!",
        "[ I / Tab ] : Open your inventory.",
        "\n--- Dangers ---",
        "Drgn - Dangerous, fire breathing, scaly enemies that love BBQ'ing unprepared adventurers!",
        "\"Elite\" Knights - Heavily armored enemies, nobody knows what they look like under the armor.",
        "Rodents - Unusually large for rodents, they love to inhabit caverns and attack adventurers.",
        "Wizzrobes - Mages driven insane by calculating complex teleportation spells, they're relatively weak but they can teleport you back to the entrance of the cavern."
    ];
    public static void MapSelect()
    {
        Console.Clear();
        Console.WriteLine("You're nearing the Cavern of Objects, how big do you expect the cavern to be?");
        ColorPrint("Small --- Medium --- Large");
        Console.WriteLine("Enter a selection to continue...");
    }
    public static void ColorPrint(string input)
    {
        string[] words = input.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            string lowerWord = words[i].ToLowerInvariant();
            string printWord = words[i];

            if (blueWords.Contains(lowerWord))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                if (lowerWord == "fountain" && i + 2 < words.Length)  // fountain should always followed by "of objects(.), so we want to print the next two words blue too
                {
                    printWord = $"{words[i]} {words[i + 1]} {words[i + 2]}";
                    i += 2;
                }
            }
            else if (yellowWords.Contains(lowerWord))
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (redWords.Contains(lowerWord))
                Console.ForegroundColor = ConsoleColor.DarkRed;
            else if (magentaWords.Contains(lowerWord))
                Console.ForegroundColor = ConsoleColor.Magenta;
            else if (cyanWords.Contains(lowerWord))
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write($"{printWord} ");
            Console.ForegroundColor = ConsoleColor.White;   // always return color to white!
        }
        Console.Write("\n");    //Finally, next line
    }
    public static void PrintList(List<string> lines)
    {
        foreach (string line in lines)
            ColorPrint(line);
    }

    public static void ItemMenu(List<Item> inventory)
    {
        Console.Clear();
        ColorPrint("--- Your Inventory ---");
        PrintInventory(inventory);
        ColorPrint("Press any key to return.");
        Console.ReadKey(true);
    }

    public static void PrintInventory(List<Item> inventory)
    {
        foreach (Item i in inventory)
        {
            ColorPrint($"{i.Info.Name} -");
            ColorPrint($"    {i.Info.Description} \n");
        }
    }

    public static void PrintUI(ref Map map, ref Player player)
    {   
        PrintTopFrame();
        PrintTopRow(ref map, ref player);
        PrintMidRow(ref map, ref player);
        PrintBotRow(ref map, ref player);
        PrintBotFrame();
        PrintControls();
        ColorPrint(player.Sense(ref map));
        ColorPrint(player.lastAction);
    }

    public static void PrintTopFrame()
    {
        Console.WriteLine(  "         [ 🢁 ]    "  );
        Console.WriteLine(  "     ┏━━━━━━━━━━━┓"  );
    }

    public static void PrintBotFrame()
    {
        Console.WriteLine(  "     ┗━━━━━━━━━━━┛"  );
        Console.WriteLine(  "         [ 🢃 ]    "  );
    }

    public static void PrintTopRow(ref Map map, ref Player player)
    {   
        (string top, string mid, string bot) cell = ("   ", "   ", "   ");
        if (player.CurrentExits.Contains('N'))
        {
            cell = ConstructCell(ref map, player.X, player.Y - 1);
        }

        Console.WriteLine("     ┃AT  " + cell.mid + "  DF┃");
        Console.WriteLine("     ┃" + $"{player.GearStats.Attack:D2}" + "  " + cell.bot + "  " + $"{player.GearStats.Defense:D2}" + "┃");
    }

    public static void PrintMidRow(ref Map map, ref Player player)
    {   
        (string top, string mid, string bot) cellW = ("   ", "   ", "   ");
        (string top, string mid, string bot) cellM;
        (string top, string mid, string bot) cellE = ("   ", "   ", "   ");
        if (player.CurrentExits.Contains('W'))
        {
            cellW = ConstructCell(ref map, player.X - 1, player.Y);
        }

        cellM = ConstructCell(ref map, player.X, player.Y);

        if (player.CurrentExits.Contains('E'))
        {
            cellE = ConstructCell(ref map, player.X + 1, player.Y);
        }

        Console.WriteLine("     ┃  " + cellW.top[1] + cellW.top[2] + cellM.top + cellE.top[0] + cellE.top[1]+ "  ┃");
        Console.WriteLine("[ 🢀 ]┃  " + cellW.mid[1] + cellW.mid[2] + cellM.mid + cellE.mid[0] + cellE.mid[1] + "  ┃[ 🢂 ]");
        Console.WriteLine("     ┃  " + cellW.bot[1] + cellW.bot[2] + cellM.bot + cellE.bot[0] + cellE.bot[1] + "  ┃");
    }

    public static void PrintBotRow(ref Map map, ref Player player)
    {   
        (string top, string mid, string bot) cell = ("   ", "   ", "   ");
        if (player.CurrentExits.Contains('S'))
        {
            cell = ConstructCell(ref map, player.X, player.Y + 1);
        }

        Console.WriteLine( "     ┃XY  " + cell.top + "  HP┃");
        Console.WriteLine("     ┃" + $"{player.X}" + $"{player.Y}" + "  " + cell.mid + "  " + $"{player.Health:D2}" + "┃");
    }

    public static (string top, string middle, string bottom) ConstructCell(ref Map map, int x, int y)
    {
        string exits = map.RoomData[y][x].exits;
        char[][] cell =
        [
            ['┛', ' ', '┗'],
            [' ', ' ', ' '],
            ['┓', ' ', '┏']
        ];

        cell[1][1] = map.RoomData[y][x].IsVisited ? (map.RoomData[y][x].IsClear ? ' ' : '!') : '?'; 

        // Check if we need to block off cardinals
        if (!exits.Contains('N'))
            cell[0][1] = '━';

        if (!exits.Contains('E'))
            cell[1][2] = '┃';

        if (!exits.Contains('S'))
            cell[2][1] = '━';

        if (!exits.Contains('W'))
            cell[1][0] = '┃';

        // Find correct char for diagonals
        //NE
        if (exits.Contains('N') && !exits.Contains('E'))
            cell[0][2] = '┃';
        else if (!exits.Contains('N') && exits.Contains('E'))
            cell[0][2] = '━';
        else if (!exits.Contains('N') && !exits.Contains('E'))
            cell[0][2] = '┓';

        //SE
        if (exits.Contains('E') && !exits.Contains('S'))
            cell[2][2] = '━';
        else if (!exits.Contains('E') && exits.Contains('S'))
            cell[2][2] = '┃';
        else if (!exits.Contains('E') && !exits.Contains('S'))
            cell[2][2] = '┛';

        //SW
        if (exits.Contains('S') && !exits.Contains('W'))
            cell[2][0] = '┃';
        else if (!exits.Contains('S') && exits.Contains('W'))
            cell[2][0] = '━';
        else if (!exits.Contains('S') && !exits.Contains('W'))
            cell[2][0] = '┗';

        //NW
        if (exits.Contains('N') && !exits.Contains('W'))
            cell[0][0] = '┃';
        else if (!exits.Contains('N') && exits.Contains('W'))
            cell[0][0] = '━';
        else if (!exits.Contains('N') && !exits.Contains('W'))
            cell[0][0] = '┏';

        string top = $"{cell[0][0]}{cell[0][1]}{cell[0][2]}";
        string mid = $"{cell[1][0]}{cell[1][1]}{cell[1][2]}";
        string bot = $"{cell[2][0]}{cell[2][1]}{cell[2][2]}";
        return (top, mid, bot);
    }

    static void PrintControls()
    {     
        Console.WriteLine();
        Console.Write("   ");
        ColorPrint("[ 🢁 / 🢀 / 🢃 / 🢂 ]");
        Console.Write("   ");
        ColorPrint("[ W / A / S / D ]");
        Console.Write("   ");
        ColorPrint("[E]nable | [H]elp");
        Console.Write("      ");
        ColorPrint("[I]nventory\n");
    }
}
