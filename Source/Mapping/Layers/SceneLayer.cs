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

namespace Drumstalotajs.Mapping.Layers;

public partial class SceneLayer : Layer<string, SceneTile, SceneLayerData>
{
	[Signal] public delegate void TileSpawnedEventHandler(SceneTile entity);
	[Signal] public delegate void TileExitingEventHandler(SceneTile entity);
	
	[Export] private SceneLayerAtlasData[] Atlas { get; set; }
	public List<SceneTile> Instances { get; private set; }
	
	public override void _Ready()
	{
		PrepareAtlas(Atlas);
		Instances = new List<SceneTile>();
		ChildEnteredTree += TileSpawnedAction;
		ChildExitingTree += TileExitingAction;
	}
	
	public SceneTile GetInstance(Vector2I position)
	{
		var list = Instances.Where(i => LocalToMap(i.Position) == position);
		return list.Count() > 0 ? list.ToArray()[0] : null;
	}
	
	private void PrepareAtlas(SceneLayerAtlasData[] atlas)
	{
		TileSetSource source;
		
		if (TileSet == null || TileSet.GetSourceCount() == 0)
		{
			var tileset = TileSet ?? new TileSet();
			tileset.TileSize = Constants.Mapping.TileSize;
			var newSceneSource = new TileSetScenesCollectionSource();
			tileset.AddSource(newSceneSource, 0);
			TileSet = tileset;
			source = newSceneSource;
		}
		else
		{
			source = TileSet.GetSource(0);
		}
		
		if (source is TileSetScenesCollectionSource sceneSource)
		{
			foreach (var tile in atlas)
			{
				if (!sceneSource.HasSceneTileId(tile.Id))
				{
					sceneSource.CreateSceneTile(tile.Scene, tile.Id);
				}
			}
		}
	}
	
	private void TileSpawnedAction(Node node)
	{
		if (node is SceneTile sceneTile)
		{
			if (!Instances.Contains(sceneTile)) Instances.Add(sceneTile);
			EmitSignal(SignalName.TileSpawned, sceneTile);
		}
	}
	
	private void TileExitingAction(Node node)
	{
		if (node is SceneTile sceneTile)
		{
			if (Instances.Contains(sceneTile)) Instances.Remove(sceneTile);
			EmitSignal(SignalName.TileExiting, sceneTile);
		}
	}
	
	public override string[] GetAtlas()
	{
		return Atlas.Select(a => a.Name).ToArray();
	}
	
	public SceneLayerAtlasData GetAtlasData(string name)
	{
		return Atlas.FirstOrDefault(a => a.Name == name);
	}
	
	public SceneLayerAtlasData[] GetFullAtlas()
	{
		return Atlas;
	}
	
	public async override Task AddTile(Vector2I position, string atlas)
	{
		int id = Atlas.FirstOrDefault(a => a.Name == atlas).Id;
		SetCell(position, 0, Vector2I.Zero, id);
		var nodes = await ToSignal(this, SignalName.TileSpawned);
		if (nodes.Length > 0 && nodes[0].VariantType == Variant.Type.Object)
		{
			var tile = (SceneTile)nodes[0];
			tile.TileId = id;
			tile.Data = new Godot.Collections.Dictionary();
		}
		EmitSignal(SignalName.ChangedLayer);
	}
	
	public async Task AddTile(SceneLayerTileData atlas)
	{
		SetCell(atlas.Position, 0, Vector2I.Zero, atlas.Id);
		var nodes = await ToSignal(this, SignalName.TileSpawned);
		if (nodes.Length > 0 && nodes[0].VariantType == Variant.Type.Object)
		{
			var tile = (SceneTile)nodes[0];
			tile.TileId = atlas.Id;
			tile.Data = atlas.Data;
		}
		EmitSignal(SignalName.ChangedLayer);
	}
	
	public override void RemoveTile(Vector2I position)
	{
		EraseCell(position);
		EmitSignal(SignalName.ChangedLayer);
	}
	
	public override SceneLayerData Export()
	{
		return new SceneLayerData(this);
	}
	
	public async override Task Load(SceneLayerData layerData)
	{
		foreach (var tile in layerData.Tiles)
		{
			AddTile(tile);
		}
	}
	
	public override Godot.Collections.Array<SceneTile> Flash(Vector2I position)
	{
		Array<SceneTile> tiles = new Array<SceneTile>();
		var spaceState = GetWorld2D().DirectSpaceState;
		PhysicsPointQueryParameters2D query = new PhysicsPointQueryParameters2D();
		query.Position = GlobalPosition + MapToLocal(position);
		query.CollideWithAreas = true;
		var intersectedNodes = spaceState.IntersectPoint(query, 9);
		if (intersectedNodes.Count > 0)
		{
			foreach (var node in intersectedNodes)
			{
				Node2D collider = (Node2D)node["collider"];
				if (collider is SceneTile tile && tile.GetParent() == this)
				{
					tiles.Add(tile);
				}
			}
			
			if (tiles.Count > 0)
			{
				tiles.OrderBy(tile => {
					return ((Vector2)position).DistanceTo(tile.Position);
				});
			}
		}
		return tiles;
	}
}
