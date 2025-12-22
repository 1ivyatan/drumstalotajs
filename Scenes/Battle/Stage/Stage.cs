using Godot;
using System;

public abstract partial class Stage : Node2D
{
	public abstract void Input(InputEvent @event);
	public abstract void Load();
	
	protected Node mapRootNode;
	protected Node2D mapGridNode;
	
	public Node MapRootNode
	{
		get { return mapRootNode; }
		set { mapRootNode = value; }
	}
	
	public Node2D MapGridNode
	{
		get { return mapGridNode; }
		set { mapGridNode = value; }
	}
}
