# OpenImageDenoisePrecompiled

This is a [NuGet](https://www.nuget.org/packages/OpenImageDenoisePrecompiled/1.0.0] package) package that simply includes precompiled releases of Intel's [Open Image Denoise](http://openimagedenoise.org) library.
The precompiled files support 64-bit versions of Linux, MacOS, and Windows, and they came straight from the official [download page](https://www.openimagedenoise.org/downloads.html).
There is also a simple class `OidnPrecompiled` that helps with the loading and unloading of the library in C#.
I use this package in the [Echo](https://github.com/GaryHuan9/Echo) project.

To update the files when a new version comes out:
1. Download the 3 compressed files from the [download page](https://www.openimagedenoise.org/downloads.html).
2. Uncompress the downloaded files into folders.
3. Rename and copy the `OpenImageDenoise` and `tbb` binaries into `./oidn/linux/`, `./oidn/macos/`, and `./oidn/windows/`.
4. Build the project, this should automatically pack it.
5. Find the `.nuget` file in `./bin/Debug/` and upload it to `nuget.org`.

Note: this git repository does not actually include the binary files for size and copyright issues; each precompiled binary is about 50MB large.