using Godot;
using System;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Managers.Scenes;

public partial class SceneManager : Node
{
	public async void Start()
	{
		Node scene = await LoadScene("Start");
		await SetScene(scene);
	}
	
	public async void LevelSelection()
	{
		Node scene = await LoadScene("LevelSelection");
		await SetScene(scene);
	}
	
	public async void Editor()
	{
		Node scene = await LoadScene("Editor");
		await SetScene(scene);
	}
	
	public async void Battle(string mapPath)
	{
		Node scene = await LoadScene("Battle");
		await SetScene(scene);
	}
	
	public async void Battle(LevelSet levelSet, LevelProps level)
	{
		Node scene = await LoadScene("Battle");
		await SetScene(scene);
	}
}
