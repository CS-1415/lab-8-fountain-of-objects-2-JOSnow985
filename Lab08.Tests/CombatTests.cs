namespace Lab08.Tests;

public class CombatTests
{
    Map map = Map.Small;
    Player player;
    Soldier soldier;

    [SetUp]
    public void Setup()
    {
        player = new(map);
        soldier = Soldier.At(0,2);
    }

    [Test]
    public void AttackTest()
    {
        var originalPlayerHealth = player.Health;
        var originalMonHealth = soldier.Health;

        Combat.Attack(player, soldier);

        Assert.Multiple(() =>
        {
            Assert.That(
                player.Health,
                Is.LessThan(originalPlayerHealth),
                "Player health did not change after Combat.Attack()!"
                );
            Assert.That(
                soldier.Health,
                Is.LessThan(originalMonHealth),
                "Monster health did not change after Combat.Attack()!"
                );
        });
    }

    [Test]
    public void LootTest()
    {
        List<Item> monInventory = soldier.Inventory;
        Combat.Loot(player, soldier);

        Assert.Multiple(() =>
        {
            Assert.That(soldier.Inventory, Has.Count.EqualTo(0) , "Monster Inventory wasn't emptied.");
            Assert.That(player.Inventory, Has.Count.GreaterThan(2), "Player Inventory didn't receive items");    // Player starts with 2 items
        });
    }

    [Test]
    public void RemoveMonsterTest()
    {
        var monster = map.MonsterList[0];
        Combat.RemoveMonsterAt(monster.X, monster.Y, ref map);

        Combat.Loot(player, soldier);

        Assert.Multiple(() =>
        {
            Assert.That(map.MonsterList, Does.Not.Contain(monster), "Monster wasn't removed from MonsterList.");
            Assert.That(map.RoomData[monster.Y][monster.X].IsClear, Is.True, "Room's cleared tag wasn't set to true.");
        });
    }

    [Test]
    public void CombatLoopTest()
    {
        Combat.CombatLoop(player, soldier, ref map);
        if (player.Health <= 0 || soldier.Health <= 0)
            Assert.Pass();
        else
            Assert.Fail("Neither combatants had their health zeroed.");
    }
}