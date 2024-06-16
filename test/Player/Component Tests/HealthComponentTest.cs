using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using System.Threading.Tasks;
using System.Threading;


[TestSuite]
public class HealthComponentTest
{
    private bool HealthDepleted = false;
    private void OnHealthDepleted()
    {
        HealthDepleted = true;
    }
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
        AssertSignal(health).IsNotEmitted("health_depleted");
        health._Ready();
        health.health_depleted += OnHealthDepleted;
        Attack attack = new Attack();
        attack.Damage = 1000;
        health.ApplyDamage(attack);
        AssertInt(health.Health).IsEqual(0);
        await AssertSignal(health).IsEmitted("health_depleted").WithTimeout(200);
        AssertBool(HealthDepleted).IsTrue();
    }
}