namespace Lab08;

public static class Printer
{
    public static readonly string wallBonk = "You bump into a wall of the cavern, you can't go that way.";
    public static readonly List<string> blueWords = ["fountain", "rushing", "dripping", "activate", "enable"];
    public static readonly List<string> yellowWords = ["sunlit", "light", "derezzing"];
    public static readonly List<string> redWords = ["amarok", "maelstrom", "pit", "pits", "died", "died.", "deactivated", "why?", "rotten", "stench", "growling", "groaning", "fire", "darkness", "draft", "dangers"];
    public static readonly List<string> magentaWords = ["north", "east", "south", "west", "walk", "bump", "feel", "hear", "smell", "see"];
    public static readonly List<string> cyanWords = ["you", "small", "medium", "large", "commands", "shoot", "press", "help"];
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
    public static void OpeningCrawl()
    {
        Console.Clear();
        ColorPrint("You descend into the Cavern of Objects, a maze of rooms with dangerous pits in search of the Fountain of Objects.");
        ColorPrint("Light is visible only here, in the entrance, a roiling wall of darkness covers the passage to the rest of the cavern.");
        ColorPrint("You must navigate the Cavern with your other senses.");
        ColorPrint("Find the Fountain of Objects and activate it, then return to the entrance.");
        Console.Write("\nRemember you can use ");
        ColorPrint("help to see the controls!\n");
        ColorPrint("--- Dangers ---\n");
        ColorPrint("Pits in the cavern pull you in to a certain death, but you will feel a draft from nearby rooms.");
        ColorPrint("The Maelstrom are violent forces of sentient wind, they will teleport you somewhere else in the caverns. You will be able to hear their growling and groaning from nearby rooms.");
        ColorPrint("Amarok roam the caverns. Encountering one is certain death, but you can smell their rotten stench from nearby rooms.");
        ColorPrint("You carry a bow and a quiver of arrows. You can use them to shoot Amarok or Maelstrom in the caverns but be warned: you have a limited supply.");
        Console.WriteLine();
        ColorPrint("Press any key to begin!");
    }

    public static void HelpMenu()
    {
        Console.Clear();
        ColorPrint("--- Commands ---");
        ColorPrint("North / East / South / West : Attempt to walk that direction.");
        ColorPrint("Activate : Attempt to activate the Fountain of Objects.");
        ColorPrint("Fire <Direction>: Attempt to fire your bow in a direction (only North, East, South, West!).");
        Console.WriteLine();
        ColorPrint("Press any key to return.");
        Console.ReadKey(true);
    }

    public static void ItemMenu(List<Item> inventory)
    {
        Console.Clear();
        ColorPrint("--- Your Inventory ---");
        foreach(Item i in inventory)
        {
            ColorPrint($"{i.Info.Name}:");
            ColorPrint($"    {i.Info.Description}");
        }
        ColorPrint("Press any key to return.");
        Console.ReadKey(true);
    }
}
