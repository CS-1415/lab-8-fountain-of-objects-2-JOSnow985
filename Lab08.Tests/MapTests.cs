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