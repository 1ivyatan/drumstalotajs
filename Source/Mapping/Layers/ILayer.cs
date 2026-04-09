using Godot;
using System;

namespace Drumstalotajs.Mapping.Layers;

public interface ILayer<T>
{
	public T[] GetAtlas();
}
