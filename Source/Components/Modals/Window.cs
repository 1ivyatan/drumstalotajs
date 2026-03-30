using Godot;
using System;

namespace Drumstalotajs.Components.Modals;

public partial class Window : Container
{
	public Modal GetModal()
	{
		return GetNode("../..") as Modal;
	}
}
