using Godot;
using System;

namespace drumstalotajs.Battle.TopPanels;

public partial class PauseMenu : Control
{
	[Signal] public delegate void ToLevelsEventHandler();
	
	private Button resumeButton;
	private Button toLevelsButton;

	public override void _Ready()
	{
		resumeButton = GetNode<Button>("ResumeButton");
		toLevelsButton = GetNode<Button>("ToLevelsButton");
		
		resumeButton.Pressed += () => {
			(GetNode("..") as Components.Modals.Window).GetModal().Close();
		};
		
		toLevelsButton.Pressed += () => {
			EmitSignal(SignalName.ToLevels);
		};
	}
}
