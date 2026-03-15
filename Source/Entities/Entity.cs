using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Entities;

public partial class Entity : Node2D
{
	[Export] public Resources.Entities.Entity EntityResource { get; private set; }
	
	public void Initialize(Vector2 position, int id)
	{
		Position = position;
		EntityResource.Id = id;
	}
}
