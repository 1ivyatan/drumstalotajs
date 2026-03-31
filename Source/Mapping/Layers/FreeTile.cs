using Godot;
using System;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Layers;

public partial class FreeTile : Area2D
{
	[Export] public Resources.Mapping.FreeLayers.FreeTile Resource { get; private set; }
	
	public void Initialize(int id, Vector2 position)
	{
		Resource.Id = id;
		Position = position;
	}
}
