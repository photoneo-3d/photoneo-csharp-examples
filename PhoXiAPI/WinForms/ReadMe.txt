========================================================================
    WINDOWS FORMS APPLICATION : WinFormsNoCMake Project Overview
========================================================================

This is a simple example of how to use Photoneo PhoXi C# API without CMake in
Windows Forms application.

You will learn how to:

* set up your C# project independent from CMake,
* use WinForms in your project.

==============
Prerequisites:
==============
Make sure the PHOXI_CONTROL_PATH environment variable is correctly set to the currently
installed PhoXi Control, typically C:\Program Files\Photoneo\PhoXiControl-[version].
The C# API library WrapperCSharp.dll is located in the path %PHOXI_CONTROL_PATH%\API\bin.

=========
Overview:
=========
No CMake
Example application shows how to use Wrapper C# API without CMake.

WinForms and async/await
This example also illustrates how to use Wrapper C# API dlls in WinForms
application without blocking the UI thread during API calls.

/////////////////////////////////////////////////////////////////////////////
