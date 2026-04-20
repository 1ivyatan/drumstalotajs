using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Mapping.Layers;

public partial class EntityLayer : SceneLayer
{
	new public Array<Entity> Flash(Vector2I position)
	{
		var arr = base.Flash(position);
		return new Array<Entity>(arr.Select(t => t as Entity));
	}
	
	new public EntityLayerData Export()
	{
		return new EntityLayerData(this);
	}
	
	public override void Load(SceneLayerData layerData)
	{
		if (layerData is EntityLayerData entityLayerData)
		{
			
		} else return;
	}
}
