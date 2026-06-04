using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Markers;

public partial class MarkerLayer : Node2D
{
	[Export] private Texture2D _pointTexture;
	private List<(Vector2 A, Vector2 B)> _rulers = new();
	private List<Vector2> _points = new();
	
	public override void _Draw()
	{
		foreach(var ruler in _rulers)
		{
			/* line */
			DrawLine(ruler.A, ruler.B, Colors.Black, 7.5f);
			DrawLine(ruler.A, ruler.B, Colors.White, 3.5f);
			
			/* points */
			var direction = (ruler.B - ruler.A).Normalized();
			float distance = ruler.A.DistanceTo(ruler.B);
			int count = (int)(distance / 32f) + 1;
			for (int i = 0; i < count; i++)
			{
				var pos = ruler.A + direction * i * 32f;
				DrawCircle(pos, 5f, Colors.Red);
			}
		}
		
		foreach(var point in _points)
		{
			var offset = _pointTexture.GetSize() / 2;
			DrawTexture(_pointTexture, point - offset);
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
	
	public (Vector2 A, Vector2 B) PopRuler()
	{
		var ruler = _rulers[_rulers.Count - 1];
		_rulers.RemoveAt(_rulers.Count - 1);
		QueueRedraw();
		return ruler;
	}
	
	public void PopPoint()
	{
		_points.RemoveAt(_points.Count - 1);
		QueueRedraw();
	}
	
	public void RemovePoint(Vector2 pos)
	{
		_points.Remove(pos);
		QueueRedraw();
	}
	
	public int GetRulerCount()
	{
		return _rulers.Count;
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
