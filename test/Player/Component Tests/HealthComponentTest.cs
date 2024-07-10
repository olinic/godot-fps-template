using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using System.Threading.Tasks;
using static FPS.Test.SignalTester;

namespace FPS.Characters.Health;
[TestSuite]
public class HealthComponentTest
{
    [TestCase]
    public void GivenAttack_ApplyDamage_ReducesHealth()
    {
        HealthComponent health = new HealthComponent();
        health._Ready();
        Attack attack = new Attack();
        attack.Damage = 300;
        health.ApplyDamage(attack);
        AssertInt(health.Health.Value).IsEqual(700);
    }
    [TestCase]
    public async Task GivenHealthReachesZero_ApplyDamage_Deletes()
    {
        HealthComponent health = new HealthComponent();
        health._Ready();
        Attack attack = new Attack();
        attack.Damage = 1000;
        await AssertSignalEmitted("health_depleted", () => health.ApplyDamage(attack)).On(health);
        AssertInt(health.Health.Value).IsEqual(0);
    }
}