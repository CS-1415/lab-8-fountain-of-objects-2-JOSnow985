namespace Lab08.Tests;

public class PlayerTests
{
    Map map = Map.Small;
    Player player;
    [SetUp]
    public void Setup()
    {
        player = new(map);
    }

    [Test]
    public void PlayerInventoryTest() // Player object should be created with a weapon and armor in inventory
    {
        Assert.That(player.Inventory, Has.Count.EqualTo(2));
    }

    [Test]
    public void CheckGearTest() // CheckGear() is what Player uses to return the GearStats tuple
    {
        (int atk, int def) = player.GearStats;
        
        Assert.Multiple(() =>
        {
            Assert.That(atk, Is.EqualTo(1));
            Assert.That(def, Is.EqualTo(1));
        });
    }
    [Test]
    public void UpdateGearTest()
    {
        (int atk, int def) = player.GearStats;
        player.Inventory.Add(Upgrade.Weapon);
        player.Inventory.Add(Upgrade.Armor);

        (int new_atk, int new_def) = player.GearStats;  // Calling for the stats again should update their values according to upgrades held
        Assert.Multiple(() =>
        {
            Assert.That(new_atk, Is.GreaterThan(atk));
            Assert.That(new_def, Is.GreaterThan(def));
        });
    }
}