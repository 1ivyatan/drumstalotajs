using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Editor.Components;

public partial class EntityProps : Props
{
	[Export] private Map _map;
	[Export] private SpinBox _azimuthSpinner;
	[Export] private SpinBox _integritySpinner;
	[Export] private CheckBox _playerCheck;
	private Entity _entity = null;
	
	public override void _Ready()
	{
		_azimuthSpinner.ValueChanged += (double value) => {
			if (_entity != null)
			{
				_entity.Azimuth = (float)value;
			}
		};
		
		_integritySpinner.ValueChanged += (double value) => {
			if (_entity != null)
			{
				_entity.Integrity = (float)value;
			}
		};
		
		_playerCheck.Toggled += (bool on) => {
			if (_entity != null)
			{
				_entity.Player = on;
			}
		};
	}
	
	public override void Load(Tile tile)
	{
		if (tile is Entity entity)
		{
			_entity = entity;
			_azimuthSpinner.Value = entity.Azimuth;
			_integritySpinner.Value = entity.Integrity;
			_playerCheck.ButtonPressed = entity.Player;
			Visible = true;
		} else {
			Close();
			_entity = null;
		}
	}
}
