using Godot;
using System;
using Drumstalotajs.Resources.Levels;

public partial class LevelInfoContainer : Control
{
	public LevelSetProps Props { get; private set; } = null;
	private RichTextLabel info;
	private Button battle;
	
	public override void _Ready()
	{
		var container = GetNode("VBoxContainer");
		info = container.GetNode<RichTextLabel>("Info");
		battle = container.GetNode<Button>("Battle");
		
		battle.Pressed += () => {
			
		};
	}
	
	public void Open(LevelSetProps props)
	{
		Props = props;
		info.Text = $"[b]{props.Meta.Name}[/b]\n\n{props.Meta.Desc}";
		Visible = true;
	}
	
	public void Close()
	{
		Props = null;
		Visible = false;
	}
}
