EPiServer-Application-Files
---------------------------
This package contains all the EPiServer application-files (admin, edit etc.). The package
does not add any content to the project. The content in the package can be copied to the
project, by using pre- and post-build events, or mapped to, by using virtual path providers.

Application
The folder contains the EPiServer application files that normally is installed
under %PROGRAMFILES%\EPiServer\CMS\x.x.x.x\Application.

Configuration
The folder contains templates:
- log4net.Template.config
- Web.Template.config
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