using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Markers;

public partial class MarkerLayer : Node2D
{
	[Export] private Texture2D _pointTexture;
	[Export] private Sprite2D _brush;
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
			Vector2 perpendicular = new Vector2(-direction.Y, direction.X);
			for (float i = 32f; i <= distance; i += 32f)
			{
				var pos = ruler.A + direction * i;
				DrawLine(
					pos - perpendicular * 7.5f, 
					pos + perpendicular * 7.5f,
					Colors.Black,
					7.5f
				);
				DrawLine(
					pos - perpendicular * 5f, 
					pos + perpendicular * 5f,
					Colors.White,
					5f
				);
			}
		}
		
		foreach(var point in _points)
		{
			var offset = _pointTexture.GetSize() / 2;
			DrawTexture(_pointTexture, point - offset);
		}
		
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (_brush.Visible && @event is InputEventMouseMotion mouseMotionEvent)
		{
			_brush.Position = GetLocalMousePosition();
		}
	}
	
	public void ActivateBrush()
	{
		_brush.Texture = _pointTexture;
		_brush.Visible = true;
	}
	
	public void HideBrush()
	{
		_brush.Visible = false;
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
