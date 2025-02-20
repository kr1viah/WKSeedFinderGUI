# WindowkillSeedFinderGUI

This is a GUI for my [Windowkill Seed Finder](https://github.com/kr1viah/WKChallengeModeSeedFinder), which is a tool to find seeds for [Windowkill](https://store.steampowered.com/app/2726450/Windowkill/), written in Go, Cuda, and C#. 

On my GTX 1660, it can check every possible seed (roughly 4 billion) in 4 minutes (average time per seed is 54ns, and on my Ryzen 7 5800x it takes 30 minutes (average time per seed is 413ns).
## Features
- Find seeds for challenge mode in Windowkill on the GPU or CPU (CPU is only there for those without a CUDA capable GPU)
- Parameters:
	- Number of threads (CPU only)
	- Upgrades
	- Character
	- Starting time
	- Ability and level of ability

## Installation
1. Download the latest release from the [releases page](https://github.com/kr1viah/WKSeedFinderGUI/releases) and extract it
2. Install dotnet 8.0 or higher from [here](https://dotnet.microsoft.com/download/dotnet/8.0)
3. Run WKSeedFinderGUI.exe

## Planned Features
Features that might be added later if I feel like it

- Save/load parameters to/from a file 
- Keeping track of the best seed found
- Have the option to search for every seed matching the parameters (up to a point), not just the first one
- More options for the seed finder (Such as bosses, although this would require a lot of extra work. Currently, only implemented in Go, and you can compile the project yourself if you want to search for bosses too)
- Rewrite the seed finder in OpenCL for better compatibility with different GPUs

## Building
After cloning the repository, run `dotnet build` (or `dotnet publish -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true` for a single .exe file) in the root directory to build the project. The executable will be in `WindowkillSeedFinderGUI/bin/Debug/net8.0-windows/WindowkillSeedFinderGUI.exe` (or `net8.0-windows/win-x64/publish/WindowkillSeedFinderGUI.exe`).

As for the seed finder itself, install GCC to path, Go, the Cuda toolkit, and run `nvcc -shared -o main.dll main.cu` followed by `go build` in the `WindowkillSeedFinder` directory. The executable and .dll file will be in the same directory.

## Contact
If you have any questions or suggestions, feel free to contact me on Discord (.kr1v) or open an issue on the repository.