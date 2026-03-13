using Godot;
using System;

namespace drumstalotajs.Resources;

[GlobalClass]
public partial class EntitySet : Resource
{
	[Export] public int TileSize { get; set; }
	[Export] public PackedScene[] Scenes { get; set; }
}
