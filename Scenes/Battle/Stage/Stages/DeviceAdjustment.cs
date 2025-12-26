using Godot;
using System;

public partial class DeviceAdjustment : Stage
{
	EntityLayer entityLayer;
	Selector selector;
	
	public override void LoadStage()
	{
		Map map = mapRootNode as Map;	
		entityLayer = mapGridNode.GetNode<TileMapLayer>("EntityLayer") as EntityLayer;
		selector = mapGridNode.GetNode<Node2D>("Selector") as Selector;
		
		selector.Connect("ClickedOnEntity", new Callable(this, nameof(SelectedEntity)));
	}
	
	void SelectedEntity(int entityType, Vector2I position)
	{
		GD.Print(entityLayer.GetEntityCollection((EntityType)entityType).GetInstance(position));
	}
	
	public override void Input(InputEvent @event)
	{
	}
}
