using Godot;
using System;

namespace Drumstalotajs.Utils;

public static class Files
{	
	public static T SafeLoadResource<T>(string path) where T : Resource
	{
		if (Exists<T>(path))
		{
			return ResourceLoader.Load<T>(path);
		}
		return null;
	}
	public static bool Exists<T>(string path) where T : Resource
	{
		return (ResourceLoader.Exists(path, typeof(T).Name));
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
