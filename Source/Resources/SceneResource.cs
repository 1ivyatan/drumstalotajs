using Godot;
using System;

namespace Drumstalotajs.Resources
{
	[GlobalClass]
	public partial class SceneResource : Resource
	{
		[Export]
		public string Path { get; set; }
		
		public SceneResource() : this("") {}
		
		public SceneResource(string path)
		{
			this.Path = path ?? "";
		}
	}
}
