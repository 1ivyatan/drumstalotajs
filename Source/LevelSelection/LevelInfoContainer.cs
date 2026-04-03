using Godot;
using System;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Progress;
using Drumstalotajs.Utils;

namespace Drumstalotajs.LevelSelection;

public partial class LevelInfoContainer : Control
{
	private LevelSetProps Props { get; set; } = null;
	private RichTextLabel info;
	private Button battle;
	private LevelProgress LevelProgress { get; set; } = null;
	
	public override void _Ready()
	{
		var container = GetNode("VBoxContainer");
		info = container.GetNode<RichTextLabel>("Info");
		battle = container.GetNode<Button>("Battle");
		
		battle.Pressed += () => {
			if (Props != null && LevelProgress.GetProgressState(Props) != ProgressState.Locked)
			{
				Nodes.GetRoot().SceneManager.Battle(Props);
			}
		};
	}
	
	public void SetLevelProgress(LevelProgress levelProgress)
	{
		LevelProgress = levelProgress;
	}
	
	public void Open(LevelSetProps props, LevelProgressScore progress)
	{
		/*
	public ProgressState GetProgressState(LevelSetProps levelProps)*/
		Props = props;
		info.Text = $"[b]{props.Meta.Name}[/b]\n\n{props.Meta.Desc}";
		battle.Disabled = LevelProgress.GetProgressState(props) == ProgressState.Locked;
		Visible = true;
	}
	
	public void Close()
	{
		Props = null;
		Visible = false;
	}
}
