using Godot;
using GdUnit4;
using static GdUnit4.Assertions;



[TestSuite]
public class SprintTest{
    private player Player;
    private Sprint Sprint;

    [Before]
    public void Before(){
        Player = new player();
    }

    [BeforeTest]
    public void Setup()
    {
        Sprint = new Sprint(Player);
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
        IMoveState SprintState = (IMoveState) Sprint;
        AssertVector(SprintState.GetInitialVelocityChange())
            .Equals(Vector3.Zero);
    }
    [TestCase]
    public void GivenInputInOneDirection_GetVelocity_ReturnsVelocityWithSprintSpeed()
    {
        AssertVector(Sprint.GetVelocity(1.0f, new Vector2(1, 0), Player.Transform.Basis))
                .IsEqual(new Vector3(Sprint.GetSpeed(), 0, 0));
        AssertVector(Sprint.GetVelocity(1.0f, new Vector2(0, 1), Player.Transform.Basis))
                .IsEqual(new Vector3(0, 0, Sprint.GetSpeed()));
    }
    [TestCase]
    public void GivenInputTwoDirections_GetVelocity_ReturnsNormalizedVelocity()
    {
        AssertVector(Sprint.GetVelocity(1.0f, new Vector2(1, 0), Player.Transform.Basis))
                .IsLess(new Vector3(Sprint.GetSpeed(), 0, Sprint.GetSpeed()));
    }
    [TestCase]
    public void GivenJumpInput_GetNextState_ReturnsJump()
    {
        InputWrapper.ActionPress("jump");
        AssertObject(Sprint.GetNextState(Vector2.Up, true).GetValue()).IsInstanceOf<Jump>();
    }
    [TestCase]
    public void GivenNotOnGround_GetNextState_ReturnsFall()
    {
        AssertObject(Sprint.GetNextState(Vector2.Up, false).GetValue()).IsInstanceOf<Fall>();
    }
    [TestCase]
    public void GivenNoMovementInput_IsMovingForward_ReturnsFalse()
    {
        AssertBool(Sprint.IsMovingForward(Vector2.Zero)).IsFalse();
    }

    [TestCase(-1, 0, TestName ="Left")]
    [TestCase(1, 0, TestName = "Right")]
    public void GivenSidewaysMovement_IsMovingForward_ReturnsFalse(int x, int y)
    {
        AssertBool(Sprint.IsMovingForward(new Vector2(x,y))).IsFalse();
    }

    [TestCase(0, -1, TestName = "forward")]
    public void GivenForwardMovement_IsMovingForward_ReturnsTrue(int x, int y)
    {
        AssertBool(Sprint.IsMovingForward(new Vector2(x,y))).IsTrue();
    }

    //Need to check which is left and right.
    [TestCase(-Mathf.Pi / 4, TestName = "Left Angle")]
    [TestCase(-Mathf.Pi*3/4, TestName = "Right Angle")]
    public void GivenForwardMovementAngle_IsMovingForward_ReturnsTrue(float angle)
    {
        AssertBool(Sprint.IsMovingForward(Vector2.FromAngle(angle))).IsTrue();
    }

    [TestCase(0, 1, TestName = "Backward")]
    public void GivenBackwardMovement_IsMovingForward_ReturnsFalse(int x, int y)
    {
        AssertBool(Sprint.IsMovingForward(new Vector2(x,y))).IsFalse();
    }

    //Need to check which is left and right.
    [TestCase(Mathf.Pi /4 ,TestName = "Left Angle Backward")]
    [TestCase(Mathf.Pi *3/4 ,TestName = "Right Angle Backward")]
    public void GivenBackwardMovementAngle_IsMovingForward_ReturnsFalse(float angle){
        AssertBool(Sprint.IsMovingForward(Vector2.FromAngle(angle)));
    }
}