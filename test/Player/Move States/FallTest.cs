using Godot;
using GdUnit4;
using static GdUnit4.Assertions;


[TestSuite]
public class FallTest{
    private player Player;
    private Fall Fall;

    [Before]
    public void Before(){
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
        //(sprint_state.get_initial_velocity_change()).is_equal(Vector3.ZERO)
        IMoveState FallState = (IMoveState) Fall;
        AssertVector(FallState.GetInitialVelocityChange())
            .Equals(Vector3.Zero);
    }
}