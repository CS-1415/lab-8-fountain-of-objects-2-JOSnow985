namespace Lab08.Tests;


public class MonsterTests
{
    Wizzrobe wizzrobe = Wizzrobe.At(0,1);
    Soldier soldier = Soldier.At(0,2);
    Drgn drgn = Drgn.At(0,3);
    Rodent rodent = Rodent.At(0,4);
    Map map = Map.Small;
    Player player;

    [SetUp]
    public void Setup()
    {
        player = new(map);
    }

    [Test]
    public void MonsterInventoryTest()
    {
        Assert.Multiple(() =>
        {   // Pretty unlikely that Drgn wouldn't roll any items in it's inventory, 10 rolls of 25%
            Assert.That(drgn.Inventory, Has.Count.GreaterThan(0));
            Assert.That(drgn.Inventory[1] as Upgrade, Is.Not.Null);
        });
    }

    [Test]
    public void WizzrobeTeleportTest()
    {   // Player is not at entrance
        (player.X, player.Y) = (2, 2);

        wizzrobe.SpecialAttack(player);

        // Should be at entrance after
        Assert.That((player.X, player.Y), Is.EqualTo((0, 0)));
    }
}