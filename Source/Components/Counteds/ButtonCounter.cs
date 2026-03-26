using Godot;
using System;

namespace drumstalotajs.Components.Counteds;

public partial class ButtonCounter : Button
{
	[Signal] public delegate void CounterPressedEventHandler(int id);
	
	public int TargetEntityId { get; private set; }
	
	public override void _Ready()
	{
		Pressed += () => {
			EmitSignal(SignalName.CounterPressed, TargetEntityId);
		};
	}

	public void SetCounter(int num)
	{
		Text = $"{num}";
	}
	
	public void SetButton(int id, Texture2D texture)
	{
		TargetEntityId = id;
		Icon = texture;
		SetCounter(0);
	}
}
