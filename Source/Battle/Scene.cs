using Godot;
using System;

namespace Drumstalotajs.Battle
{	
	public partial class Scene : Node
	{
		public void LoadLevel(string name)
		{
			string levelResourcePath = $"res://Resources/Levels/{name}.tres";
			Drumstalotajs.Resources.Level levelResource = ResourceLoader.Load<Drumstalotajs.Resources.Level>(levelResourcePath);
			GD.Print(levelResource.Title);
		}
	}
}
