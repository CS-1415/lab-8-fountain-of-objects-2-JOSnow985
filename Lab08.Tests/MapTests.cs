namespace Lab08.Tests;


public class MapTests
{
    Map smallMap = Map.Small;
    Map mediumMap = Map.Medium;
    Map largeMap = Map.Large;

    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void PopulateMonsterListTest() => Assert.That(
                                            smallMap.MonsterList,
                                            Has.Count.GreaterThan(0),
                                            $"Monster List Empty!"
                                            );

    [Test]
    public void MonsterSpawnTest()
    {
        int monstersSpawned = 0;
        foreach (var row in smallMap.RoomData)
            foreach (var (_, IsClear, _) in row)
                if (!IsClear)
                    monstersSpawned++;
        Console.WriteLine($"Monsters Found: {monstersSpawned}");
        Assert.That(monstersSpawned, Is.GreaterThan(0), $"Small Map X Test Failed");
    }

    [Test]
    public void SmallMapRoomDataTest()
    {
        Assert.That(
            smallMap.RoomData,
            Has.Count.EqualTo(4),
            $"Small Map Y Test Failed"
            );
        Assert.That(
            smallMap.RoomData[0],
            Has.Count.EqualTo(4),
            $"Small Map X Test Failed"
        );
    }

    [Test]
    public void MediumMapRoomDataTest()
    {
        Assert.That(
            mediumMap.RoomData,
            Has.Count.EqualTo(6),
            $"Medium Map Y Test Failed"
            );
        Assert.That(
            mediumMap.RoomData[0],
            Has.Count.EqualTo(6),
            $"Medium Map X Test Failed"
        );
    }

    [Test]
    public void LargeMapRoomDataTest()
    {
        Assert.That(
            largeMap.RoomData,
            Has.Count.EqualTo(8),
            $"Large Map Y Test Failed"
            );
        Assert.That(
            largeMap.RoomData[0],
            Has.Count.EqualTo(8),
            $"Large Map X Test Failed"
        );
    }

    // [Test]
    // public void Test()
    // {
    //     Assert.Fail("No Map Tests yet!");
    // }
}