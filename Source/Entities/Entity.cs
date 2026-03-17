using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Entities;

public partial class Entity : Area2D
{
	[Export] public Resources.Entities.Entity EntityResource { get; private set; }
	
	public int Id { get; set; }
	
	public double Azimuth { 
		get; 
		set
		{
			field = ((value % 360) + 360) % 360;
			Rotation = (float)Utilities.Physics.ToRadians(field);
		}
	}
	
	public void Initialize(Vector2 position, int id)
	{
		Position = position;
		Id = id;
	}
	
	public void Initialize(Vector2 position, double azimuth, int id)
	{
		Position = position;
		Azimuth = azimuth;
		Id = id;
	}
}
