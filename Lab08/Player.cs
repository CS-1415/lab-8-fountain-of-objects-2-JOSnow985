namespace Lab08;
using System.Linq;
using System.Text;

public class Player
{
    // Player starts from 0, 0, top left corner.
    private readonly Map Map;
    public int X = 0;
    public int Y = 0;
    public string CurrentExits => Map.RoomData[Y][X].exits;
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
        Map.UpdateRoomVisitedAt(0, 0, _isvisited: true);
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

    public string Sense(ref Map map)        // Returns a string based on where the player is
    {
        StringBuilder weSenseThis = new();
        if (!string.IsNullOrEmpty(CurrentRoom.Feedback))
            weSenseThis.Append($"{CurrentRoom.Feedback} \n");

        foreach (var (direction, deltaX, deltaY) in Map.cardinals)
            if (CurrentExits.Contains(direction[0])) // Use the first letter in the direction's name, N E S W
            {
                Monster? monster = map.MonsterList.FirstOrDefault(monster => (monster.X, monster.Y) == (X + deltaX, Y + deltaY));
                if (monster is not null)
                {
                    weSenseThis.Append($"From the {direction} - {monster.Feedback} \n");
                }
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
                    break;
                case 'E':
                    X++;
                    break;
                case 'S':
                    Y++;
                    break;
                case 'W':
                    X--;
                    break;
                default:
                    return false;
            }
            Map.UpdateRoomVisitedAt(X, Y, _isvisited: true);
            return true;
        }
    }
}
