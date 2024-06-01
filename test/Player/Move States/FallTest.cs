using Godot;
using GdUnit4;
using static GdUnit4.Assertions;


[TestSuite]
public class FallTest
{
    private player Player;
    private Fall Fall;

    [Before]
    public void Before()
    {
        Player = new player();
    }

    [BeforeTest]
    public void Setup()
    {
        Fall = new Fall(Player);
    }
    [AfterTest]
    public void Teardown()
    {
        InputWrapper.Clear();
    }

      [TestCase]
    public void GetInitialVelocityChange()
    {
        IMoveState FallState = (IMoveState) Fall;
        AssertVector(FallState.GetInitialVelocity(new Vector3(1, 1, 1)))
            .Equals(new Vector3(1, 1, 1));
    }
}