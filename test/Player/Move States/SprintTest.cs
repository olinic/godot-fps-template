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

    /*[TestCase(Vector2.Left, TestName ="Left")]
    [TestCase(Vector2.Right, TestName = "Right")]
    public void GivenSidewaysMovement_IsMovingForward_ReturnsFalse(Vector2 dir){
        AssertBool(Sprint.IsMovingForward(dir)).IsFalse();
    }*/


    /*func test_sideways_is_moving_forward_false(sideways: Vector2, 
		test_parameters := [[Vector2.LEFT], [Vector2.RIGHT]]) -> void:
	assert_bool(Sprint.is_moving_forward(sideways)).is_false()

    func test_forward_is_moving_forward_true(forward: Vector2, 
		test_parameters := [
			[Vector2.UP], 
			[Vector2.from_angle(-PI / 4)], 
			[Vector2.from_angle(-PI * 3 / 4)]]) -> void:
	assert_bool(Sprint.is_moving_forward(forward)).is_true()

    func test_backward_is_moving_forward_false(backward: Vector2, 
		test_parameters := [
			[Vector2.DOWN], 
			[Vector2.from_angle(PI / 4)], 
			[Vector2.from_angle(PI * 3 / 4)]]) -> void:
	assert_bool(Sprint.is_moving_forward(backward)).is_false()*/
}