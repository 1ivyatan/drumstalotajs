using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Battle;
using Drumstalotajs.Mapping;
using Drumstalotajs.Resources.Mapping.Sets;

namespace Drumstalotajs.Battle.Stages;

public partial class DevicePlacement : Control
{
	[Export] private ItemList _deviceInventory;
	private BattleScene _scene;
	private Map _map;
	
	private DeviceProps _deviceProps;
	
	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_scene.BattleTopnav.Title = "Device placement";
		//_map.EntityLayer.InstanceCount();
		foreach (var device in _map.CurrentLoadedMap.DeviceProps)
		{
			GD.Print(device);
		}
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		//GD.Print(3333);
	}
}
