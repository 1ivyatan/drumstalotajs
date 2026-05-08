using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
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
	new protected List<EntityLayerTileData> _newTileQueue = new();
	
	public Array<int> GetEntityIdsByType(EntityType entityType)
	{
		var ids = GetFullAtlas()
		.Where(a => {
			if (a is EntityLayerAtlasData ea)
			{
				return ea.Type == entityType;
			} else return false;
		})
		.Select(e => e.Id).ToArray();
		return new Array<int>(ids);
	}
	
	new public Array<Entity> Flash(Vector2I position)
	{
		var arr = base.Flash(position);
		return new Array<Entity>(arr.Select(t => t as Entity));
	}
	
	new public Entity GetInstance(Vector2I position)
	{
		var tile = base.GetInstance(position);
		return tile != null ? tile as Entity : null;
	}
	
	public int InstanceCount(int id, bool player)
	{
		return Instances
			.Where(i => i.TileId == id)
			.Where(i => i is Entity)
			.Where(e => ((Entity)e).Player == player)
			.Count();
	}
	
	protected override void TileSpawnedAction(Node node)
	{
		if (node is Entity entity)
		{
			var pos = LocalToMap(entity.Position);
			var atlas = _newTileQueue.FirstOrDefault(t => t.Position == pos);
			
			if (atlas != null && atlas is EntityLayerTileData entityAtlas)
			{
				entity.Azimuth = entityAtlas.Azimuth;
				entity.Integrity = entityAtlas.Integrity;
				entity.Player = entityAtlas.Player;
				entity.TileId = entityAtlas.Id;
				entity.Data = entityAtlas.Data;
				entity.Height = entityAtlas.Height;
				
				var entityLayerAtlas = (EntityLayerAtlasData)GetAtlasData(entityAtlas.Id);
				if (entityLayerAtlas != null)
				{
					entity.Properties = entityLayerAtlas.Properties;
				}
				
				if (entity is Device device && entityAtlas is EntityLayerDeviceTileData deviceAtlas)
				{
					device.Angle = deviceAtlas.Angle;
					device.Properties = (DevicePropertiesData)entityLayerAtlas.Properties;
				}

				if (!Instances.Contains(entity)) Instances.Add(entity);
				_newTileQueue.Remove(atlas);
				EmitSignal(SignalName.TileSpawned, entity);
				EmitSignal(SignalName.ChangedLayer);
			}
		}
	}
	
	public override void AddTile(Vector2I position, string atlas)
	{
		EntityLayerTileData data = new EntityLayerTileData();
		int id = Atlas.FirstOrDefault(a => a.Name == atlas).Id;
		data.Position = position;
		data.Id = id;
		AddTile(data);
	}
	
	public void AddTile(EntityLayerTileData atlas)
	{
		_newTileQueue.Add(atlas);
		SetCell(atlas.Position, 0, Vector2I.Zero, atlas.Id);
	}
	
	new public EntityLayerData Export()
	{
		return new EntityLayerData(this);
	}
	
	public override void Load(SceneLayerData layerData)
	{
		if (layerData is EntityLayerData entityLayerData)
		{
			foreach (var tile in entityLayerData.Tiles)
			{
				if (tile != null)
				{
					AddTile(tile);
				}
			}
		}
	}
}
