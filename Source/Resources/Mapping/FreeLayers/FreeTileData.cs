using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping.FreeLayers;

[GlobalClass]
public partial class FreeTileData : Resource
{
	[Export] public int Id { get; set; }
	[Export] public Vector2 Position { get; set; }
}
