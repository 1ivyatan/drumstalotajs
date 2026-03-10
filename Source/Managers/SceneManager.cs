using Godot;
using System;

namespace drumstalotajs.Managers;

public partial class SceneManager : Node
{
	private Node CurrentScene;
	
	public void StartScene()
	{
		LoadScene("Start");
		ShowScene();
	}
	
	public void LevelsScene()
	{
		LoadScene("Levels");
		ShowScene();
	}
	
	private void LoadScene(string name)
	{
		String path = $"res://Scenes/{name}/{name}.tscn";
		
		if (CurrentScene != null)
		{
			CurrentScene.QueueFree();
  			RemoveChild(CurrentScene);
		}
			
		if (ResourceLoader.Exists(path))
		{
			CurrentScene = ResourceLoader.Load<PackedScene>(path).Instantiate();
		}
	}
		
	private void ShowScene()
	{
		AddChild(CurrentScene);
	}
}
