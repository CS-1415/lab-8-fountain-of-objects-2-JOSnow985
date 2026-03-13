using NUnit.Framework.Constraints;

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
        {   // No Monster should have a null inventory, should at least have a list
            Assert.That(wizzrobe.Inventory, Is.Not.Null);
            Assert.That(soldier.Inventory, Is.Not.Null);
            Assert.That(drgn.Inventory, Is.Not.Null);
            Assert.That(rodent.Inventory, Is.Not.Null);
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

    // --- Probably not worth testing the stats ---

    // [Test]
    // public void WizzrobeStatTest()
    // {
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(wizzrobe.Health, Is.EqualTo(3));
    //         Assert.That(wizzrobe.MonStats.Attack, Is.EqualTo(1));
    //         Assert.That(wizzrobe.MonStats.Defense, Is.EqualTo(0));
    //     });
    // }

    // [Test]
    // public void SoldierStatTest()
    // {
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(soldier.Health, Is.EqualTo(5));
    //         Assert.That(soldier.MonStats.Attack, Is.EqualTo(0));
    //         Assert.That(soldier.MonStats.Defense, Is.EqualTo(0));
    //     });
    // }

    // [Test]
    // public void DrgnStatTest()
    // {
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(drgn.Health, Is.EqualTo(10));
    //         Assert.That(drgn.MonStats.Attack, Is.EqualTo(0));
    //         Assert.That(drgn.MonStats.Defense, Is.EqualTo(0));
    //     });
    // }

    // [Test]
    // public void ROUSStatTest()
    // {
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(rodent.Health, Is.EqualTo(2));
    //         Assert.That(rodent.MonStats.Attack, Is.EqualTo(0));
    //         Assert.That(rodent.MonStats.Defense, Is.EqualTo(0));
    //     });
    // }
}