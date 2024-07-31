using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using System.Threading.Tasks;
using static FPS.Test.SignalTester;

namespace FPS.Characters.Health;
[TestSuite]
public class HealthComponentTest
{
    private HealthComponent health;
    [Before]
    public void Before()
    {
        health = new HealthComponent();
        health._Ready();
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
}