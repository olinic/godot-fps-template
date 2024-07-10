using Godot.Bridge;

namespace FPS.Weapons;
public interface IGun
{
    public void Shoot(double delta);
    public int GetCurrentAmmo();
    public int GetAmmoCapacity();

    public void Reload();
}