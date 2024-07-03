using Godot;
using System;

public partial class AmmoLabel : Label
{
	public void OnWeaponHandlerAmmoUpdate(int AmmoCapacity, int CurrentAmmo)
	{
		Text = CurrentAmmo.ToString() + "/" + AmmoCapacity.ToString();
	}
}
