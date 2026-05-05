using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Battle;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Battle.Components;

public partial class InitDeviceAdjustmentContainer : Container
{
	private Map _map;
	
	[Export] private Label _deviceLabel;
	[Export] private Label _devicePosition;
	[Export] private TextureRect _deviceTexture;
	
	[Export] private CircleSlider _azimuthSlider;
	[Export] private Label _azimuthLabel;
	[Export] private Wheel _angleSlider;
	[Export] private Label _angleLabel;
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
		/*
		_angleSlider.ValueChanged += (double value) => {
			_angleLabel.Text = $"~{Math.Round(value)}°";
		};*/
	}

	public void Load(Device device)
	{
		var atlas = _map.EntityLayer.GetAtlasData(device.TileId);

		_deviceTexture.Texture = atlas.Thumbnail;
		_deviceLabel.Text = atlas.Name;
		_devicePosition.Text = $"{_map.EntityLayer.LocalToMap(device.Position)}";
		
		_device = device;
		
		Visible = true;
	}
	
	public void Close()
	{
		Visible = false;
		_device = null;
	}
}
