# DotNet Core Samples

This repository contains some workable sample projects which can be used for learning how to develop cross platform applications using .NET Core.

These sammples align to the .NET CLI 1.0.0-rc2-002464.


### Note:

If you get the below error message when you run the application:

    Expected to load libhostpolicy.dylib from [/usr/local/share/dotnet/shared/Microsoft.NETCore.App/1.0.0-xxxxx]
    This may be because the targeted framework ["Microsoft.NETCore.App": "1.0.0-xxxxx"] was not found.
    

You need find the runtime config file (*.runtimeconfig.json) in the directory bin/Debug/netcoreapp1.0, and then replace the version number with the version of your current installed dotnet cli:

    {
        "runtimeOptions": {
            "framework": {
                "name": "Microsoft.NETCore.App",
                "version": "1.0.0-rc2-3002464"
            }
        }
    }
    
    

