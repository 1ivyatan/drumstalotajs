using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Markers;

public partial class MarkerLayer : Node2D
{
	private List<(Vector2 A, Vector2 B)> _rulers = new();
	
	public override void _Ready()
	{
		
	}
	
	public void Wipe()
	{
		_rulers.Clear();
	}
}
