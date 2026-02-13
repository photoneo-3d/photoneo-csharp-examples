cmake_minimum_required (VERSION 3.10)

if(POLICY CMP0054)
    cmake_policy(SET CMP0054 NEW)
endif()

project (ChangeColorSettingsExample_CSharp LANGUAGES CSharp CXX)

set(Files
    ${ChangeColorSettingsExample_CSharp_SOURCE_DIR}/Program.cs
    ${ChangeColorSettingsExample_CSharp_SOURCE_DIR}/ReadMe.txt
)

add_executable(ChangeColorSettingsExample_CSharp
    ${Files}
)

if(NOT PHO_API_CMAKE_CONFIG_PATH)
    set(PHO_API_CMAKE_CONFIG_PATH "$ENV{PHOXI_CONTROL_PATH}")
endif()

if(PHO_BUILT_IN_API_IN_EXAMPLES)
    set_property(TARGET ${PROJECT_NAME} PROPERTY 
        VS_DOTNET_REFERENCE_WrapperCSharp ${PHOXI_MANAGED_DLL_FOR_EXAMPLE})
else()
    find_package(PhoXi REQUIRED CONFIG PATHS "${PHO_API_CMAKE_CONFIG_PATH}")
    
    set_property(TARGET ${PROJECT_NAME} PROPERTY 
        VS_DOTNET_REFERENCE_WrapperCSharp ${PHOXI_API_MANAGED_DLL})
endif()

# Set the .NET Framework version for the executable.
set_property(TARGET ChangeColorSettingsExample_CSharp PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.8")
# Set the console application.
set_property(TARGET ChangeColorSettingsExample_CSharp PROPERTY WIN32_EXECUTABLE FALSE)
# Set the executable to be 64-bit.
set_property(TARGET ChangeColorSettingsExample_CSharp PROPERTY VS_GLOBAL_Prefer32Bit "false")
# Add assembly reference for SaveTexture
set_property(TARGET ChangeColorSettingsExample_CSharp PROPERTY VS_DOTNET_REFERENCES "System.Drawing")
# Allow unsafe operations in SaveTexture example
target_compile_options(ChangeColorSettingsExample_CSharp PUBLIC "/unsafe")
# Set the C# language version (defaults to 3.0).
set(CMAKE_CSharp_FLAGS "/langversion:latest")