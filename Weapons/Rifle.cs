using Godot;

namespace FPS.Weapons;
public partial class Rifle: HitscanWeapon
{
    public Rifle()
    {
        FireRate = 144;
        Recoil = 0.3f;
        AmmoCapacity = 6;
        Damage = 334;
        FireMode = FireMode.Semi;
        WeaponType = WeaponType.Secondary;
    }
}
