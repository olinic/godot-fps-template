using Godot;
using System;

namespace FPS.Weapons;
public partial class AmmoLabel : Label
{
	public void OnWeaponHandlerAmmoUpdate(int AmmoCapacity, int CurrentAmmo)
	{
		Text = CurrentAmmo.ToString() + "/" + AmmoCapacity.ToString();
	}
}
