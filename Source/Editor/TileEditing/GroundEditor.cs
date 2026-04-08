using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Editor.TileEditing;

public partial class GroundEditor : Control
{
	private Map _map;
	private Label _relHeight;
	private SpinBox _heightChanger;
	
	public override void _Ready()
	{
		_map = Nodes.GetSceneRoot().Map;
		var heightEditor = GetNode("HeightEditor");
		_relHeight = heightEditor.GetNode<Label>("RelHeight");
		_heightChanger = heightEditor.GetNode<SpinBox>("HeightChanger");
	}
	
	public void Load(Vector2I position)
	{
		_map = Nodes.GetSceneRoot().Map;
		_relHeight.Text = $"{_map.GroundLayer.GetUnaddedHeight(position)}+";
		_heightChanger.Value = _map.GroundLayer.GetAddedHeight(position);
	}
}
