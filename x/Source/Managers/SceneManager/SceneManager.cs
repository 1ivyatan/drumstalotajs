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
		
		public void Levels()
		{
			LoadScene("Levels");
			ShowScene();
		}
		
		public void Battle(Resources.Levels.Level level)
		{
			LoadScene("Battle");
			Battle.Scene battleScene = CurrentScene as Battle.Scene;
			battleScene.AssignLevel(level);
			ShowScene();
		}
		
		public void Battle(string levelName)
		{
			LoadScene("Battle");
			Battle.Scene battleScene = CurrentScene as Battle.Scene;
			battleScene.AssignLevel(levelName);
			ShowScene();
		}
			
		private void LoadScene(string name)
		{
			string sceneResourcePath = $"res://Resources/Scenes/{name}.tres";
			
			if (CurrentScene != null)
			{
				CurrentScene.QueueFree();
  				RemoveChild(CurrentScene);
			}
			
			if (ResourceLoader.Exists(sceneResourcePath))
			{
				Drumstalotajs.Resources.Scene sceneResource = ResourceLoader.Load<Drumstalotajs.Resources.Scene>(sceneResourcePath);
				CurrentScene = ResourceLoader.Load<PackedScene>(sceneResource.Path).Instantiate();
			}
		}
		
		private void ShowScene()
		{
			AddChild(CurrentScene);
		}
	}
}
