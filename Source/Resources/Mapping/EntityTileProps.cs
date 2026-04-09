using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class EntityTileProps: SceneTileProps
{
	[Export] public EntityType Type { get; private set; }
}
