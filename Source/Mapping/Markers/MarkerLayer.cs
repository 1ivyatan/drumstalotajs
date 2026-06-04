using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Markers;

public partial class MarkerLayer : Node2D
{
	[Export] private Map _map;
	[Export] private Texture2D _pointTexture;
	[Export] private Sprite2D _brush;

	private List<(Vector2 A, Vector2 B)> _rulers = new();
	private List<Vector2> _points = new();
	private Font _font = ThemeDB.FallbackFont;
	
	public override void _Draw()
	{
		var offset = _pointTexture.GetSize() / 2;
			
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
			DrawTexture(_pointTexture, point - offset);
		}
		
		foreach(var ruler in _rulers)
		{
			var oldACellPos = _map.LocalPosToMilCoords(ruler.A);
			var oldBCellPos = _map.LocalPosToMilCoords(ruler.B);
			var aPosDecimals = ruler.A / 32f;
			var bPosDecimals = ruler.B / 32f;
			aPosDecimals.X = (float)Math.Round(Calculations.GetDecimal(aPosDecimals.X), 3);
			aPosDecimals.Y = (float)Math.Round(Mathf.Abs(Calculations.GetDecimal(aPosDecimals.Y)), 3);
			bPosDecimals.X = (float)Math.Round(Calculations.GetDecimal(bPosDecimals.X), 3);
			bPosDecimals.Y = (float)Math.Round(Mathf.Abs(Calculations.GetDecimal(bPosDecimals.Y)), 3);
			
			var newACellPos = oldACellPos + aPosDecimals;
			var newBCellPos = oldBCellPos + bPosDecimals;
			
			DrawPointLabel(ruler.A, $"{newACellPos}", 0);
			DrawPointLabel(ruler.B, $"{newBCellPos}", 0);
		}
	}
	
	private void DrawPointLabel(Vector2 pos, string text, double spacing)
	{
		var offset = _pointTexture.GetSize() / 2;
		DrawStringOutline(_font, 
			pos + new Vector2(offset.X, 8f + (float)spacing), 
			text, HorizontalAlignment.Center, -1,
			12, 8, Colors.Black
		);
		DrawString(_font,
			pos + new Vector2(offset.X, 8f + (float)spacing), 
			text, HorizontalAlignment.Center, -1,
			12, Colors.White
		);
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
