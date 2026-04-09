using Godot;
using System;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class SceneLayer : TileSet
{
	[Export] public SceneTile[] SceneTiles { get; private set; }
	
	public void PrepareAtlas()
	{
		TileSetSource source = GetSource(0);
		if (source is TileSetScenesCollectionSource sceneSource)
		{
			foreach (SceneTile sceneTile in SceneTiles)
			{
				if (!sceneSource.HasSceneTileId(sceneTile.Id))
				{
					sceneSource.CreateSceneTile(sceneTile.Scene, sceneTile.Id);
				}
			}
		}
	}
}
/*
namespace Drumstalotajs.Mapping.Layers;

public partial class SceneLayer : Layer
{
	[Export] public Resources.Mapping.SceneTile[] SceneTiles { get; private set; }*/
