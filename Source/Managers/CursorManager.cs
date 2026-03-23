using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Managers;

public partial class CursorManager : Node
{
	[Export] private Texture2D zoomCursorTexture;
	
	public void DragCursor()
	{
		ResetCursor();
		Input.SetDefaultCursorShape(Input.CursorShape.Move);
	}
	
	public void ArrowCursor()
	{
		ResetCursor();
		Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
	}
	
	public void ZoomCursor()
	{
		Input.SetCustomMouseCursor(zoomCursorTexture);
	}

	public void ResetCursor()
	{
		Input.SetCustomMouseCursor(null);
	}
}
