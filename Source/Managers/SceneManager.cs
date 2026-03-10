using Godot;
using System;

namespace drumstalotajs.Managers;

public partial class SceneManager : Node
{
	private Node CurrentScene;
	
	public void StartScene()
	{
		LoadScene("Start");
	}
	
	private void LoadScene(string name)
	{
		String path = $"res://Scenes/{name}/{name}.tscn";
	}
}
