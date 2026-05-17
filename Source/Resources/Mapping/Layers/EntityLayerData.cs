using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class EntityLayerData : SceneLayerData
{
	public EntityLayerData () {}
	public EntityLayerData (EntityLayer layer) : base(layer)
	{
		Tiles.Clear();
		foreach (Entity instance in layer.Instances)
		{
			if (instance is Device deviceInstance)
			{
				EntityLayerDeviceTileData data = new EntityLayerDeviceTileData(layer, deviceInstance);
				Tiles.Add(data);
			} else
			{
				EntityLayerTileData data = new EntityLayerTileData(layer, instance);
				Tiles.Add(data);
			}
			
		}
		
	}
}
