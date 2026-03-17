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
        Assert.That(player.Weapon.Info, Is.EqualTo(("Dull Sword", "An old, dull sword. Better than nothing")));

        for (int i = 0; i < 5; i++)
        {
            var old = player.Weapon.Info;
            player.Inventory.Add(Upgrade.Weapon);
            player.UpdateGear();
            Assert.That(old, Is.Not.EqualTo(player.Weapon.Info));
        }
    }

    [Test]
    public void ArmorDescriptionTest()
    {
        Assert.That(player.Armor.Info, Is.EqualTo(("Saint's Locket", "A locket given to you by your friend, Saint Jiub, a note inside reads \"Not even last night's storm could wake you\".")));

        for (int i = 0; i < 5; i++)
        {
            var old = player.Armor.Info;
            player.Inventory.Add(Upgrade.Armor);
            player.UpdateGear();
            Assert.That(old, Is.Not.EqualTo(player.Armor.Info));
        }
    }
}