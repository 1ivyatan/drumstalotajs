using Godot;
using System;
using Drumstalotajs.Managers.Scenes;

namespace Drumstalotajs.Utilities;

public static class Nodes
{/*
	public static Map GetMap()
	{
		var main = GetSceneRoot();
		foreach (var node in main.GetChildren())
		{
			if (node is Map)
			{
				return node as Map;
			}
		}
		
		return null;
	}*/

	public static Main GetRoot()
	{
		return ((SceneTree)Engine.GetMainLoop()).Root.GetNode("Main") as Main;
	}

	public static dynamic GetSceneRoot()
	{
		foreach (var node in ((((SceneTree)Engine.GetMainLoop()).Root.GetNode("Main/SceneManager")) as SceneManager).GetChildren())
		{
			if (!(node is FadeCurtainContainer)) return node;
		}
		return null;
	}
}
