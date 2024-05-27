using Godot;
using GdUnit4;
using static GdUnit4.Assertions;

[TestSuite]
public class WalkTest
{
    private player player;
    private Walk walk;

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
        IMoveState walkState = (IMoveState) walk;
        AssertVector(walkState.GetInitialVelocityChange())
                .Equals(new Vector3(0,0,0));
    }

    [TestCase]
    public void GivenInputInOneDirection_GetVelocity_ReturnsVelocityWithWalkSpeed()
    {
        AssertVector(walk.GetVelocity(1.0f, new Vector2(1, 0), player.Transform.Basis))
                .IsEqual(new Vector3(walk.GetSpeed(), 0, 0));
        AssertVector(walk.GetVelocity(1.0f, new Vector2(0, 1), player.Transform.Basis))
                .IsEqual(new Vector3(0, 0, walk.GetSpeed()));
    }

    [TestCase]
    public void GivenInputTwoDirections_GetVelocity_ReturnsNormalizedVelocity()
    {
        AssertVector(walk.GetVelocity(1.0f, new Vector2(1, 0), player.Transform.Basis))
                .IsLess(new Vector3(walk.GetSpeed(), 0, walk.GetSpeed()));
    }

    [TestCase]
    public void GivenMovementInput_GetNextState_ReturnsEmpty()
    {
        AssertBool(walk.GetNextState(new Vector2(0, 0), true).IsPresent()).IsFalse();
    }

    [TestCase]
    public void GivenJumpInput_GetNextState_ReturnsJump()
    {
        Input.ActionPress("jump");
        AssertObject(walk.GetNextState(Vector2.Up, true).GetValue()).IsInstanceOf<Jump>();
        Input.ActionRelease("jump");
    }

    [TestCase]
    public void GivenNotOnGround_GetNextState_ReturnsFall()
    {
        AssertObject(walk.GetNextState(Vector2.Up, false).GetValue()).IsInstanceOf<Fall>();
    }
}