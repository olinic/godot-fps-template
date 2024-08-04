using Godot;

namespace FPS.Weapons;
public partial class SMG: HitscanWeapon
{
    public SMG()
    {
        RoundsPerSecond = 14;
        Recoil = 0.05f;
        AmmoCapacity = 20;
        Damage = 100;
        FireMode = FireMode.Auto;
        WeaponType = WeaponType.Primary;
    }
}
