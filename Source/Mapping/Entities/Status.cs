using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Status : Sprite2D
{
	[Export] private Texture2D[] _icons;
	
	public void HideIcon() { Visible = false; }
	public void ResupplyIcon() { Texture = _icons[0]; Visible = true; }
	public void DisabledIcon() { Texture = _icons[1]; Visible = true; }
}
