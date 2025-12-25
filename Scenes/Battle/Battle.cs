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
	
	void RefreshContainer(Node container, string name, string type)
	{
		container = GetNode<Control>($"{type}Container");
		
		foreach (Node child in container.GetChildren())
		{
			child.QueueFree();
			container.RemoveChild(child);
		}
		
		string path = $"res://Scenes/Battle/Widgets/{name}/{name}";
		
		if (ResourceLoader.Exists($"{path}{type}.tscn")) 
		{
			Node child = ResourceLoader.Load<PackedScene>($"{path}{type}.tscn").Instantiate();
			(child as Widget).Load(this, GetNode("MapContainer/Map/Grid"));
			container.AddChild(child);
		}
	}
	
	void StageChanged(string name)
	{	
		RefreshContainer(headerContainer, name, "Header");
		RefreshContainer(footerContainer, name, "Footer");
	}
	
	public void LeaveBattle() {
		EmitSignal(SignalName.LevelSelect);
	}
}
