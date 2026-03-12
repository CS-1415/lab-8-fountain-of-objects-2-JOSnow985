namespace Lab08;

public class Map
{
    public List<(Room room, int x, int y)> RoomList { get; private set; }
    public List<List<string>> ExitsList { get; private set; }
    public List<Obstacle> ObstaclesList { get; private set; }
    public static GateRoom Entrance { get; } = new();
    public static FountainRoom Fountain { get; } = new();
    public static Room EmptyRoom { get; } = new();
    private static (int X, int Y) _boundary = (3, 3); // Small Map is the max dimension
    public static (int X, int Y) Boundary => _boundary;

    public Map(List<(Room, int, int)> rooms, List<List<string>> exits, List<Obstacle> obstacles)
    {
        RoomList = rooms;
        ExitsList = exits;
        ObstaclesList = obstacles;
        _boundary.X = _boundary.Y = exits.Count - 1;    // Maps are square right now but could be different later?
    }

    public static Map Small => new(smallMap.rooms, smallMap.exits, smallMap.obstacles);
    public static Map Medium => new(mediumMap.rooms, mediumMap.exits, mediumMap.obstacles);
    public static Map Large => new(largeMap.rooms, largeMap.exits, largeMap.obstacles);

    // Small maps are 4x4, have 1 Amarok, 1 Maelstrom, 1 Pit
    private static (List<List<string>> exits, List<Obstacle> obstacles, List<(Room, int x, int y)> rooms) smallMap = (
    [
        ["S",   "ES",   "EW",   "SW"],
        ["NS",  "NE",   "SW",   "NS"],
        ["NES", "EW",   "NESW", "NSW"],
        ["N",   "E",    "NW",   "N"]
    ],
    [ Amarok.At(3, 0), Maelstrom.At(1, 3), Pit.At(3, 3) ],
    [ (Entrance, 0, 0), (Fountain, 2, 0) ]
    );
    // Medium maps are 6x6, have 2 Amaroks, 1 Maelstrom, 2 Pits
    private static (List<List<string>> exits, List<Obstacle> obstacles, List<(Room, int x, int y)> rooms) mediumMap = (
    [
        ["E",   "EW",   "EW",   "ESW",  "EW",   "SW"],
        ["E",   "EW",   "EW",   "NW",   "ES",   "NSW"],
        ["E",   "ESW",  "EW",   "ESW",  "NW",   "NS"],
        ["ES",  "NW",   "ES",   "NSW",  "E",    "NW"],
        ["NES", "EW",   "NSW",  "NE",   "EW",   "SW"],
        ["N",   "E",    "NEW",  "EW",   "EW",   "NW"]
    ],
    [
        Amarok.At(2, 2), Amarok.At(4, 4),
        Maelstrom.At(5, 2),
        Pit.At(1, 3), Pit.At(4, 3)
    ],
    [ (Entrance, 0, 0), (Fountain, 5, 5) ]
    );

    // Large maps are 8x8, have 3 Amaroks, 2 Maelstroms, 4 Pits
    private static (List<List<string>> exits, List<Obstacle> obstacles, List<(Room, int x, int y)> rooms) largeMap = (
    [
        ["S",   "ES",   "EW",   "EW",   "SW",   "ES",   "W",    "S"],
        ["NE",  "NW",   "ES",   "EW",   "NSW",  "NEW",  "EW",   "NSW"],
        ["ES",  "EW",   "NEW",  "SW",   "NS",   "NE",   "SW",   "NS"],
        ["NS",  "E",    "EW",   "NW",   "NES",  "EW",   "NESW", "NW"],
        ["NES", "EW",   "EW",   "ESW",  "NW",   "S",    "NES",  "SW"],
        ["NS",  "S",    "NESW", "NS",   "ES",   "NW",   "NS",   "NS"],
        ["NS",  "NE",   "EW",   "NEW",  "NSW",  "ES",   "NW",   "NS"],
        ["NE",  "EW",   "EW",   "EW",   "NEW",  "NEW",  "EW",   "NW"]
    ],
    [
        Amarok.At(0, 3), Amarok.At(1, 5), Amarok.At(5, 7),
        Maelstrom.At(3, 1), Maelstrom.At(4, 4),
        Pit.At(2, 2), Pit.At(0, 7), Pit.At(6, 1), Pit.At(7, 6)
    ],
    [ (Entrance, 0, 0), (Fountain, 0, 2) ]
    );
    // public enum Direction { North, East, South, West }
}
