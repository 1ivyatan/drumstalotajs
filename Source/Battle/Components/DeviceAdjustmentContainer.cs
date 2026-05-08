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

public partial class DeviceAdjustmentContainer : Container
{
	private Map _map;
	
	[Export] private Label _deviceLabel;
	[Export] private Label _devicePosition;
	[Export] private TextureRect _deviceTexture;
	[Export] private Label _deviceShellInfo;

	[Export] private Wheel _traverseSlider;
	[Export] private Label _traverseLabel;

	[Export] private Wheel _angleSlider;
	[Export] private Label _angleLabel;
	
	private DevicePropertiesData _props = null;
	private EntityLayerAtlasData _atlas = null;
	private Device _device = null;
	
	public override void _Ready()
	{
		BattleScene _scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
	
		_traverseSlider.ValueChanged += (double value) => {
			if (_device != null)
			{
				_traverseLabel.Text = $"~{Math.Round(value)}°";
				_device.Traverse = value;
			}
		};
		
		_angleSlider.ValueChanged += (double value) => {
			if (_device != null)
			{
				_angleLabel.Text = $"~{Math.Round(value)}°";
				_device.Angle = value;
			}
		};
	}
	
	public void Load(Device device)
	{
		_atlas = (EntityLayerAtlasData)_map.EntityLayer.GetAtlasData(device.TileId);
		_device = device;
		_props = (DevicePropertiesData)device.Properties;
		_deviceTexture.Texture = _atlas.Thumbnail;
		_deviceLabel.Text = _atlas.Name;
		_devicePosition.Text = $"{_map.EntityLayer.LocalToMap(device.Position)}";
		
		if (device.Shells == 0)
		{
			_deviceShellInfo.Text = $"{device.ResupplyTurns} turns until resupply";
		} else
		{
			_deviceShellInfo.Text = $"{device.Shells} shells at disposal";
		}
		
		
		_traverseSlider.MinValue = -_props.TraverseRadius;
		_traverseSlider.MaxValue = _props.TraverseRadius;
		_traverseSlider.Value = _device.Traverse;
		
		_angleSlider.MinValue = _props.MinAngle;
		_angleSlider.MaxValue = _props.MaxAngle;
		_angleSlider.Value = _device.Angle;
		
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
