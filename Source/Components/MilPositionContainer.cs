using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources.Mapping.Sets;
using Drumstalotajs.Resources.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Components;

public partial class MilPositionContainer : BaseButton
{
	[Export] private Label _posText;
	[Export] private Map _map;
	private bool _precise = false;
	private Vector2 _oldPos;
	
	public override void _Ready()
	{
		Toggled += (bool on) => {
			_precise = on;
			UpdateCoords(_oldPos);
		};
	}
	
	public void UpdateCoords(Vector2 coords)
	{
		if (_map != null)
		{
			if (_precise)
			{
				var pos = coords.Snapped(new Vector2(0.01f, 0.01f));
				var str = _map.FormatMilCoords(coords);
				_oldPos = pos;
				_posText.Text = str;
			} else
			{
				var pos = coords;
				var str = _map.FormatMilCoords((Vector2I)pos);
				_oldPos = pos;
				_posText.Text = str;
			}
		}
	}
	
	public void UpdateCoords()
	{
		if (_map != null)
		{
			if (_precise)
			{
				var coords = _map.ViewportToMilCoords();
				var decimals = _map.ViewportMouseToLocal() / 32f;
				decimals.X = (float)Math.Round(Calculations.GetDecimal(decimals.X), 2);
				decimals.Y = (float)Math.Round(Mathf.Abs(Calculations.GetDecimal(decimals.Y)), 2);
				var newCoords = (coords + decimals).Snapped(new Vector2(0.01f, 0.01f));
				var str = _map.FormatMilCoords(newCoords);
				_oldPos = newCoords;
				_posText.Text = str;
			} else
			{
				var coords = _map.ViewportToMilCoords();
				var str = _map.FormatMilCoords((Vector2I)coords);
				_oldPos = coords;
				_posText.Text = str;
			}
		}
	}
}
