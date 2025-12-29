using Godot;
using System;

public partial class TopPanel : PanelContainer
{
	private Button exitButton;
	private Label deviceCountLabel;
	private Label topbarLabel;
	
	private EntityLayer entityLayer;
	private Callable entityAppearanceCall;
	
	private void RefreshCounters(int entityType, Vector2I position)
	{
		if ((Entity.EntityType)entityType == Entity.EntityType.Device)
		{
			long count = this.entityLayer.EntityCollections[Entity.EntityType.Device].Count;
			this.deviceCountLabel.Text = $"{count}";
		}
	}
	
	public void SetTopbarLabel(string text)
	{
		this.topbarLabel.Text = text;
	}
	
	public override void _Ready()
	{
		this.exitButton = this.GetNode<Button>("OptionsStatsGrid/ExitButton");
		this.deviceCountLabel = this.GetNode<Label>("OptionsStatsGrid/StatsContainer/DeviceCount");
		this.topbarLabel = this.GetNode<Label>("FlyingLabel");
		
		this.entityLayer = this.GetNode<TileMapLayer>("../MapControl/MapContainer/Map/Grid/EntityLayer") as EntityLayer;
		this.entityAppearanceCall = new Callable(this, nameof(RefreshCounters));
		this.entityLayer.Connect("EntitySpawned", this.entityAppearanceCall);
		this.entityLayer.Connect("EntityDestroyed", this.entityAppearanceCall);
	}
	
	public override void _ExitTree()
	{
		this.entityLayer.Disconnect("EntitySpawned", this.entityAppearanceCall);
		this.entityLayer.Disconnect("EntityDestroyed", this.entityAppearanceCall);
	}
}
