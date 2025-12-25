using Godot;
using System;

public partial class Battle : VBoxContainer
{
	[Signal]
	public delegate void LevelSelectEventHandler();
	
	Node headerContainer;
	Node footerContainer;
	Map map;
	
	public override void _Ready()
	{
		map = GetNode<Node2D>("MapContainer/Map") as Map;
		map.LoadLevel("1");
	}
	
	public Node HeaderWidget
	{
		get
		{ 
			headerContainer = GetNode<Control>($"HeaderContainer");
			return (headerContainer.GetChildCount() == 1)
				? headerContainer.GetChild(0)
				: null;
		}
	}
	
	public Node FooterWidget
	{
		get
		{ 
			footerContainer = GetNode<Control>($"FooterContainer");
			return (footerContainer.GetChildCount() == 1)
				? footerContainer.GetChild(0)
				: null;
		}
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
