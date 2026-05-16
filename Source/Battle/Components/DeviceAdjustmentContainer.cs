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
	[Export] private Label _title;

	[Export] private Wheel _traverseSlider;
	[Export] private Label _traverseLabel;

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
		
		_traverseSlider.MinValue = -_props.TraverseRadius;
		_traverseSlider.MaxValue = _props.TraverseRadius;
		_traverseSlider.Value = _device.Traverse;
		
		_angleSlider.MinValue = _props.MinAngle;
		_angleSlider.MaxValue = _props.MaxAngle;
		_angleSlider.Value = _device.Angle;
		
		_shellSlider.MaxValue = _props.MaxFiringPerTurn;
		_shellSlider.Value = _device.ShellsPerTurn;
		_shellLabel.Text = $"{_device.ShellsPerTurn}";
		
		var resupplyStr = device.Shells == 0
			? (
				_map.CurrentLoadedMap.PlayerResupply
					? $"({device.ResupplyTurns} until resupply)"
					: $"(No resupply available)"
			)
			: $"({device.Shells} shells)";
		
		
		_title.Text = $"{_atlas.Name} {_map.EntityLayer.LocalToMap(new Vector2(device.Position.X, Math.Abs(device.Position.Y)))} {resupplyStr}";
		
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
