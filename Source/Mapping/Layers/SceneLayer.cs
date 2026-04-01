using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs;

namespace Drumstalotajs.Mapping.Layers;

public partial class SceneLayer : Layer
{
	public List<SceneTile> Instances { get; private set; }
	private Resources.Layers.SceneLayer sceneLayerSet;
	
	public override void _Ready()
	{
		sceneLayerSet = TileSet as Resources.Layers.SceneLayer;
		sceneLayerSet.PrepareAtlas();
		Instances = new List<SceneTile>();
		ChildEnteredTree += (Node node) => {
			if (node is SceneTile tile)
			{
				Instances.Add(tile);
			}
		};
		ChildExitingTree += (Node node) => {
			if (node is SceneTile tile)
			{
				Instances.Remove(tile);
			}
		};
	}
	
	public SceneTile AddTile(string name, Vector2I position)
	{
		int id = sceneLayerSet.SceneTiles.FirstOrDefault(s => s.Name == name).Id;
		SetCell(position, 0, new Vector2I(0, 0), id);
		return Instances.FirstOrDefault(i => position == LocalToMap(position));
	}
	
	public SceneTile AddTile(int id, Vector2I position)
	{
		SetCell(position, 0, new Vector2I(0, 0), id);
		return Instances.FirstOrDefault(i => position == LocalToMap(position));
	}
}
