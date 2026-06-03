using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Markers;

public partial class MarkerLayer : Node2D
{
	private List<(Vector2 A, Vector2 B)> _rulers = new();
	private List<Vector2> _points = new();
	
	public override void _Draw()
	{
		foreach(var point in _points)
		{
			DrawCircle(point, 7.5f, Colors.Black);
			DrawCircle(point, 5f, Colors.White);
		}
	}
	
	public void PutPoint(Vector2 position)
	{
		_points.Add(position);
		QueueRedraw();
	}
	
	public void WipePoints()
	{
		_points.Clear();
		QueueRedraw();
	}
	
	public void WipeRulers()
	{
		_rulers.Clear();
		QueueRedraw();
	}
	
	public void Wipe()
	{
		WipeRulers();
		WipePoints();
	}
}
