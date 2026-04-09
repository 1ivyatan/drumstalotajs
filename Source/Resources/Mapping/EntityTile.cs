using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping;

public partial class EntityTile : SceneTile
{
	[Export] public EntityType Type { get; private set; }
}
