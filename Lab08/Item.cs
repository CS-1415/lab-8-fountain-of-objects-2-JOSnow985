namespace Lab08;

public abstract class Item   // Basic Item
{
    public int Level = 1;
    public abstract (string Name, string Description) Info {get;}
}

public class Upgrade : Item
{
    private bool _forweapon;
    public bool IsForWeapon => _forweapon;
    public override (string Name, string Description) Info => ($"Mote of {(_forweapon ? "Power" : "Defense")}", $"A small speck of shimmering data, having it in your inventory is making your {(_forweapon ? "weapon" : "armor")} stronger.");
    public Upgrade(bool isWeaponUpgrade) => _forweapon = isWeaponUpgrade;
    public static Upgrade Weapon = new(true);
    public static Upgrade Armor = new(false);
}

public class Weapon : Item
{
    public int MaxLevel = 6;
    public int Attack => Level < 6 ? Level : 99;   // Returns attack based on the weapon level
    public override (string Name, string Description) Info
    {
        get
        {
            return Level switch
            {
                1 => ("Dull Sword", "An old, dull sword. Better than nothing"),
                2 => ("Broken Giant's Knife", "A giant's knife that somebody already broke, you could get it fixed but there's no time for a trading quest right now."),
                3 => ("Z Sword", "An extremely heavy sword. It doesn't seem to be very good, but you don't think Old Kai is going to appear if you break it."),
                4 => ("Razor Sword", "A sword with a fancy split design, good thing this isn't Termina or it would go dull after 100 strikes."),
                5 => ("Galaxy Sword", "A farmer traded a cool rock to a desert to get this, he's going to have to pay 50,000g to get it back!"),
                _ => ("Super Shotgun", "A break-action double-barrel demon-deleter, nothing short of a cyberdemon stands a chance.")
            };
        }
    }
}

public class Armor : Item
{
    public int MaxLevel = 6;
    public int Defense => Level < 6 ? Level : 99;
    public override (string Name, string Description) Info
    {
        get
        {
            return Level switch
            {
                1 => ("Saint's Locket", "A locket given to you by your friend, Saint Jiub, a note inside reads \"Not even last night's storm could wake you\"."),
                2 => ("Running Shoes", "Professors have long lectured those who try to run without these."),
                3 => ("Blue Mail", "A blue, tunic-shaped mail, a chest in an ice palace is empty now without this."),
                4 => ("Armored Duster", "A travel-worn duster with sand permanently stuck to the bottom, you feel like you could patrol the Mojave while wearing this!"),
                5 => ("Mirror Shield", "A shield with an unblemishable mirrored surface, the faint symbol of a far away land can be seen at the right angle."),
                _ => ("Abyssal Armor", "Made of all ten collectible armor pieces! A horsemen somewhere's going to want this back...")
            };
        }
    }
}