using Godot;
using System;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Levels;

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
	
	public void Battle(string mapPath)
	{
		Node scene = LoadScene("Battle");
		SetScene(scene);
	}
	
	public void Battle(LevelSet levelSet, LevelProps level)
	{
		Node scene = LoadScene("Battle");
		SetScene(scene);
	}
}
