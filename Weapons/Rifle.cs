using Godot;

namespace FPS.Weapons;
public partial class Rifle: HitscanWeapon
{
    public Rifle()
    {
        FireRate = 144;
        _Recoil = 0.3f;
        AmmoCapacity = 6;
        _Damage = 334;
        FireMode = FireMode.Semi;
        WeaponType = WeaponType.Secondary;
    }
}
