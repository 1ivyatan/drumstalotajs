using Godot;
using System;
using Drumstalotajs.Mapping.Tiles.Entities;

namespace Drumstalotajs.Mapping.Layers;

public partial class EntityLayer : SceneLayer, ISceneLayer<EntityTile>
{
	public ISceneLayer<EntityTile> AsISceneLayer => (ISceneLayer<EntityTile>)this;

}
