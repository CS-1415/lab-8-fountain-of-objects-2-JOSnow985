namespace Lab08;

public class Map
{
    public List<(Room room, int x, int y)> SpecRoomList { get; }
    public List<List<(string exits, bool IsClear, bool IsVisited)>> RoomData { get; }
    public List<Monster> MonsterList { get; }
    // private static (int X, int Y) _boundary = (3, 3); // Small Map is the max dimension
    // public static (int X, int Y) Boundary => _boundary;

    public Map(List<(Room, int, int)> rooms, List<List<string>> exits)
    {
        SpecRoomList = rooms;
        RoomData = AddRoomTags(exits);
        // _boundary.X = _boundary.Y = exits.Count - 1;    // Maps are square right now but could be different later?\
        MonsterList = SpawnMonsters();
    }

    public static Map Small => new(smallMap.rooms, smallMap.exits);
    public static Map Medium => new(mediumMap.rooms, mediumMap.exits);
    public static Map Large => new(largeMap.rooms, largeMap.exits);
    private List<Monster> SpawnMonsters()
    {
        return [];
    }
    private List<List<(string exits, bool IsClear, bool IsVisited)>> AddRoomTags(List<List<string>> exitList)
    {
        return [];
    }

    // Rooms that are made then placed according to map size
    public static GateRoom Entrance { get; } = new();
    public static FountainRoom Fountain { get; } = new();
    public static Room EmptyRoom { get; } = new();

    // Small maps are 4x4, have 1 Amarok, 1 Maelstrom, 1 Pit
    private static (List<List<string>> exits, List<(Room, int x, int y)> rooms) smallMap = (
    [
        ["S",   "ES",   "EW",   "SW"],
        ["NS",  "NE",   "SW",   "NS"],
        ["NES", "EW",   "NESW", "NSW"],
        ["N",   "E",    "NW",   "N"]
    ],
    [ (Entrance, 0, 0), (Fountain, 2, 0) ]
    );
    // Medium maps are 6x6, have 2 Amaroks, 1 Maelstrom, 2 Pits
    private static (List<List<string>> exits, List<(Room, int x, int y)> rooms) mediumMap = (
    [
        ["E",   "EW",   "EW",   "ESW",  "EW",   "SW"],
        ["E",   "EW",   "EW",   "NW",   "ES",   "NSW"],
        ["E",   "ESW",  "EW",   "ESW",  "NW",   "NS"],
        ["ES",  "NW",   "ES",   "NSW",  "E",    "NW"],
        ["NES", "EW",   "NSW",  "NE",   "EW",   "SW"],
        ["N",   "E",    "NEW",  "EW",   "EW",   "NW"]
    ],
    [ (Entrance, 0, 0), (Fountain, 5, 5) ]
    );

    // Large maps are 8x8, have 3 Amaroks, 2 Maelstroms, 4 Pits
    private static (List<List<string>> exits, List<(Room, int x, int y)> rooms) largeMap = (
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
    [ (Entrance, 0, 0), (Fountain, 5, 4) ]
    );
}
