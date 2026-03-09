using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class LevelMarker : Button
	{
		[Signal] public delegate void SelectedEventHandler(Resources.Levels.LevelProps levelProps);
		
		public Resources.Levels.LevelProps LevelProps { get; private set; }
		
		public override void _Pressed()
		{
			EmitSignal(SignalName.Selected, LevelProps);
		}
		
		public void SetMarker(Resources.Levels.LevelProps levelProps)
		{
			LevelProps = levelProps;
			Position = levelProps.Position;
		}
	}
}
