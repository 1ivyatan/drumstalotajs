using Godot;
using System;

public abstract partial class Stage : Node2D
{
	protected Node mapRootNode;
	protected Node2D mapGridNode;
	protected Control sceneUiNode;
	
	public abstract void Input(InputEvent @event);
	public abstract void LoadStage();
	
	public void Load(Control uiNode, Node mapNode, Node2D gridNode)
	{
		sceneUiNode = uiNode;
		mapRootNode = mapNode;
		mapGridNode = gridNode;
		LoadStage();
	}
}
