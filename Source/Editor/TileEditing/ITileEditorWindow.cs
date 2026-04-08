using Godot;
using System;

namespace Drumstalotajs.Editor.TileEditing;

public interface ITileEditorWindow<T>
{
	public void Load(T tile);
	public void Save();
}
