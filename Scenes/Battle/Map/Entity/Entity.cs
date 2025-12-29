using Godot;
using System;

public partial class Entity : Node2D
{
	public enum EntityType
	{
		None = -1,
		DeviceMarker = 0,
		Device = 1
	}
}
