using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping.FreeLayers;

[GlobalClass]
public partial class FreeTile : Resource
{
	[Export] public int Id { get; set; }
}
