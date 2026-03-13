using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Entities;

public partial class Entity : Node2D
{
	public void Initialize(Vector2 position)
	{
		Position = position;
	}
}
