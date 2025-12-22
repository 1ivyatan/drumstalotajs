using Godot;
using System;

public abstract partial class Stage : Node2D
{
	public abstract void Input(InputEvent @event);
	public abstract void Load();
	
	protected Node MapRootNode;
	protected Node2D MapGridNode;
	
	public void SetMapRootNode(Node mapRootNode) {
		MapRootNode = mapRootNode;
	}
	
	public void SetMapGridNode(Node2D mapGridNode) {
		MapGridNode = mapGridNode;
	}
}
