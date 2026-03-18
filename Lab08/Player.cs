namespace Lab08;
using System.Linq;
using System.Text;

public class Player
{
    // Player starts from 0, 0, top left corner.
    private readonly Map Map;
    public int X = 0;
    public int Y = 0;
    public int Health = 50;
    public bool IsDead => Health <= 0;
    public (int Attack, int Defense) GearStats => CheckHighestGear();
    public Weapon Weapon = new Weapon();
    public Armor Armor = new Armor();
    public List<Item> Inventory = [];
    public string lastAction = "";      // Holds the last action we took so it's easier to set this from different places

    // Checks if any room in the room list has matching coordinates to us, returns an empty room if not.
    public Room CurrentRoom => Map.SpecRoomList.Any(tuple => (tuple.x, tuple.y) == (X, Y)) ? Map.SpecRoomList.First(tuple => (tuple.x, tuple.y) == (X, Y)).room : Map.EmptyRoom;

    public Player(Map selectedMap)
    { 
        Map = selectedMap;
        Inventory.Add(Weapon);
        Inventory.Add(Armor);
    }

    private (int, int) CheckHighestGear()
    {
        UpdateGear();
        
        (int atk, int def) = (1, 1);
        foreach(var item in Inventory)
        {
            if (item is Weapon weapon && weapon.Attack > atk)
            {
                atk = weapon.Attack;
            }
            else if (item is Armor armor && armor.Defense > def)
            {
                def = armor.Defense;
            }
        }
        return (atk, def);
    }

    public void UpdateGear()
    {
        (int atkLevel, int defLevel) = (1, 1);
        foreach(var item in Inventory)
        {
            if (item is Upgrade upgrade)
            {
                if (upgrade.IsForWeapon)
                    atkLevel++;
                else
                    defLevel++;
            }
        }
        (Weapon.Level, Armor.Level) = (atkLevel, defLevel);
    }

    public string Sense(ref Map map)        // Returns a string based on where the player is and what they're close to
    {
        StringBuilder weSenseThis = new();

        weSenseThis.Append(CurrentRoom.Feedback);       // May be nothing, might be Gate or Fountain Room feedback

        foreach (Monster monster in map.MonsterList)
        {
            int deltaX = Math.Abs(X - monster.X);
            int deltaY = Math.Abs(Y - monster.Y);
            (bool adjacentX, bool adjacentY) = (deltaX <= 1, deltaY <=1);
            if (deltaX <= 1 && deltaY <= 1)
                if (map.RoomData[Y][X].exits.Contains('A'))
                weSenseThis.Append(monster.Feedback);      // If an obstacle is near, we retrieve the string for it's feedback
        }

        return weSenseThis.ToString();
    }

    public bool Move(char dir)
    {
        if (!Map.RoomData[Y][X].exits.Contains(dir))
            return false;
        else
        {
            switch (dir)
            {
                case 'N':
                    Y--;
                    return true;
                case 'E':
                    X++;
                    return true;
                case 'S':
                    Y++;
                    return true;
                case 'W':
                    X--;
                    return true;
                default:
                    return false;
            }
        }
    }
}
