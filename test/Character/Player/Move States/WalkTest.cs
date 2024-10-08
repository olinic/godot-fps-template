using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using FPS.Test;

namespace FPS.Characters.Player.Movestates;
[TestSuite]
public class WalkTest
{
    private Player Player;
    private Walk Walk;

    [Before]
    public void Before()
    {
        Player = new Player();
    }

    [BeforeTest]
    public void Setup()
    {
        Walk = new Walk(Player);
    }

    [AfterTest]
    public void Teardown()
    {
        InputWrapper.Clear();
    }

    [TestCase]
    public void GetInitialVelocityChange()
    {
        IMoveState WalkState = (IMoveState) Walk;
        AssertVector(WalkState.GetInitialVelocity(new Vector3(1, 1, 1)))
                .Equals(new Vector3(1, 1, 1));
    }

    [TestCase]
    public void GivenInputInOneDirection_GetVelocity_ReturnsVelocityWithWalkSpeed()
    {
        AssertVector(Walk.GetVelocity(1.0f, new Vector2(1, 0), Player.Transform.Basis))
                .IsEqual(new Vector3(Walk.GetSpeed(), 0, 0));
        AssertVector(Walk.GetVelocity(1.0f, new Vector2(0, 1), Player.Transform.Basis))
                .IsEqual(new Vector3(0, 0, Walk.GetSpeed()));
    }

    [TestCase]
    public void GivenInputTwoDirections_GetVelocity_ReturnsNormalizedVelocity()
    {
        AssertVector(Walk.GetVelocity(1.0f, new Vector2(1, 0), Player.Transform.Basis))
                .IsLess(new Vector3(Walk.GetSpeed(), 0, Walk.GetSpeed()));
    }

    [TestCase]
    public void GivenMovementInput_GetNextState_ReturnsEmpty()
    {
        AssertBool(Walk.GetNextState(new Vector2(0, 0), true).IsPresent()).IsFalse();
    }

    [TestCase]
    public void GivenJumpInput_GetNextState_ReturnsJump()
    {
        InputWrapper.ActionPress("jump");
        AssertObject(Walk.GetNextState(Vector2.Up, true).GetValue()).IsInstanceOf<Jump>();
    }

    [TestCase]
    public void GivenNotOnGround_GetNextState_ReturnsFall()
    {
        AssertObject(Walk.GetNextState(Vector2.Up, false).GetValue()).IsInstanceOf<Fall>();
    }

    [TestCase]
    public void GivenControllerSprintInputAndNotMovingForward_GetNextState_ReturnsEmpty()
    {
        InputWrapper.ActionPress("controller_sprint");
        AssertBool(Walk.GetNextState(Vector2.Zero, true).IsPresent()).IsFalse();
    }

    [TestCase]
    public void GivenSprintInputAndMovingForward_GetNextState_ReturnsSprint()
    {
        InputWrapper.ActionPress("sprint");
        AssertObject(Walk.GetNextState(Vector2.Up, true).GetValue()).IsInstanceOf<Sprint>();
    }

    [TestCase]
    public void GivenSprintInputAndNotMovingForward_GetNextState_ReturnsEmpty()
    {
        InputWrapper.ActionPress("sprint");
        AssertBool(Walk.GetNextState(Vector2.Zero, true).IsPresent()).IsFalse();
    }
}