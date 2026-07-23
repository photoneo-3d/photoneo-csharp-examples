# PhoXi API examples

This folder contains standalone C# example applications demonstrating the PhoXi API (`WrapperCSharp.dll`) for interacting with Photoneo 3D scanners / PhoXi devices. Each subdirectory is an independent, self-contained project — there is no shared library code between them. Copy the example closest to your use case and adapt it.

## Prerequisites

- PhoXi Control installed, with the `PHOXI_CONTROL_PATH` environment variable pointing to its install directory (typically `C:\Program Files\Photoneo\PhoXiControl-[version]`).
- CMake 3.10+ (all examples except `WinForms`).
- Visual Studio with C# / .NET Framework tooling.

## Building

Every example (except `WinForms`) is CMake-based and built the same way, from inside the example's directory:

```
cmake -B build .
cmake --build build --config Release
```

CMake finds the PhoXi API via `find_package(PhoXi REQUIRED CONFIG PATHS "${PHO_API_CMAKE_CONFIG_PATH}")`, where `PHO_API_CMAKE_CONFIG_PATH` defaults to `PHOXI_CONTROL_PATH` if not passed explicitly. Targets build against .NET Framework v4.8, 64-bit only, with C# language version `latest`.

`WinForms` is the exception: it ships a plain `WinFormsNoCMake.sln` / `.csproj` (targeting .NET Framework v4.6.1) that references `WrapperCSharp.dll` directly — open/build it in Visual Studio or with `msbuild`, no CMake step required.

## Examples

| Example | What it demonstrates |
|---|---|
| [ConnectAndGrab](ConnectAndGrab) | Checking that PhoXi Control is running, listing devices, connecting to a device, and capturing scans in software-trigger, freerun, and asynchronous modes. |
| [FullAPI](FullAPI) | Comprehensive walkthrough: finding scanners, multiple connection methods, querying device state/capabilities, capturing in free-run and manual mode, requesting every output format, changing scan settings, and disconnecting cleanly. |
| [ChangeSettings](ChangeSettings) | Retrieving and setting capturing, processing, and coordinate settings, plus best practice for disconnecting. |
| [ChangeColorSettings](ChangeColorSettings) | Retrieving and setting color camera settings. |
| [ChangeProfile](ChangeProfile) | Retrieving and setting the device's active profile. |
| [MotionCamChangeSettings](MotionCamChangeSettings) | Retrieving and setting capturing, processing, and coordinate settings specific to MotionCam-3D devices. |
| [PointCloudCalculation](PointCloudCalculation) | Calculating a point cloud from a depth map. |
| [Recording](Recording) | Starting/stopping frame recording, checking recording state, and configuring a recording container (e.g. PLY) for a connected device. |
| [PTPTime](PTPTime) | Reading PTP-synchronized acquisition timestamps from a frame, validating them, formatting them, and computing durations between frames. |
| [TwoScannersMultithread](TwoScannersMultithread) | Discovering and connecting to multiple PhoXi devices and capturing scans concurrently from separate threads. |
| [AutonomousMaintenance](AutonomousMaintenance) | Checking and improving the consistency of the device's calibration via the autonomous maintenance functionality. |
| [MaintenanceCommands](MaintenanceCommands) | Using `PhoXiFactory` maintenance functionality — reboot, shutdown, and factory reset of a device. |
| [WinForms](WinForms) | Using the PhoXi C# API from a Windows Forms application without CMake, including async/await calls that don't block the UI thread. |

Each example folder also has its own `ReadMe.txt` with more detail — read it before modifying that example, as it states the example's intended teaching scope.
