using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using System.Threading.Tasks;
using System.Threading;


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
        await AssertSignal(health).IsNotEmitted("health_depleted").WithTimeout(100);
        health._Ready();
        Attack attack = new Attack();
        attack.Damage = 1000;
        health.ApplyDamage(attack);
        await AssertSignal(health).IsEmitted("health_depleted").WithTimeout(100);
        AssertInt(health.Health).IsEqual(0);
    }
}