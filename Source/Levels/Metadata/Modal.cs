using Godot;
using System;

namespace drumstalotajs.Levels.Metadata;

public partial class Modal : Control
{
	private Resources.Sets.Levels.LevelProperties LevelProperties { get; set; }

	private Label title;
	private RichTextLabel desc;
	private Button toBattleButton;
	
	public override void _Ready()
	{
		Container container = GetNode<Container>("VBoxContainer");
		title = container.GetNode<Label>("Title");
		desc = container.GetNode<RichTextLabel>("Desc");
		toBattleButton = container.GetNode<Button>("ToBattleButton");
	}
	
	public void LoadModal(Resources.Sets.Levels.LevelProperties levelProps)
	{
		Visible = true;
	}
	
	public void CloseModal()
	{
		Visible = false;
	}
}
