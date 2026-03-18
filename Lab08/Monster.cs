namespace Lab08;

public abstract class Monster
{
    public int X;
    public int Y;
    public string Name;
    public string Feedback;
    public int Health;
    public bool Dead => Health <= 0;
    public (int Attack, int Defense) MonStats;
    public bool hasSpecAtk = false;
    public List<Item> Inventory = [];
    public static Random rng = new();

    public Monster(string n, int x, int y, string s)
    {
        Name = n;
        X = x;
        Y = y;
        Feedback = s;
    }

    public abstract void SpecialAttack(Player player);  // Instead of "tripping" that fires once, a special attack
    protected List<Item> RollInventory()
    {
        List<Item> itemList = [];
        for (int i = Health; i > 0; i--)        // One loot roll per monster hp, probably poorly balanced
        {
            int roll = rng.Next(1, 101);
            if (roll >= 75)
                itemList.Add(Upgrade.Weapon);   // 25% chance to add a weapon upgrade
            else if (roll <= 25)
                itemList.Add(Upgrade.Armor);    // 25% chance to add an armor upgrade
        }

        return itemList;
    }
}

public class Wizzrobe : Monster // Maelstrom renamed to Wizzrobe for fun, teleport simplified
{
    bool hasTeleported = false;
    public Wizzrobe(int x, int y) : base("Wizzrobe", x, y, "You hear the delirious muttering of a Wizzrobe, \"Ah, Kos. Or some say Kosm...\", a Wizzrobe is nearby.")
    {
        Health = 3;
        MonStats = (1, 0);
        hasSpecAtk = true;
        Inventory = RollInventory();
    }
    public static Wizzrobe At(int x, int y) => new(x, y);
    public override void SpecialAttack(Player player)   // Wizzrobe returns player to the entrance
    {
        if (!hasTeleported)
            (hasTeleported, player.X, player.Y) = (true, 0, 0);
        else
            return;     // Maybe the Wizzrobe explodes if he tries teleporting the player again?
        string Wizzrobed = "The Wizzrobe casts teleport on you! You're back at the entrance, weren't Wallmasters the ones that did that?";

        Printer.ColorPrint(Wizzrobed);
        player.lastAction = Wizzrobed;
    }
}

public class Soldier : Monster
{
    public Soldier(int x, int y) : base("\"Elite\" Knight", x, y, "The loud clattering of metal armor means an \"Elite\" Knight is near.")
    {
        Health = 5;
        MonStats = (5,3);
        Inventory = RollInventory();
    }
    public static Soldier At(int x, int y) => new(x, y);
    public override void SpecialAttack(Player player)
    {
        throw new NotImplementedException("This monster does not have a special attack!");
    }
}

public class Drgn : Monster
{
    public Drgn(int x, int y) : base("Drgn", x, y, "Low growls and gouts of flame and smoke signal the presence of a Drgn, watch out!")
    {
        Health = 10;
        MonStats = (7,5);
        Inventory = RollInventory();
    }
    public static Drgn At(int x, int y) => new(x, y);
    public override void SpecialAttack(Player player)
    {
        throw new NotImplementedException("This monster does not have a special attack!");
    }
}

public class Rodent : Monster
{
    public Rodent(int x, int y) : base("R.O.U.S.", x, y, "Squeaks and scratching sounds, it could only be a Rodent of Unusual Size!")
    {
        Health = 2;
        MonStats = (1,0);
        Inventory = RollInventory();
    }
    public static Rodent At(int x, int y) => new(x, y);
    public override void SpecialAttack(Player player)
    {
        throw new NotImplementedException("This monster does not have a special attack!");
    }
}
