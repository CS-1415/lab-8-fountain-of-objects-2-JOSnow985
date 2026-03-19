namespace Lab08;

public class Map
{
    public List<(Room room, int x, int y)> SpecRoomList { get; }
    public List<List<(string exits, bool IsClear, bool IsVisited)>> RoomData { get; }
    public List<Monster> MonsterList { get; }

    public Map(List<(Room, int, int)> rooms, List<List<string>> exits)
    {
        SpecRoomList = rooms;
        RoomData = AddRoomTags(exits);
        MonsterList = SpawnMonsters(exits);
    }

    public static Map Small => new(smallMap.rooms, smallMap.exits);
    public static Map Medium => new(mediumMap.rooms, mediumMap.exits);
    public static Map Large => new(largeMap.rooms, largeMap.exits);
    private List<Monster> SpawnMonsters(List<List<string>> exits)
    {
        List<Monster> monsters = [];
        int Y = 0;
        int X = 0;
        foreach (var row in exits)                  // First dimension in exits is Y
        {
            foreach (var column in exits)           // Next is X
            {
                if (Monster.rng.Next(1, 101) >= 70) // 30% chance for a monster to be placed
                    monsters.Add(RandomMonsterAt(X, Y));
                X++;
            }
            Y++;
            X = 0;
        }
        return monsters;
    }

    // Returns a monster spawned at the passed x, y
    private Monster RandomMonsterAt(int x, int y)
    {
        UpdateRoomClearedAt(x, y);
        return Monster.rng.Next(1, 101) switch
        {
            var roll when roll <= 20 => Wizzrobe.At(x, y),                  // 20%
            var roll when 20 < roll && roll <= 45 => Soldier.At(x, y),      // 25%
            var roll when 45 < roll && roll <= 55 => Drgn.At(x, y),         // 10%
            _ => Rodent.At(x, y)                                            // Remaining 45%
        };
    }

    // Updates RoomData at a passed x
    public void UpdateRoomClearedAt(int x, int y, bool _isclear = false) => RoomData[y][x] = (RoomData[y][x].exits, _isclear, RoomData[y][x].IsVisited);
    public void UpdateRoomVisitedAt(int x, int y, bool _isvisited = false) => RoomData[y][x] = (RoomData[y][x].exits, RoomData[y][x].IsClear, _isvisited);
    private List<List<(string exits, bool IsClear, bool IsVisited)>> AddRoomTags(List<List<string>> exits)
    {   // Iterates through the exit list from the map and makes a new list of tuples
        // the exits and two flags that indicate if it's been visited and if a monster is there or not.
        List<List<(string exits, bool IsClear, bool IsVisited)>> taggedRooms = [];
        foreach (var row in exits)
        {
            List<(string exits, bool IsClear, bool IsVisited)> taggedRow = [];
            foreach (var col in row)
            {
                taggedRow.Add((col, true, false));
            }
            taggedRooms.Add(taggedRow);
        }
        return taggedRooms;
    }

    // Rooms that are made then placed according to map size
    public static GateRoom Entrance { get; } = new();
    public static FountainRoom Fountain { get; } = new();
    public static Room EmptyRoom { get; } = new();

    // Small maps are 4x4
    private static (List<List<string>> exits, List<(Room, int x, int y)> rooms) smallMap = (
    [
        ["S",   "ES",   "EW",   "SW"],
        ["NS",  "NE",   "SW",   "NS"],
        ["NES", "EW",   "NESW", "NSW"],
        ["N",   "E",    "NW",   "N"]
    ],
    [ (Entrance, 0, 0), (Fountain, 2, 0) ]
    );
    // Medium maps are 6x6
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

    // Large maps are 8x8
    private static (List<List<string>> exits, List<(Room, int x, int y)> rooms) largeMap = (
    [
        ["S",   "ES",   "EW",   "EW",   "SW",   "ES",   "W",    "S"],
        ["NE",  "NW",   "ES",   "EW",   "NSW",  "NES",  "EW",   "NSW"],
        ["ES",  "EW",   "NEW",  "SW",   "NS",   "NE",   "SW",   "NS"],
        ["NS",  "E",    "EW",   "NW",   "NES",  "EW",   "NESW", "NW"],
        ["NES", "EW",   "EW",   "ESW",  "NW",   "S",    "NES",  "SW"],
        ["NS",  "S",    "NESW", "NS",   "ES",   "NW",   "NS",   "NS"],
        ["NS",  "NE",   "EW",   "NEW",  "NSW",  "ES",   "NW",   "NS"],
        ["NE",  "EW",   "EW",   "EW",   "NEW",  "NEW",  "EW",   "NW"]
    ],
    [ (Entrance, 0, 0), (Fountain, 5, 4) ]
    );

    // Directions and offsets for X and Y based on those directions
    public static readonly (string direction, int deltaX, int deltaY)[] cardinals =
        [
            ("North", 0,-1),
            ("East",  1, 0),
            ("South", 0, 1),
            ("West", -1, 0)
        ];
}
