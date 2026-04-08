using Godot;
using System;

namespace Drumstalotajs.Mapping.Layers;

public interface ILayerAtlas<T>
{
	public T[] GetAtlas();
}
