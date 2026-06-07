using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Mapping.Overlays;

public partial class LevelMarker : OverlayTile
{
	[Export] private Texture2D[] _buildings;
	[Export] private Sprite2D _arrow;
	[Export] private Sprite2D _building;
	[Export] private Flag _flag;
	
	private float _t = 0f;
	private Vector2 _ogArrowPos;

	public override Dictionary Data { get; 
		set {
			field = value;
			_building.Texture = _buildings[(int)field["Type"]];
			_flag.SetFlag((bool)field["Unlocked"]);
		}
	} = new();
	
	public override void _PhysicsProcess(double delta)
	{
		if (_arrow.Visible)
		{
			_t += (float)delta * 10f;
			float weight = Mathf.PingPong(_t, 10f);
			var newPos = _ogArrowPos;
			newPos.Y = newPos.Y - weight;
			_arrow.Offset = newPos ;
		}
	}
	
	public void MarkSelected()
	{
		if ((bool)Data["Unlocked"])
		{
			_arrow.Modulate = Colors.Blue;
		} else
		{
			_arrow.Modulate = Colors.Red;
		}
		_arrow.Visible = true;
	}
	
	public void MarkDeselected()
	{
		_arrow.Visible = false;
		_arrow.Modulate = Colors.White;
	}
}
