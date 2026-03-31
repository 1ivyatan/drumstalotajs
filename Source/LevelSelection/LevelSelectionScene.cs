using Godot;
using System;
using System.Linq;
using System.Collections; 
using System.Collections.Generic;
using System.Collections.Specialized;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Sets.LevelSets;

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionScene : Node
{
	private LevelSet LevelSet { get; set; }
	private Button _toStart;
	
	public override void _Ready()
	{
		Node overlay = GetNode("Overlay");
		LevelSet = Nodes.GetRoot().DataManager.LevelSetProgress.Keys.First();
		_toStart = overlay.GetNode<Button>("ToStart");
		
		_toStart.Pressed += () => {
			Nodes.GetRoot().SceneManager.Start();
		};
	}
}
