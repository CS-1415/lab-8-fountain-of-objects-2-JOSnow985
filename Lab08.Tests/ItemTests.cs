namespace Lab08.Tests;

public class ItemTests
{
    Map map = Map.Small;
    Player player;

    [SetUp]
    public void Setup()
    {
        player = new(map);
    }

    [Test]
    public void WeaponDescriptionTest()
    {
        var one = player.Weapon.Info;
        player.Inventory.Add(Upgrade.Weapon);
        _ = player.GearStats;       // Currently only updates values when Attack and Defense are retrieved...

        var two = player.Weapon.Info;
        player.Inventory.Add(Upgrade.Weapon);
        _ = player.GearStats;

        var three = player.Weapon.Info;
        player.Inventory.Add(Upgrade.Weapon);
        _ = player.GearStats;

        var four = player.Weapon.Info;
        player.Inventory.Add(Upgrade.Weapon);
        _ = player.GearStats;

        var five = player.Weapon.Info;
        player.Inventory.Add(Upgrade.Weapon);
        _ = player.GearStats;

        var six = player.Weapon.Info;
        player.Inventory.Add(Upgrade.Weapon);
        _ = player.GearStats;

        var seven = player.Weapon.Info;
        player.Inventory.Add(Upgrade.Weapon);
        _ = player.GearStats;

        Assert.Multiple(() =>
        {
            Assert.That(one, Is.EqualTo(("Dull Sword", "An old, dull sword. Better than nothing")));
            Assert.That(two, Is.Not.EqualTo(one));      // Info should change for every level
            Assert.That(three, Is.Not.EqualTo(two));
            Assert.That(four, Is.Not.EqualTo(three));
            Assert.That(five, Is.Not.EqualTo(four));
            Assert.That(six, Is.Not.EqualTo(five));
            Assert.That(seven, Is.EqualTo(six));        // Above level 6 is the same!
        });
    }

    [Test]
    public void ArmorDescriptionTest()
    {
        var one = player.Armor.Info;
        player.Inventory.Add(Upgrade.Armor);
        _ = player.GearStats;

        var two = player.Armor.Info;
        player.Inventory.Add(Upgrade.Armor);
        _ = player.GearStats;

        var three = player.Armor.Info;
        player.Inventory.Add(Upgrade.Armor);
        _ = player.GearStats;

        var four = player.Armor.Info;
        player.Inventory.Add(Upgrade.Armor);
        _ = player.GearStats;

        var five = player.Armor.Info;
        player.Inventory.Add(Upgrade.Armor);
        _ = player.GearStats;

        var six = player.Armor.Info;
        player.Inventory.Add(Upgrade.Armor);
        _ = player.GearStats;

        var seven = player.Armor.Info;
        player.Inventory.Add(Upgrade.Armor);
        _ = player.GearStats;

        Assert.Multiple(() =>
        {
            Assert.That(one, Is.EqualTo(("Saint's Locket", "A locket given to you by your friend, Saint Jiub, a note inside reads \"Not even last night's storm could wake you\".")));
            Assert.That(two, Is.Not.EqualTo(one));
            Assert.That(three, Is.Not.EqualTo(two));
            Assert.That(four, Is.Not.EqualTo(three));
            Assert.That(five, Is.Not.EqualTo(four));
            Assert.That(six, Is.Not.EqualTo(five));
            Assert.That(seven, Is.EqualTo(six));
        });
    }
}