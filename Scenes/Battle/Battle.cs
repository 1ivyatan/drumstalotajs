using Godot;
using System;

public partial class Battle : VBoxContainer
{
	[Signal]
	public delegate void LevelSelectEventHandler();
	
	Node headerContainer;
	Node footerContainer;
	
	public override void _Ready()
	{
	}
	
	void StageChanged(string name)
	{
		headerContainer = GetNode<PanelContainer>("HeaderContainer");
		footerContainer = GetNode<PanelContainer>("FooterContainer");
		
		foreach (Node child in headerContainer.GetChildren())
		{
			child.QueueFree();
			headerContainer.RemoveChild(child);
		}
		
		foreach (Node child in footerContainer.GetChildren())
		{
			child.QueueFree();
			footerContainer.RemoveChild(child);
		}
		
		string path = $"res://Scenes/Battle/Controls/{name}";
		
		if (ResourceLoader.Exists($"{path}/Header.tscn")) 
		{
			Node header = ResourceLoader.Load<PackedScene>($"{path}/Header.tscn").Instantiate();
			headerContainer.AddChild(header);
		}
		
		if (ResourceLoader.Exists($"{path}/Footer.tscn"))
		{
			Node footer = ResourceLoader.Load<PackedScene>($"{path}/Footer.tscn").Instantiate();
			footerContainer.AddChild(footer);
		}
	}

	void ToLevelSelect() {
		EmitSignal(SignalName.LevelSelect);
	}
}
