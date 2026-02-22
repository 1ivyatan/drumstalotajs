using Godot;
using System;

namespace Drumstalotajs.Resources
{
	[GlobalClass]
	public partial class Entity : Resource
	{
		[Export] public string Name { get; set; }
		[Export] public int Id { get; set; }
		[Export] public int Type { get; set; }
		[Export] public Texture2D[] Sprites { get; set; }
	}
}
