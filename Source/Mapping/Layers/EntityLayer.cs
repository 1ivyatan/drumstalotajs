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
	
	public async override Task AddTile(Vector2I position, string atlas)
	{
		EntityLayerTileData data = new EntityLayerTileData();
		int id = Atlas.FirstOrDefault(a => a.Name == atlas).Id;
		data.Position = position;
		data.Id = id;
		await AddTile(data);
	}
	
	public async Task AddTile(EntityLayerTileData atlas)
	{
		_newTileQueue.Add(atlas);
		SetCell(atlas.Position, 0, Vector2I.Zero, atlas.Id);
	}
	
	new public EntityLayerData Export()
	{
		return new EntityLayerData(this);
	}
	
	public async override Task Load(SceneLayerData layerData)
	{
		if (layerData is EntityLayerData entityLayerData)
		{
			foreach (var tile in entityLayerData.Tiles)
			{
				if (tile != null)
				{
					await AddTile(tile);
				}
			}
		}
			//_layerData = entityLayerData;
			//foreach (var tile in entityLayerData.Tiles)
			//{
			//	if (tile != null)
			//	{
				//	await this.AddTile((EntityLayerTileData)tile);
					//SetCell(tile.Position, 0, Vector2I.Zero, tile.Id);
			//	}
			//}
		//		if (tile != null)
		//		{
		//			var data = new EntityLayerTileData();
		//			data.Id = tile.Id;
		//			data.Position = tile.Position;
			//	GD.Print(tile.GetType().Name);
			//	GD.Print(tile.Id);
			//	GD.Print(tile.Position);
					//await AddTile(data);
			//await EntityLayer.Load(data.EntityLayer);
			//await OverlayLayer.Load(data.OverlayLayer);
					
		//		}
				//await this.AddTile(tile);  SceneLayerTileData . Tiles
				/*
	[Export] public int Id { get; set; }
	[Export] public Vector2I Position { get; set; }*/
		//	}
	}
}
