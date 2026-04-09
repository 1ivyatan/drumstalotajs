using Godot;
using System;
using Drumstalotajs.Resources.Mapping;

public partial class EntityLayer : SceneLayer
{
	[Export] new public EntityTile[] SceneTiles { get; private set; }
}
