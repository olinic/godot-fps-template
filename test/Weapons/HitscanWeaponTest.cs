using Godot;
using GdUnit4;
using static GdUnit4.Assertions;

namespace FPS.Weapons;
[TestSuite]
public class HitscanWeaponTest
{
    private HitscanWeapon weapon;

    [Before]
    public void Before()
    {
        weapon = new()
        {
            WeaponMesh = new Node3D(),
            RayCast = new RayCast3D(),
            AmmoCapacity = 20
        };
        weapon._Ready();
    }

    [TestCase]
    public void GivenFullCapacity_GetAmmoCount_ReturnsCount()
    {
        AssertInt(weapon.GetCurrentAmmo()).IsEqual(weapon.AmmoCapacity);
    }

    [TestCase]
    public void GivenShot_GetAmmoCount_ReturnsReducedCount()
    {
        weapon.Shoot(1);
        AssertInt(weapon.GetCurrentAmmo()).IsEqual(weapon.AmmoCapacity - 1);
    }
}