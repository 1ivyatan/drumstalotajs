using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Editor.Components;

public partial class GroundProperties : TileProperties
{
	[Export] private Map _map;
	[Export] private Label _heightText;
	[Export] private SpinBox _heightSpinner;
	private GroundTile _groundTile = null;
	
	public override void _Ready()
	{
		_heightSpinner.Connect(SpinBox.SignalName.ValueChanged, new Callable(this, nameof(SetAddedHeight)));;
	}
	
	/*
	public double GetGetAddedHeightAddedHeight(Vector2I position)
	{
		return AddedHeights.ContainsKey(position) ? AddedHeights[position] : 0;
	}
	
	public void SetAddedHeight(Vector2I position, double value)*/
	
	public override void Load(Tile tile)
	{
		if (tile is GroundTile groundTile)
		{
			_groundTile = groundTile;
			_heightSpinner.Value = _map.GroundLayer.GetAddedHeight(groundTile.CellPosition);
			Visible = true;
		} else
		{
			Close();
		}
	}
	
	public override void Close()
	{
		Visible = false;
		_groundTile = null;
	}
	
	private void SetAddedHeight(double value)
	{
		if (IsInstanceValid(_groundTile))
		{
			_map.GroundLayer.SetAddedHeight(_groundTile.CellPosition, value);
		}
	}
}
