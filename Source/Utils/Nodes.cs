using Godot;
using System;

namespace Drumstalotajs.Utils;

public static class Nodes
{
	public static Main GetRoot()
	{
		return ((SceneTree)Engine.GetMainLoop()).Root.GetNode("Main") as Main;
	}
	
	public static Node GetSceneRoot()
	{
		return ((SceneTree)Engine.GetMainLoop()).Root.GetNode("Main/SceneManager").GetChild(0);
	}
}
