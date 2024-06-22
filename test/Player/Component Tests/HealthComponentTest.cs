using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using System.Threading.Tasks;
using static TestUtils.SignalTester;

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
        AssertInt(health.Health).IsEqual(700);
    }
    [TestCase]
    public async Task GivenHealthReachesZero_ApplyDamage_Deletes()
    {
        HealthComponent health = new HealthComponent();
        health._Ready();
        Attack attack = new Attack();
        attack.Damage = 1000;
        await AssertSignalEmitted("health_depleted", () => health.ApplyDamage(attack)).On(health);
        AssertInt(health.Health).IsEqual(0);
    }
}