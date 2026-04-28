using Godot;
using System;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Managers.Scenes;

public partial class SceneManager : Node
{
	public void Start()
	{
		Node scene = LoadScene("Start");
		SetScene(scene);
	}
	
	public void LevelSelection()
	{
		Node scene = LoadScene("LevelSelection");
		SetScene(scene);
	}
	
	public void Editor()
	{
		Node scene = LoadScene("Editor");
		SetScene(scene);
	}
}
