using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Mapping.Overlays;

public partial class SelectorHighlight : OverlayTile
{
	[Export] private Color _c1;
	[Export] private Color _c2;
	
	[Export] private float _speed = 1.0f;
	private float _t = 0f;
	
	public override void _PhysicsProcess(double delta)
	{
		_t += (float)delta * _speed;
		float weight = Mathf.PingPong(_t, 1f);
		Modulate = _c1.Lerp(_c2, weight);
	}
}
