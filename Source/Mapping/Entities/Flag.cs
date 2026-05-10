using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Flag : Node2D
{
	[Export] private Sprite2D _pole;
	[Export] private Sprite2D _vexillium;
	[Export] private Vector2 _vexilliumEnabled = new Vector2(0, -15);
	[Export] private Vector2 _vexilliumDisabled = Vector2.Zero;
	
	[Export] private Texture2D _enabledFlag;
	[Export] private Texture2D _disabledFlag;
	
	public void SetFlag(bool player, bool disabled = false)
	{
		if (player) _vexillium.Modulate = new Color(0.0f, 0.081f, 1.0f, 1.0f);
		else _vexillium.Modulate = new Color(1.0f, 0.088f, 0.0f, 1.0f);
		if (disabled)
		{
			_vexillium.Position = _vexilliumDisabled;
			_vexillium.Texture = _disabledFlag;
		} else
		{
			_vexillium.Position = _vexilliumEnabled;
			_vexillium.Texture = _enabledFlag;
		}
	}
}
