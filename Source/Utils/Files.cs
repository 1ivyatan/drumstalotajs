using Godot;
using System;

namespace Drumstalotajs.Utils;

public static class Files
{
	public static T SafeLoadResource<T>(string path) where T : Resource, new()
	{
		if (ResourceLoader.Exists(path))
		{
			return ResourceLoader.Load<T>(path);
		} else
		{
			T resource = new T();
			resource.ResourcePath = path;
			return resource;
		}
	}
	
	public static FileAccess SafeLoadFile(string path, FileAccess.ModeFlags flags)
	{
		if (FileAccess.FileExists(path))
		{
			return FileAccess.Open(path, flags);
		}
		return null;
	}
	
}
