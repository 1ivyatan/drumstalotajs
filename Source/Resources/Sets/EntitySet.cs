using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Sets;

[GlobalClass]
public partial class EntitySet : Resource
{
	[Export] public int TileSize { get; set; }
	[Export] public Dictionary<int, PackedScene> Scenes { get; set; }
	//[Export] public PackedScene[] Scenes { get; set; }
}
