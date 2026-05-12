using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Mapping.Overlays;

public partial class DeviceMarker : OverlayTile
{
	[Export] private Sprite2D _arrow;

	//public override Dictionary Data { get; 
//		set {
//			field = value;
//		}
//	} = new();
/*
	[Export] private float _speed = 1.0f;
	private float _t = 0f;
	
	public override void _PhysicsProcess(double delta)
	{
		_t += (float)delta * _speed;
		float weight = Mathf.PingPong(_t, 1f);
		Modulate = _c1.Lerp(_c2, weight);
	}*/
	
	[Export] private float _speed = 10.0f;
	private float _t = 0f;
	private Vector2 _ogArrowPos;
	
	public override void _Ready()
	{
		_ogArrowPos = _arrow.Offset;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		_t += (float)delta * _speed;
		float weight = Mathf.PingPong(_t, 10f);
		var newPos = _ogArrowPos;
		newPos.Y = newPos.Y - weight;
		_arrow.Offset = newPos;
	}
	
	public void SetArrowRotation(double degrees)
	{
		_arrow.RotationDegrees = (float)degrees;
	}
}
