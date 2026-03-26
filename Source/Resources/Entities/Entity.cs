using Godot;
using System;

namespace drumstalotajs.Resources.Entities;

[GlobalClass]
public partial class Entity : Resource
{
	[Export] public drumstalotajs.Entities.EntityType EntityType { get; set; }
	public int Id { get; set; }
}
