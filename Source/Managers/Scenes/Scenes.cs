using Godot;
using System;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Battle;

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
		BattleScene scene = await LoadScene("Battle") as BattleScene;
		await scene.Open(mapPath);
		await SetScene(scene);
	}
	
	public async void Battle(LevelSet levelSet, LevelProps level)
	{
		BattleScene scene = await LoadScene("Battle") as BattleScene;
		await scene.Open(levelSet, level);
		await SetScene(scene);
	}
}
