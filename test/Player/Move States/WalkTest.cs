using System.Numerics;
using GdUnit4;
using static GdUnit4.Assertions;

[TestSuite]
public class WalkTest
{
    private player player;
    private IMoveState walk;

    [Before]
    public void Before()
    {
        player = new player();
    }

    [BeforeTest]
    public void Setup()
    {
        walk = new Walk(player);
    }

    [TestCase]
    public void GetInitialVelocityChange()
    {
        AssertVector(walk.GetInitialVelocityChange())
                .Equals(new Vector3(0,0,0));
    }    
}