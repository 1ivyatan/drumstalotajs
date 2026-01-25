using Godot;
using System;

namespace Drumstalotajs.Managers
{
	public partial class SceneManager : Node
	{
		private Node CurrentScene { get; set; }
		
		public void LoadScene(string name)
		{
			if (CurrentScene != null)
			{
				CurrentScene.QueueFree();
  				RemoveChild(CurrentScene);
			}
			
			string sceneResourcePath = $"res://Resources/Scenes/{name}.tres";
			Resources.SceneResource sceneResource = ResourceLoader.Load<Resources.SceneResource>(sceneResourcePath);
			Node newScene = ResourceLoader.Load<PackedScene>(sceneResource.Path).Instantiate();
			
			CurrentScene = newScene;
			AddChild(CurrentScene);
		}
	}
}
