using Godot;
using System;

namespace Drumstalotajs.Mapping.Tiles;

public interface IInitializer<T>
{
	public void Initialize(T data);
}
