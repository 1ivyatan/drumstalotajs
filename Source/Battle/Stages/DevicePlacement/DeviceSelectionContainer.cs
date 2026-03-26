using Godot;
using System;
using System.Linq;

namespace drumstalotajs.Battle.Stages.DevicePlacement;

public partial class DeviceSelectionContainer : Container
{
	[Signal] public delegate void SelectedEventHandler(int id);
	
	[Export] private PackedScene buttonCounterScene;
	private Resources.Sets.Entities.EntityProperties deviceMarker;
	
	private Resources.Maps.Layers.Entities.PlacableEntityProperties[] PlacableEntityProperties;
	private HBoxContainer buttonContainer;
	private Components.Counteds.ButtonCounter currentButton;
	private Mapping.Map map;
	
	public int SelectedDeviceId { get; private set; } = -1;
	
	public override void _Ready()
	{
		buttonContainer = GetNode<HBoxContainer>("HBoxContainer");
	}
	
	public void SetDevices(Mapping.Map map, Resources.Sets.Entities.EntityProperties deviceMarker, Resources.Sets.Entities.EntitySet entitySet, Resources.Maps.Layers.Entities.PlacableEntityProperties[] placableEntityProperties)
	{
		this.map = map;
		this.deviceMarker = deviceMarker;
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
		
		Callable updateSelectedDeviceCall = new Callable(this, nameof(UpdateSelectedDevice));
		
		if (!this.map.EntityLayer.IsConnected("EntityEntered", updateSelectedDeviceCall))
		{
			this.map.EntityLayer.Connect("EntityEntered", updateSelectedDeviceCall);
		}
		
		SelectedDevice((buttonContainer.GetChild(0) as Components.Counteds.ButtonCounter).TargetEntityId);
	}
	
	public void SelectedDevice(int id)
	{
		currentButton = buttonContainer.GetChildren().First(btn => (btn as Components.Counteds.ButtonCounter).TargetEntityId == id) as Components.Counteds.ButtonCounter;
		SelectedDeviceId = id;
		EmitSignal(SignalName.Selected, id);
	}
	
	private void UpdateSelectedDevice(Entities.Entity entity)
	{
		if (!(entity.EntityResource.Id != deviceMarker.Id || entity.EntityResource.EntityType != Entities.EntityType.DEVICE)) return;
		var props = map.MapData.GetPlacableEntityPropertiesById(SelectedDeviceId);
		currentButton.SetCounter(props.Max - map.EntityLayer.GetEntitiesById(SelectedDeviceId).Length);
	}
}
