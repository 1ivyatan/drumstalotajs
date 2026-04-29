using Godot;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FileAccess = Godot.FileAccess;
using Path = System.IO.Path;
using RandomNumberGenerator = System.Security.Cryptography.RandomNumberGenerator;

namespace Drumstalotajs.Utilities;

public static class SecureSavefiles
{
	private static readonly byte[] SaveMagic = Encoding.ASCII.GetBytes("DSA1");
	private const int SaveFormatVersion = 1;
	private const int SignatureLength = 32;
	private const string DeviceKeyPath = "user://save_integrity.key";
	
	public static bool SaveSignedBinary(string path, byte[] payloadBytes)
	{
		if (string.IsNullOrWhiteSpace(path) || payloadBytes == null)
		{
			return false;
		}
		
		byte[] key = GetOrCreateDeviceKey();
		byte[] header = BuildHeader(payloadBytes.Length);
		byte[] signature = ComputeSignature(key, header, payloadBytes);
		
		string absolutePath = ProjectSettings.GlobalizePath(path);
		EnsureParentDirectoryExists(absolutePath);
		
		using FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
		if (file == null)
		{
			return false;
		}
		
		file.StoreBuffer(header);
		file.StoreBuffer(signature);
		file.StoreBuffer(payloadBytes);
		return file.GetError() == Error.Ok;
	}
	
	public static bool TryLoadSignedBinary(string path, out byte[] payloadBytes)
	{
		payloadBytes = null;
		if (!FileAccess.FileExists(path))
		{
			return false;
		}
		
		using FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		if (file == null)
		{
			return false;
		}
		
		byte[] header = file.GetBuffer(SaveMagic.Length + sizeof(int) + sizeof(int));
		if (!TryReadHeader(header, out int payloadLength))
		{
			return false;
		}
		
		byte[] signature = file.GetBuffer(SignatureLength);
		byte[] payload = file.GetBuffer(payloadLength);
		if (payload.Length != payloadLength)
		{
			return false;
		}
		
		byte[] expectedSignature = ComputeSignature(GetOrCreateDeviceKey(), header, payload);
		if (!CryptographicOperations.FixedTimeEquals(signature, expectedSignature))
		{
			return false;
		}
		
		payloadBytes = payload;
		return true;
	}
	
	public static string CreateTimestampedBackupPath(string path)
	{
		string timestamp = Time.GetDatetimeStringFromSystem().Replace(":", "-");
		return $"{path}.{timestamp}.bak";
	}
	
#if DEBUG
	public static bool TamperOneByte(string path)
	{
		if (!FileAccess.FileExists(path))
		{
			return false;
		}
		
		using FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.ReadWrite);
		if (file == null || file.GetLength() <= 0)
		{
			return false;
		}
		
		file.Seek(file.GetLength() - 1);
		byte current = (byte)file.Get8();
		file.Seek(file.GetLength() - 1);
		file.Store8((byte)(current ^ 0xFF));
		return file.GetError() == Error.Ok;
	}
#endif
	
	private static byte[] BuildHeader(int payloadLength)
	{
		byte[] header = new byte[SaveMagic.Length + sizeof(int) + sizeof(int)];
		Buffer.BlockCopy(SaveMagic, 0, header, 0, SaveMagic.Length);
		Buffer.BlockCopy(BitConverter.GetBytes(SaveFormatVersion), 0, header, SaveMagic.Length, sizeof(int));
		Buffer.BlockCopy(BitConverter.GetBytes(payloadLength), 0, header, SaveMagic.Length + sizeof(int), sizeof(int));
		return header;
	}
	
	private static bool TryReadHeader(byte[] header, out int payloadLength)
	{
		payloadLength = 0;
		if (header.Length < SaveMagic.Length + sizeof(int) + sizeof(int))
		{
			return false;
		}
		
		byte[] magic = new byte[SaveMagic.Length];
		Buffer.BlockCopy(header, 0, magic, 0, SaveMagic.Length);
		if (!magic.SequenceEqual(SaveMagic))
		{
			return false;
		}
		
		int formatVersion = BitConverter.ToInt32(header, SaveMagic.Length);
		if (formatVersion != SaveFormatVersion)
		{
			return false;
		}
		
		payloadLength = BitConverter.ToInt32(header, SaveMagic.Length + sizeof(int));
		return payloadLength >= 0;
	}
	
	private static byte[] ComputeSignature(byte[] key, byte[] header, byte[] payload)
	{
		byte[] data = new byte[header.Length + payload.Length];
		Buffer.BlockCopy(header, 0, data, 0, header.Length);
		Buffer.BlockCopy(payload, 0, data, header.Length, payload.Length);
		using HMACSHA256 hmac = new(key);
		return hmac.ComputeHash(data);
	}
	
	private static byte[] GetOrCreateDeviceKey()
	{
		if (FileAccess.FileExists(DeviceKeyPath))
		{
			using FileAccess existing = FileAccess.Open(DeviceKeyPath, FileAccess.ModeFlags.Read);
			byte[] key = existing?.GetBuffer(32) ?? [];
			if (key.Length == 32)
			{
				return key;
			}
		}
		
		byte[] generated = RandomNumberGenerator.GetBytes(32);
		string absolutePath = ProjectSettings.GlobalizePath(DeviceKeyPath);
		EnsureParentDirectoryExists(absolutePath);
		using FileAccess created = FileAccess.Open(DeviceKeyPath, FileAccess.ModeFlags.Write);
		created?.StoreBuffer(generated);
		return generated;
	}
	
	private static void EnsureParentDirectoryExists(string absolutePath)
	{
		string parentDirectory = Path.GetDirectoryName(absolutePath);
		if (string.IsNullOrWhiteSpace(parentDirectory))
		{
			return;
		}
		
		DirAccess.MakeDirRecursiveAbsolute(parentDirectory);
	}
}
