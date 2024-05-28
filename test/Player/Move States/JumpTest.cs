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
        IMoveState JumpState = (IMoveState) Jump;
        AssertVector(JumpState.GetInitialVelocityChange())
            .Equals(new Vector3(0, (float) Math.Sqrt(Jump.JumpHeight/Jump.Gravity * 2.0f) *  Jump.Gravity,0));
    }

    [TestCase]
    public void GetVelocity()
    {
        var _velocity = Jump.GetVelocity(1.0f, new Vector2(0.5f, 0.5f), new Basis());
        AssertFloat(_velocity.X).Equals(1);
        AssertFloat(_velocity.Y).IsLess(0); //falling
        AssertFloat(_velocity.Z).Equals(1); 
    }
    
   [TestCase( 1, TestName= "Walk")]
   [TestCase( 2, TestName= "Sprint")]
    public void GivenLanding_GetNextState_ReturnsWalkOrSprint(int x)
    {
        if(x == 1)
        {
            Jump.SetTargetState(new Walk(Player));
            AssertObject(Jump.GetNextState(Vector2.Up, true).GetValue()).IsInstanceOf<Walk>();        
        }
        else if(x == 2)
        {
            Jump.SetTargetState(new Sprint(Player));
            AssertObject(Jump.GetNextState(Vector2.Up, true).GetValue()).IsInstanceOf<Sprint>();  
        }
    }

    [TestCase]
    public void GivenNoLanding_GetNextState_ReturnsJump()
    {
        Jump.SetTargetState(new Jump(Player));
        AssertObject(Jump.GetNextState(Vector2.Up, true).GetValue()).IsInstanceOf<Jump>();    
    }
}