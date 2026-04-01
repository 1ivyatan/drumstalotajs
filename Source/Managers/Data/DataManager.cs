using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Managers.Data;

public partial class DataManager : Node
{
	[Export] public LevelSet[] LevelSets { get; set; }
	
	
}
/*
namespace Drumstalotajs.Resources.Levels;

[GlobalClass]
public partial class LevelSet : Resource*/
