using Godot;
using System;

namespace drumstalotajs;

public partial class Main : Node
{
	private Node CurrentScene;
	
	public override void _Ready()
	{
		StartScene();
	}
	
	public void StartScene()
	{
		LoadScene("Start");
	}
	
	private void LoadScene(string name)
	{
		String path = $"res://Scenes/{name}/{name}.tscn";
	}
}
