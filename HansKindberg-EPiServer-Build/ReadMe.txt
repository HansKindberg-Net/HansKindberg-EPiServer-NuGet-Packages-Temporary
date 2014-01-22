HansKindberg-EPiServer-Build
----------------------------
This package contains build-targets.
This package has a dependency to HansKindberg-EPiServer-Application-Files and through that also to HansKindberg-EPiServer-Binaries.

Regarding HansKindberg-EPiServer-Application-Files:
The package contains build scripts/targets that transforms the config-files.
1.
- package\EPiServer-Application-Files.x.x.x.x\Configuration\log4net.Template.config with log4net.Common.config (transforms) to log4net.Template.config
- package\EPiServer-Application-Files.x.x.x.x\Configuration\Web.Template.config with Web.Common.config (transforms) to Web.Template.config
2.
- log4net.Template.config with log4net.$(Configuration).config (transforms) to log4net.config
- Web.Template.config with Web.$(Configuration).config (transforms) to Web.config
Exclude
- *.config
- *.Template.config
from source-control.

Regarding HansKindberg-EPiServer-Binaries:
This package contains build scripts/targets that copies all the binaries to the bin folder on build.