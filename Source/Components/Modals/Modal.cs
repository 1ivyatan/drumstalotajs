using Godot;
using System;

namespace drumstalotajs.Components.Modals;

public partial class Modal : Control
{
	[Signal] public delegate void ClosedEventHandler();
	
	private Control window;
	private Control background;
	
	public override void _Ready()
	{
		Close();
		window = GetNode<Control>("Window");
		background = GetNode<Control>("Background");
		
		foreach (Node child in GetChildren())
		{
			if (child != window && child != background)
			{
				AddToWindow(child);
			}
		}
		
		ChildEnteredTree += AddToWindow;
	}
	
	public Control GetModalWindow()
	{
		return window;
	}
	
	public void Open()
	{
		Visible = true;
	}
	
	public void Close()
	{
		EmitSignal(SignalName.Closed);
		Visible = false;
	}
	
	private void AddToWindow(Node node)
	{
		if (node != window && node != background)
		{
			node.Reparent(window);
		}
	}
}
