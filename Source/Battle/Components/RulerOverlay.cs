using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Battle.Components;

public partial class RulerOverlay : Control
{
	[Export] private Map _map;
	[Export] private Button _deselect;
	[Export] private Button _wipe;
	[Export] private Button _removeNewest;
	
	private bool _adding = false;
	private Vector2 _pointA;
	private Vector2 _pointB;
	
	public override void _Ready()
	{
		_deselect.Pressed += () => {
			DeselectPoint();
		};
		_wipe.Pressed += () => {
			_map.MarkerLayer.Wipe();
			InspectRulerCounts();
			_deselect.Disabled = true;
		};
		_removeNewest.Pressed += () => {
			var ruler = _map.MarkerLayer.PopRuler();
			_map.MarkerLayer.RemovePoint(ruler.A);
			_map.MarkerLayer.RemovePoint(ruler.B);
			InspectRulerCounts();
			_deselect.Disabled = true;
		};
	}
	
	public override void _Input(InputEvent @event)
	{
		if (!Visible) return;
		
		if (@event is InputEventMouse mouseEvent)
		{
			bool moving = false;
			
			if (mouseEvent is InputEventMouseMotion mouseMotion)
			{
				moving = true;
			}
			
			if (mouseEvent is InputEventMouseButton mouseButton && 
				mouseButton.Pressed && !moving
			)
			{
				bool rClick = mouseButton.ButtonIndex == MouseButton.Right;
				
				if (rClick)
				{
					if (_map.ViewportMouseOnMap())
					{
						var pos = _map.ViewportMouseToLocal();
						if (!_adding)
						{
							_adding = true;
							_pointA = pos;
							_deselect.Disabled = false;
							_map.MarkerLayer.PutPoint(_pointA);
						} else
						{
							_pointB = pos;
							_map.MarkerLayer.PutPoint(_pointB);
							_map.MarkerLayer.PutRuler(_pointA, _pointB);
							InspectRulerCounts();
							_adding = false;
							_deselect.Disabled = true;
						}
					} else
					{
						DeselectPoint();
					}
				}
			}
		}
	}
	
	private void DeselectPoint()
	{
		_adding = false;
		_deselect.Disabled = true;
		_map.MarkerLayer.RemovePoint(_pointA);
	}
	
	private void InspectRulerCounts()
	{
		var zero = _map.MarkerLayer.GetRulerCount() == 0;
		_wipe.Disabled = zero;
		_removeNewest.Disabled = zero;
	}
}
