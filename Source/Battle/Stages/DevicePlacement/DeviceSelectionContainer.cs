using Godot;
using System;

namespace drumstalotajs.Battle.Stages.DevicePlacement;

public partial class DeviceSelectionContainer : Container
{
	[Export] private PackedScene buttonCounterScene;
	
	private Resources.Maps.Layers.Entities.PlacableEntityProperties[] PlacableEntityProperties;
	
	private HBoxContainer buttonContainer;
	
	public override void _Ready()
	{
		buttonContainer = GetNode<HBoxContainer>("HBoxContainer");
	}
	
	public void SetDevices(Resources.Sets.Entities.EntitySet entitySet, Resources.Maps.Layers.Entities.PlacableEntityProperties[] placableEntityProperties)
	{
		PlacableEntityProperties = placableEntityProperties;
		
		foreach (var button in buttonContainer.GetChildren())
		{
			button.QueueFree();
			buttonContainer.RemoveChild(button);
		}
		
		foreach (var placableProps in placableEntityProperties)
		{
			Components.Counteds.ButtonCounter button = buttonCounterScene.Instantiate() as Components.Counteds.ButtonCounter;
			var entProps = entitySet.GetEntityProps(placableProps.Id);
			button.SetButton(placableProps.Id, entProps.Icon);
			buttonContainer.AddChild(button);
		}
	}
}
