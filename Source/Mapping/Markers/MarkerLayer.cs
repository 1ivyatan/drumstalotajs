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
		
		foreach(var ruler in _rulers)
		{
			DrawLine(ruler.A, ruler.B, Colors.Cyan, 5.8f);
		}
	}
	
	public void PutPoint(Vector2 position)
	{
		_points.Add(position);
		QueueRedraw();
	}
	
	public void PutRuler(Vector2 positionA, Vector2 positionB)
	{
		_rulers.Add((A: positionA, B: positionB));
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
