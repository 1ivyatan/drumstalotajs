using Godot;
using System;

namespace Drumstalotajs.Resources
{
	[GlobalClass]
	public partial class Scene : Resource
	{
		[Export] public string Path { get; set; }
	}
}
