using Godot;
using System;

public abstract partial class Stage : Node2D
{
	public abstract void Input(InputEvent @event);
	public abstract void Load();
	protected Node MapRootNode;
	
	//public override void _Ready()
	//{
	//	Load();
	//}
	
	public void SetMapRootNode(Node mapRootNode) {
		MapRootNode = mapRootNode;
	}
}
