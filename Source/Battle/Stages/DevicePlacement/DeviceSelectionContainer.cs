using Godot;
using System;
using System.Linq;

namespace drumstalotajs.Battle.Stages.DevicePlacement;

public partial class DeviceSelectionContainer : Container
{
	[Signal] public delegate void SelectedEventHandler(int id);
	
	[Export] private PackedScene buttonCounterScene;
	
	private Resources.Maps.Layers.Entities.PlacableEntityProperties[] PlacableEntityProperties;
	private HBoxContainer buttonContainer;
	private Components.Counteds.ButtonCounter currentButton;
	
	public int SelectedDeviceId { get; private set; } = -1;
	
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
			button.CounterPressed += SelectedDevice;
			buttonContainer.AddChild(button);
		}
		
		SelectedDevice((buttonContainer.GetChild(0) as Components.Counteds.ButtonCounter).TargetEntityId);
	}
	
	public void SelectedDevice(int id)
	{
		currentButton = buttonContainer.GetChildren().First(btn => (btn as Components.Counteds.ButtonCounter).TargetEntityId == id) as Components.Counteds.ButtonCounter;
		SelectedDeviceId = id;
		EmitSignal(SignalName.Selected, id);
	}
}
