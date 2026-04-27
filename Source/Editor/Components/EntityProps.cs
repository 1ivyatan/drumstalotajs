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
	private Entity _entity = null;
	
	public override void _Ready()
	{
	}
	
	public override void Load(Tile tile)
	{
		if (tile is Entity entity)
		{
			_entity = entity;
			Visible = true;
		} else {
			Close();
			_entity = null;
		}
	}
}
