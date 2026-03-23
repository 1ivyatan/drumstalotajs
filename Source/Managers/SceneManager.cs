using Godot;
using System;

namespace drumstalotajs.Managers;

public partial class SceneManager : Node
{
	[Signal] public delegate void SwitchedSceneEventHandler(string SceneName);

	private Node CurrentScene { get; set; }
	private string SceneName { get; set; }
	
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
	
	public void EditorScene()
	{
		LoadScene("Editor");
		ShowScene();
	}
	
	public void BattleScene(Resources.Maps.Meta mapMeta)
	{
		LoadScene("Battle");
		(CurrentScene as Battle.BattleScene).LoadMap(mapMeta);
		ShowScene();
	}
	
	private void LoadScene(string name)
	{
		String path = $"res://Scenes/{name}/{name}.tscn";
		SceneName = name;
		
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
		EmitSignal(SignalName.SwitchedScene, SceneName);
		AddChild(CurrentScene);
	}
}
