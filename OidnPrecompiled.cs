using System.Reflection;
using System.Runtime.InteropServices;

namespace OpenImageDenoisePrecompiled;

public static class OidnPrecompiled
{
	/// <summary>
	/// The path to the precompiled Open Image Denoise binary.
	/// </summary>
	public const string Path = "OpenImageDenoise";

	/// <summary>
	/// The path to the precompiled Thread Building Blocks binary required by Oidn.
	/// </summary>
	public const string TbbPath = "tbb12";

	static readonly object locker = new();

	static IntPtr oidnHandle;
	static IntPtr tbbHandle;

	/// <summary>
	/// Tries to load Oidn.
	/// </summary>
	/// <returns>Whether Odin was successfully loaded.</returns>
	/// <remarks>Using this method prior to invoking methods marked with <see cref="DllImportAttribute"/>
	/// might help if those methods are failing with <see cref="DllNotFoundException"/>.</remarks>
	public static bool TryLoad()
	{
		const DllImportSearchPath Search = DllImportSearchPath.UseDllDirectoryForDependencies |
										   DllImportSearchPath.SafeDirectories;

		var assembly = Assembly.GetCallingAssembly();

		lock (locker)
		{
			if (NativeLibrary.TryLoad(TbbPath, assembly, Search, out tbbHandle))
			{
				if (NativeLibrary.TryLoad(Path, assembly, Search, out oidnHandle)) return true;

				NativeLibrary.Free(tbbHandle);
			}

			oidnHandle = IntPtr.Zero;
			tbbHandle = IntPtr.Zero;
		}

		return false;
	}

	/// <summary>
	/// Unloads the Odin library that was loaded with <see cref="TryLoad"/>.
	/// </summary>
	public static void Unload()
	{
		lock (locker)
		{
			NativeLibrary.Free(oidnHandle);
			NativeLibrary.Free(tbbHandle);

			oidnHandle = IntPtr.Zero;
			tbbHandle = IntPtr.Zero;
		}
	}
}