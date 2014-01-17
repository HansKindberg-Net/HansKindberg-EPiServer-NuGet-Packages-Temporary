EPiServer-Binaries
------------------
This package contains all the EPiServer binaries except:
- log4net.dll
- Microsoft.Web.Services3.dll
but those assemblies are referenced from NuGet.

The package contains build scripts/targets that copy all the binaries to the bin folder on build.