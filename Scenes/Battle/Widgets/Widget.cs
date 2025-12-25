using Godot;
using System;

public abstract partial class Widget : Control
{
	protected Node root;
	protected Node grid;
	
	protected abstract void LoadWidget();
	
	public void Load(Node rootNode, Node gridNode)
	{
		root = rootNode;
		grid = gridNode;
		LoadWidget();
	}
}
