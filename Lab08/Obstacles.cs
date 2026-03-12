namespace Lab08;

public abstract class Obstacle
{
    // Obstacles have locations
    public int X;
    public int Y;
    public string Feedback;
    public Obstacle (int x, int y, string feedback)
    {
        X = x;
        Y = y;
        Feedback = feedback;
    }

    public abstract void TripPlayer(Player player);         // Every Obstacle will do something different to the player
}

public class Pit : Obstacle
{
    public Pit(int x, int y) : base(x, y, "You feel a draft, a pit must be nearby. ") { }
    public static Pit At(int x, int y) => new Pit(x, y);    // Will give us a cute way to call up a new Pit at the coordinates
    public override void TripPlayer(Player player)          // Pit just kills the player and ends the game
    {
        player.Dead = true;
        Console.Clear();
        Printer.ColorPrint("You have fallen into a pit and died. Your journey ends here. ");
    }
}

public class Maelstrom : Obstacle
{
    public Maelstrom(int x, int y) : base(x, y, "You hear the growling and groaning of a maelstrom nearby. ") { }
    public static Maelstrom At(int x, int y) => new Maelstrom(x, y);
    public override void TripPlayer(Player player)          // Maelstrom moves player one space north, two spaces east, itself one space south, two spaces west
    {
        int playerX = player.X + 2;
        int playerY = player.Y - 1;
        if (playerX > Map.Boundary.X)
            playerX -= Map.Boundary.X + 1;
        if (playerY < 0)
            playerY += Map.Boundary.Y + 1;

        int maelstromX = X - 2;
        int maelstromY = Y + 1;
        if (X < 0)
            maelstromX += Map.Boundary.X + 1;
        if (Y > Map.Boundary.Y)
            maelstromY -= Map.Boundary.Y + 1;

        (player.X, player.Y) = (playerX, playerY);
        (X, Y) = (maelstromX, maelstromY);

        player.lastAction = "The Maelstrom casts teleport on you! You're somewhere else now!";
    }
}

public class Amarok : Obstacle
{
    public Amarok(int x, int y) : base(x, y, "You can smell the rotten stench of an amarok nearby. ") { }
    public static Amarok At(int x, int y) => new Amarok(x, y);
    public override void TripPlayer(Player player)          // Amarok also just kills the player and ends the game
    {
        player.Dead = true;
        Console.Clear();
        Printer.ColorPrint("The stench of the Amarok is too overwhelming, you died. Your journey ends here. ");
    }
}
