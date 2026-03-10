using Godot;
using System;

namespace drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	private Mapping.Map map;
	private RichTextLabel help;
	private HeightDisplay heightDisplay;
	private Vector2I[] groundLayerAtlas;
	
	public override void _Ready()
	{
		map = GetNode<Node2D>("Map") as Mapping.Map;
		help = GetNode<RichTextLabel>("UI/Help");
		groundLayerAtlas = map.GroundLayer.GetTileAtlas();
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent)
		{
			if (keyEvent.Pressed)
			{
				if (!keyEvent.Echo)
				{
					switch (keyEvent.Keycode)
					{
						case Key.H:
							help.SetVisible(!help.Visible);
							break;
						case Key.S:
							if (keyEvent.CtrlPressed)
							{
								GD.Print("Ctrl+S pressed manually");
							}
							break;
						case Key.G:
							NextTileFromAtlas(map.GroundLayer, groundLayerAtlas, map.CurrentCellPos);
							break;
						default: break;
					}	
				}
			}
		}
	}
	
	private void NextTileFromAtlas(Mapping.Layers.Layer layer, Vector2I[] atlas, Vector2I cellPos)
	{
		Vector2I currentAtlas = layer.GetCellAtlasCoords(cellPos);
		int index = Array.IndexOf(atlas, currentAtlas);
		int length = atlas.Length;
		int next = index + 1;
		
		if (next < length)
		{
			layer.SetCell(cellPos, 0, atlas[next], 0);
		} else
		{
			layer.EraseCell(cellPos);
		}
	}
}
