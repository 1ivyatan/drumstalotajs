using Godot;
using System;

namespace Drumstalotajs.Managers
{
	public partial class SceneManager : Node
	{
		private Node CurrentScene { get; set; }
		
		public void Start()
		{
			LoadScene("Start");
			ShowScene();
		}
		
		public void Battle(string levelName)
		{
			LoadScene("Battle");
			Battle.Scene battleScene = CurrentScene as Battle.Scene;
			battleScene.LoadLevel("1");
			ShowScene();
		}
			
		private void LoadScene(string name)
		{
			if (CurrentScene != null)
			{
				CurrentScene.QueueFree();
  				RemoveChild(CurrentScene);
			}
			
			string sceneResourcePath = $"res://Resources/Scenes/{name}.tres";
			Drumstalotajs.Resources.Scene sceneResource = ResourceLoader.Load<Drumstalotajs.Resources.Scene>(sceneResourcePath);
			CurrentScene = ResourceLoader.Load<PackedScene>(sceneResource.Path).Instantiate();
		}
		
		private void ShowScene()
		{
			AddChild(CurrentScene);
		}
	}
}
