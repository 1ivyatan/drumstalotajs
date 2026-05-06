using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Layers;

public partial class EntityLayer : SceneLayer
{
	public EntityType GetType(int id)
	{
		var atlas = GetAtlasData(id);
		return ((EntityLayerAtlasData)atlas).Type;
	}
	
	public Device[] GetPlayerDevices()
	{
		var instances = Instances
		.Where(e => e is Device)
		.Where(e => ((Entity)e).Player == true)
		.Select(e => e as Device)
		.ToArray();
		return instances;
	}
	/*var devs = _map.EntityLayer.Instances
			.Where(e => e is Device)//_map.EntityLayer.GetType(e.TileId) == EntityType.Device)
			.Where(e => ((Entity)e).Player == true);
			
			foreach (Device dev in devs)
			{
				dev.Placed = true;
			}
			*/
}
