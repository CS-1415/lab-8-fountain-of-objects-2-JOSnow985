namespace Lab08;
using System.Linq;
using System.Text;

public class Player
{
    // Player starts from 0, 0, top left corner.
    public int X = 0;
    public int Y = 0;
    public bool Dead = false;
    private readonly Map Map;
    public string lastAction = "";      // Holds the last action we took so it's easier to set this from different places
    public int RemainingArrows { get; private set; } = 5;

    // Checks if any room in the room list has matching coordinates to us, returns an empty room if not.
    public Room CurrentRoom => Map.RoomList.Any(tuple => (tuple.x, tuple.y) == (X, Y)) ? Map.RoomList.First(tuple => (tuple.x, tuple.y) == (X, Y)).room : Map.EmptyRoom;

    public Player(Map selectedMap) { Map = selectedMap; }

    public string Sense()        // Returns a string based on where the player is and what they're close to
    {
        StringBuilder weSenseThis = new();

        weSenseThis.Append(CurrentRoom.Feedback);       // May be nothing, might be Gate or Fountain Room feedback

        foreach (Obstacle obstacle in Map.ObstaclesList)
        {
            if (Math.Abs(X - obstacle.X) <= 1 && Math.Abs(Y - obstacle.Y) <= 1)
                weSenseThis.Append(obstacle.Feedback);      // If an obstacle is near, we retrieve the string for it's feedback
        }

        return weSenseThis.ToString();
    }

    public bool Move(char dir)
    {
        if (!Map.ExitsList[Y][X].Contains(dir))
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

    public bool ShootArrow(char dir)
    {
        if (RemainingArrows <= 0)
        {
            lastAction = "Your bow lets out a helpless twang, you're out of arrows!";
            return false;
        }

        RemainingArrows--;  // We fired an arrow

        // Check if the arrow can make it there
        if (!Map.ExitsList[Y][X].Contains(dir))
        {
            lastAction = "Your arrow snaps in half as it collides with the cavern wall.";
            return false;
        }
        else
        {
            (int x, int y) target = dir switch
            {
                'N' => (X, Y - 1),
                'E' => (X + 1, Y),
                'S' => (X, Y + 1),
                'W' => (X - 1, Y),
                _   => (X, Y),        // Should be safe to use this as a fallback, will always miss
            };
            if (KilledMonsterAt(target.x, target.y))
            {
                lastAction = "A shriek rings out as your arrow pierces a monster, the unnatural darkness is briefly overcome with the light from the shimmering pixels of the derezzing monster.";
                return true;
            }
            else
            {
                lastAction = "Your arrow sails through open air, landing somewhere in the darkness.";
                return false;
            }
        }
    }

    public bool KilledMonsterAt(int x, int y)
    {
        foreach (Obstacle obstacle in Map.ObstaclesList)
        {
            if (obstacle is Maelstrom || obstacle is Amarok)
                if(obstacle.X == x && obstacle.Y == y)
                {
                    Map.ObstaclesList.Remove(obstacle);
                    return true;
                }
        }
        return false;
    }
}
