namespace Lab08;

public class Room   // Basic room is "Empty", used as a basis for other rooms
{
    protected string _feedback = "";
    public virtual string Feedback => _feedback;
    public Room(){}
    public Room(string description)
    {
        _feedback = description;
    }
}

public class GateRoom : Room        // Entrance and Exit room
{
    public GateRoom() : base("You can see the sunlit entrance to the cavern, a roiling wall of darkness covers the only passage further. ") {}
}

public class FountainRoom : Room
{
    private bool _fountainEnabled = false;
    public bool IsFountainEnabled => _fountainEnabled;
    public override string Feedback => _fountainEnabled ? "You hear the rushing waters from the Fountain of Objects. It has been reactivated! " : _feedback;
    public FountainRoom() : base("You can hear water dripping in this room. The Fountain of Objects is here! ") {}
    public void ToggleFountain() => _fountainEnabled = !_fountainEnabled;
}
