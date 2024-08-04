using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using System.Threading.Tasks;
using static FPS.Test.SignalTester;
using System.Threading;

namespace FPS.Characters.Health;
[TestSuite]
public class HealthComponentTest
{
    private HealthComponent health;
    private float _max = 1000;

    [BeforeTest]
    public void Before()
    {
        health = new HealthComponent();
        health._Ready();
        health.MaxHealth = _max;
        health.RegenTimer.WaitTime = 0.01;
        health.RegenDurationSeconds = 0.01f;
    }

    [TestCase]
    public void GivenAttack_ApplyDamage_ReducesHealth()
    {
        Attack attack = new() { Damage = 300 };
        health.ApplyDamage(attack);
        AssertFloat(health.Health.Value).IsEqual(700);
    }

    [TestCase]
    public async Task GivenHealthReachesZero_ApplyDamage_Deletes()
    {
        Attack attack = new() { Damage = 1000 };
        await AssertSignalEmitted("health_depleted", () => health.ApplyDamage(attack)).On(health);
        AssertFloat(health.Health.Value).IsEqual(0);
    }

    [TestCase]
    public void GivenPartialRegen_Health_ReturnsGreaterHealth()
    {
        Attack attack = new() { Damage = 999 };
        health.ApplyDamage(attack);
        AssertFloat(health.Health.Value).IsEqual(1);
        health.RegenTimer.Stop();
        health._PhysicsProcess(health.RegenDurationSeconds/2);
        AssertFloat(health.Health.Value).IsEqual(501);
    }

    [TestCase]
    public void GivenPartialRegenWithDifferentDuration_Health_ReturnsGreaterHealth()
    {
        health.RegenDurationSeconds = 5;
        Attack attack = new() { Damage = 999 };
        health.ApplyDamage(attack);
        AssertFloat(health.Health.Value).IsEqual(1);
        health.RegenTimer.Stop();
        health._PhysicsProcess(health.RegenDurationSeconds/2);
        AssertFloat(health.Health.Value).IsEqual(501);
    }

    [TestCase]
    public void GivenFullRegen_Health_ReturnsGreaterHealth()
    {
        Attack attack = new() { Damage = 300 };
        health.ApplyDamage(attack);
        AssertFloat(health.Health.Value).IsEqual(700);
        health.RegenTimer.Stop();
        health._PhysicsProcess(health.RegenDurationSeconds);
        AssertFloat(health.Health.Value).IsEqual(1000);
    }
}