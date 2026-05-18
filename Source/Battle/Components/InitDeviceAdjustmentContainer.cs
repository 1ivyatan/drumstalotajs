using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Battle;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Battle.Components;

public partial class InitDeviceAdjustmentContainer : Container
{
	private Map _map;
	[Export] private Label _title;
	
	[Export] private CircleSlider _azimuthSlider;
	[Export] private Label _azimuthLabel;
	[Export] private Wheel _angleSlider;
	[Export] private Label _angleLabel;
	
	[Export] private VSlider _shellSlider;
	[Export] private Label _shellLabel;
	
	private DevicePropertiesData _props = null;
	private EntityLayerAtlasData _atlas = null;
	private Device _device = null;
	
	public override void _Ready()
	{
		BattleScene _scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_azimuthSlider.SetValue(0);

		_azimuthSlider.ValueChanged += (double value) => {
			if (_device != null)
			{
				_azimuthLabel.Text = $"~{Math.Round(value)}°";
				_device.Azimuth = value;
			}
		};
		
		_angleSlider.ValueChanged += (double value) => {
			if (_device != null)
			{
				_angleLabel.Text = $"~{Math.Round(value)}°";
				_device.Angle = value;
			}
		};
		
		_shellSlider.ValueChanged += (double value) => {
			if (_device != null)
			{
				_shellLabel.Text = $"{value}";
				_device.ShellsPerTurn = (int)value;
			}
		};
	}

	public void Load(Device device)
	{
		_atlas = (EntityLayerAtlasData)_map.EntityLayer.GetAtlasData(device.TileId);
		_device = device;
		_props = (DevicePropertiesData)device.Properties;
		
		_angleSlider.MinValue = _props.MinAngle;
		_angleSlider.MaxValue = _props.MaxAngle;
		
		if (_device.Azimuth == -1)
		{
			_azimuthSlider.SetValue(0);
		} else
		{
			_azimuthSlider.SetValue(_device.Azimuth);
		}
		
		if (_device.Angle == -1)
		{
			_angleSlider.Value = _props.MinAngle + ((_props.MaxAngle - _props.MinAngle) / 2);
		} else
		{
			_angleSlider.Value = _device.Angle;
		}
		
		_shellSlider.MaxValue = _props.MaxFiringPerTurn;
		_shellSlider.Value = _device.ShellsPerTurn;
		_shellLabel.Text = $"{_device.ShellsPerTurn}";
		
		var resupplyStr = device.ResupplyTurns > 0 ? $"({device.ResupplyTurns} turns until resupply)" : $"({device.Shells} shells)";
		_title.Text = $"{_atlas.Name} {resupplyStr}";
		
		Visible = true;
	}
	
	public void Close()
	{
		Visible = false;
		_device = null;
		_atlas = null;
		_props = null;
	}
}
