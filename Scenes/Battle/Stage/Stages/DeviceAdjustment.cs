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
		
		selector.SetSelectMode(Selector.SelectMode.Filtered);
		selector.SetFilter([ EntityType.Device ]);
		selector.Enabled(true);
		
		selector.Connect("ClickedOnEntity", new Callable(this, nameof(SelectedEntity)));
	}
	
	public override void CloseStage()
	{
		
	}
	
	void SelectedEntity(int entityType, Vector2I position)
	{
		Entity entityInstance = entityLayer.GetEntityCollection((EntityType)entityType).GetInstance(position);
				
		GD.Print(entityInstance);
	}
	
	public override void Input(InputEvent @event)
	{
	}
}
