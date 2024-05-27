using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using System;

[TestSuite]
public class Jumptest{
    private player Player;
    private Jump Jump;

    

    [Before]
    public void Before(){
        Player = new player();
    }

    [BeforeTest]
    public void Setup()
    {
        Jump = new Jump(Player);
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
        IMoveState JumpState = (IMoveState) Jump;
        AssertVector(JumpState.GetInitialVelocityChange())
            .Equals(new Vector3(0, (float) Math.Sqrt(Jump.JumpHeight/Jump.Gravity * 2.0f) *  Jump.Gravity,0));
    }

    [TestCase]
    public void GetVelocity()
    {
        Jump.GetVelocity(1.0f, new Vector2(0.5f, 0.5f), new Basis());
        /* AssertFloat().Equals(1);
        AssertFloat().IsLess(0);
        AssertFloat().Equals(1); */
    }
    
}