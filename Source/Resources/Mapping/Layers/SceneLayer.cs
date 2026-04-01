using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class SceneLayer : Resource
{
	[Export] public SceneTile[] SceneTiles { get; private set; }
}
