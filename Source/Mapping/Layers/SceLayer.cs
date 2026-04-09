using Godot;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class SceLayer<[MustBeVariant] TSceneTile> //, [MustBeVariant]TProps 
	: Layer, 
	ILayer<Resources.Mapping.SceneTileProps>,
	ISceneLayer<TSceneTile> where TSceneTile : SceneTile
{
	[Signal] public delegate void SpawnedTileEventHandler(Variant tile);
	[Signal] public delegate void DestroyedTileEventHandler(Variant tile);
	
	public ISceneLayer<TSceneTile> AsISceneLayer => (ISceneLayer<TSceneTile>)this;
	
	public List<TSceneTile> Instances { get; private set; }
	private Resources.Mapping.SceneLayer _sceneLayerSet;
	
	public override void _Ready()
	{
		_sceneLayerSet = TileSet as Resources.Mapping.SceneLayer;
		_sceneLayerSet.PrepareAtlas();
		Instances = new List<TSceneTile>();
		ChildEnteredTree += (Node node) => {
			if (node is TSceneTile tile)
			{
				Instances.Add(tile);
				EmitSignal(SignalName.SpawnedTile, tile);
			}
		};
		ChildExitingTree += (Node node) => {
			if (node is TSceneTile tile)
			{
				Instances.Remove(tile);
				EmitSignal(SignalName.DestroyedTile, tile);
			}
		};
	}

	public async Task<SceneTile> AddTile(Vector2I position, string name)
	{
		int id = _sceneLayerSet.SceneTiles.FirstOrDefault(s => s.Name == name).Id;
		SetCell(position, 0, new Vector2I(0, 0), id);
		var data = await ToSignal(this, SignalName.SpawnedTile);
		return (SceneTile)data[0];
	}
	
	new public Resources.Mapping.SceneTileProps[] GetAtlas()
	{
		return _sceneLayerSet.SceneTiles;
	}
	
	public SceneTile GetTile(Vector2 position)
	{
		return Instances.FirstOrDefault(i => position == LocalToMap(position));
	}
}
