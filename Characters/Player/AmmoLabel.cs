using Godot;
using System;

public partial class AmmoLabel : Label
{
	public void OnWeaponHandlerAmmoUpdate(int ammoCount)
	{
		Text = ammoCount.ToString();
	}
}
