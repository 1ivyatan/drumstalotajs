using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class StoredLayer : Resource
{
	[Export] public TileMapPattern Pattern { get; set; }
}
