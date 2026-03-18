namespace Lab08;

public static class Printer
{
    public static readonly string wallBonk = "You bump into a wall of the cavern, you can't go that way.";
    public static readonly List<string> blueWords = ["fountain", "rushing", "dripping", "activate", "enable"];
    public static readonly List<string> yellowWords = ["sunlit", "light", "derezzing", "inventory", "player", "power", "defense"];
    public static readonly List<string> redWords = ["wizzrobe", "wizzrobes", "rodent", "rodents", "r.o.u.s.", "\"elite\"", "knight", "knights", "drgn", "dangers"];
    public static readonly List<string> magentaWords = ["north", "east", "south", "west", "walk", "bump", "feel", "hear", "smell", "see"];
    public static readonly List<string> cyanWords = ["small", "medium", "large", "commands", "press", "help"];
    public static readonly List<string> openingLines = [
        "You descend into the Cavern of Objects in search of the Fountain of Objects.",
        "A maze with dangerous enemies awaits you.",
        "You have a dull sword and a locket, these items can be upgraded with motes of power from enemies.",
        "Find the Fountain of Objects, activate it, and return to the entrance.",
        "\nYou can always press [H] to see a help menu!!\n",
        "--- Dangers ---",
        "Drgn - Dangerous, fire breathing, scaly enemies that love BBQ'ing unprepared adventurers!",
        "\"Elite\" Knights - Heavily armored enemies, nobody knows what they look like under the armor.",
        "Rodents - Unusually large for rodents, they love to inhabit caverns and attack adventurers.",
        "Wizzrobes - Mages driven insane by calculating complex teleportation spells, they're relatively weak but they can teleport you back to the entrance of the cavern.",
        "\nPress any key to begin!"
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
        "Wizzrobes - Mages driven insane by calculating complex teleportation spells, they're relatively weak but they can teleport you back to the entrance of the cavern.",
        "\nPress any key to return."
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
        Console.Clear();
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
            ColorPrint($"{i.Info.Name}:");
            ColorPrint($"    {i.Info.Description}");
        }
    }
}
