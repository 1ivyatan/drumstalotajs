using Godot;
using System;

namespace drumstalotajs.Levels;

public partial class MetadataModal : Control
{
	[Signal] public delegate void ClickedBattleEventHandler(Resources.Sets.Levels.LevelProperties levelProps);
	
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
		toBattleButton.Pressed += () => {
			EmitSignal(SignalName.ClickedBattle, LevelProperties);
		};
	}
	
	public void LoadModal(Resources.Sets.Levels.LevelProperties levelProps)
	{
		LevelProperties = levelProps;
		title.Text = levelProps.Meta.Title;
		desc.Text = levelProps.Meta.Desc;
		Visible = true;
	}
	
	public void CloseModal()
	{
		Visible = false;
	}
}
