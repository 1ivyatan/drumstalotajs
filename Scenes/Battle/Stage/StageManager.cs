using Godot;
using System;

public partial class StageManager : Control
{
	[Signal]
	public delegate void StageChangedEventHandler(Stage newStage);
	
	public Stage ActiveStage { get; private set; }
	
	public void LoadStage(string name)
	{
		string stagePath = $"res://Scenes/Battle/Stage/Stages/{name}.tscn";
		
		if (this.ActiveStage != null)
		{
			this.ActiveStage.QueueFree();
			this.RemoveChild(this.ActiveStage);
		}
		
		if (ResourceLoader.Exists(stagePath))
		{
			Node stageNode = ResourceLoader.Load<PackedScene>(stagePath).Instantiate();
			
			this.ActiveStage = stageNode as Stage;
			this.AddChild(stageNode);
			this.EmitSignal(SignalName.StageChanged, this.ActiveStage);
		}
	}
}
