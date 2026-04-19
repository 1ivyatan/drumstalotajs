using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Layers;

public partial class SceneLayer : Layer<string, SceneTile, SceneLayerData>
{
	[Signal] public delegate void TileSpawnedEventHandler(SceneTile entity);
	[Signal] public delegate void TileExitingEventHandler(SceneTile entity);
	
	[Export] private SceneTileAtlasData[] Atlas { get; set; }
	public List<SceneTile> Instances { get; private set; }
	
	public override void _Ready()
	{
		PrepareAtlas(Atlas);
		Instances = new List<SceneTile>();
		ChildEnteredTree += TileSpawnedAction;
		ChildExitingTree += TileExitingAction;
	}
	
	private void PrepareAtlas(SceneTileAtlasData[] atlas)
	{
		TileSetSource source = TileSet.GetSource(0);
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
			Instances.Add(sceneTile);
			EmitSignal(SignalName.TileSpawned, sceneTile);
		}
	}
	
	private void TileExitingAction(Node node)
	{
		if (node is SceneTile sceneTile)
		{
			Instances.Remove(sceneTile);
			EmitSignal(SignalName.TileExiting, sceneTile);
		}
	}
	
	public override string[] GetAtlas()
	{
		return Atlas.Select(a => a.Name).ToArray();
	}
	
	public SceneTileAtlasData[] GetFullAtlas()
	{
		return Atlas;
	}
	
	public override void AddTile(Vector2I position, string atlas)
	{
		int id = Atlas.FirstOrDefault(a => a.Name == atlas).Id;
		SetCell(position, 0, new Vector2I(0, 0), id);
		EmitSignal(SignalName.ChangedLayer);
	}
	
	public override void RemoveTile(Vector2I position)
	{
		EraseCell(position);
		EmitSignal(SignalName.ChangedLayer);
	}
	
	public override Godot.Collections.Array<SceneTile> Flash(Vector2I position)
	{
		return null;
	}
	
	public override SceneLayerData Export()
	{
		return null;
	}
	
	public override void Load(SceneLayerData layerData)
	{
		
	}
}
