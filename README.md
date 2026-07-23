# Photoneo C# examples

![image](https://photoneo.com/files/dw/dw/github/Personal_Linkedin_banner_v4.png)

## Introduction

This repository provides the building blocks necessary for developing your custom C# application for working with [Photoneo](https://www.photoneo.com/) devices. Each example under [`PhoXiAPI/`](PhoXiAPI) is a standalone, self-contained console (or WinForms) application demonstrating one aspect of the PhoXi API — there is no shared library code between them. Start your development based on the example closest to your use case and modify it to suit your specific needs.

See [`PhoXiAPI/README.md`](PhoXiAPI/README.md) for the full list of examples and what each one teaches.

## Prerequisites

- [PhoXi Control](https://www.photoneo.com/) installed, with the `PHOXI_CONTROL_PATH` environment variable pointing to its install directory (typically `C:\Program Files\Photoneo\PhoXiControl-[version]`). The C# API assembly `WrapperCSharp.dll` is loaded from `%PHOXI_CONTROL_PATH%\API\bin`.
- [CMake](https://cmake.org/download/) 3.10 or above (required for all examples except `WinForms`).
- Visual Studio with C# / .NET Framework tooling, to compile the generated solutions (or the standalone `WinForms` `.csproj`).

## Building an example

Each example has its own `CMakeLists.txt` and builds independently — there is no top-level/aggregate build. From inside an example directory, e.g. `PhoXiAPI/ConnectAndGrab`:

```
cmake -B build .
cmake --build build --config Release
```

The `WinForms` example is the one exception: it ships a plain `.sln`/`.csproj` (`WinFormsNoCMake.sln`) with no CMake step — open it directly in Visual Studio or build with `msbuild`.

## Support

Visit [www.photoneo.com](https://www.photoneo.com/) for the most up-to-date information and documents. If you encounter any issues while using the examples, please do not hesitate to contact our dedicated Support team at our [Help Center](https://www.photoneo.com/Help-Center) for prompt assistance.

## License

Photoneo examples are distributed under the [BSD License](https://github.com/photoneo-3d/photoneo-csharp-examples/blob/main/LICENSE).
