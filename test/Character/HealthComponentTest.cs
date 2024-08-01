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
    public void GivenRegen_Health_ReturnsGreaterHealth()
    {
        Attack attack = new() { Damage = 300 };
        health.ApplyDamage(attack);
        AssertFloat(health.Health.Value).IsEqual(700);
        health.RegenTimer.Stop();
        health._PhysicsProcess(3);
        AssertFloat(health.Health.Value).IsEqual(1000);
    }
}