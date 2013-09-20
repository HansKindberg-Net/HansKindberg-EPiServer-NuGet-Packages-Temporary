EPiServer-Application-Files
---------------------------
This package contains all the EPiServer application-files (admin, edit etc.). The package
does not add any content to the project. The content in the package can be copied to the
project, by using pre- and post-build events, or mapped to, by using virtual path providers.

Application
The folder contains the EPiServer application files that normally is installed
under %PROGRAMFILES%\EPiServer\CMS\x.x.x.x\Application.

Configuration
The folder contains files for configuration-transformations...(need more description here)

Content
Just a dummy-file so the package install. The file is removed by "tools\Install.ps1".
A NuGet-package needs to contain content or lib to install as a project-level package.

Tools
Just containing Install.ps1 that removes the content dummy-file.