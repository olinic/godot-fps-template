using Godot;

namespace FPS.Weapons;
public partial class SMG: HitscanWeapon
{
    public SMG()
    {
        FireRate = 14;
        _Recoil = 0.05f;
        AmmoCapacity = 20;
        _Damage = 100;
        FireMode = FireMode.Auto;
        WeaponType = WeaponType.Primary;
    }
}
