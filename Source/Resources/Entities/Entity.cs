using Godot;
using System;

namespace drumstalotajs.Resources.Entities;

[GlobalClass]
public partial class Entity : Resource
{
	[Export] public int Id { get; set; }
}
