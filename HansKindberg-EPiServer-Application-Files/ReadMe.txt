HansKindberg-EPiServer-Application-Files
----------------------------------------
This package contains all the EPiServer application-files (admin, edit etc.) and *.config templates. The package
does not add any content to the project. The content in the package can be copied to the project, by using
pre- and post-build events, or mapped to, by using virtual path providers.

Application
The folder contains the EPiServer application files that normally is installed
under %PROGRAMFILES%\EPiServer\CMS\x.x.x.x\Application.

Configuration
The folder contains templates:
- log4net.Template.config
- Web.Template.config
The templates contains the default-settings for an EPiServer-site installation.
See HansKindberg-EPiServer-Build for more information.