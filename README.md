# HansKindberg-EPiServer-NuGet-Packages
This repository contains a Visual Studio solution for building NuGet-packages for EPiServer.
- **HansKindberg-EPiServer-Binaries** (all the EPiServer binaries)
- **HansKindberg-Application-Files** (all the application-files, Admin, Edit etc.)
- **HansKindberg-Build** (build scripts/targets for building EPiServer solutions/projects)

## 1 Important
**EPiServer** is a product requiring a license. Its important to not publish these packages in public. Instead keep the packages in your own NuGet-sources.

## 2 Version mappings
Write about the branches

- **HansKindberg-EPiServer-*** packages **5.2.1.*** mappes to **EPiServer CMS 5 R2 SP1** (**5.2.375.133**)
- **HansKindberg-EPiServer-*** packages **5.2.2.*** mappes to **EPiServer CMS 5 R2 SP2** (**5.2.375.236**)
- **HansKindberg-EPiServer-*** packages **6.0.0.*** mappes to **EPiServer CMS 6.0** (**6.0.530.0**)
- **HansKindberg-EPiServer-*** packages **6.1.0.*** mappes to **EPiServer CMS 6 R2** (**6.1.379.0**)
- **HansKindberg-EPiServer-*** packages **7.0.0.*** mappes to **EPiServer CMS 7** (**7.0.586.1**)
- **how should we do with 7.1**
- **HansKindberg-EPiServer-*** packages **7.5.0.*** mappes to **EPiServer CMS 7.5** (**7.5.394.2**)

## 3 Build
To build the packages you need the EPiServer files, normally installed under %PROGRAMFILES%\EPiServer. Basically you copy that directory to the root of this solution when you want to build the packages.
When building all the package content is copied to the **packages** directory under the solution root. The **packages** directory is the default NuGet-packages directory. The packages are
only built when building a release. This is to reduce build time when building for the test application **HansKindberg.EPiServer.NuGet.Packages.Tests.WebApplication**.

## 4 Package-projects
Each package-project in this solution is a **Class Library**. The target framework for each project is **.NET Framework 4.5**. Even if the target framework does not matter keep the same framework when adding more projects.
When adding a new package project:
* Change the output-path from bin\Debug to ..\Package-output\Debug and from bin\Release to ..\Package-output\Release.
* Change **Properties** (project) -> **Build** -> **Advanced...** -> **Debug Info:** to **none** for all configurations **Debug/Release**. This is to avoid [Project-name].pdb files in the output directory.

## 5 Packages
### 5.1 HansKindberg-EPiServer-Binaries
This package contains all the assemblies/dll's needed to run an EPiServer web-application. As far as possible all dependant assemblies are added as NuGet-dependencies.

### 5.2 HansKindberg-EPiServer-Application-Files
This package contains all the EPiServer application-files (admin, edit etc.) and *.config templates. The package
does not add any content to the project. The content in the package can be copied to the project, by using
pre- and post-build events, or mapped to, by using virtual path providers.

#### 5.2.1 Application
The folder contains the EPiServer application files that normally is installed
under %PROGRAMFILES%\EPiServer\CMS\x.x.x.x\Application.

#### 5.2.2 Configuration
The folder contains templates:
- log4net.Template.config
- Web.Template.config
The templates contains the default-settings for an EPiServer-site installation.
See HansKindberg-EPiServer-Build for more information.

### 5.3 HansKindberg-EPiServer-Build
This package contains build-targets. This package has a dependency to HansKindberg-EPiServer-Application-Files and through that also to HansKindberg-EPiServer-Binaries.

#### 5.3.1 Regarding HansKindberg-EPiServer-Application-Files:
The package contains build scripts/targets that transforms the config-files.
1.
- package\EPiServer-Application-Files.x.x.x.x\Configuration\Web.Template.config with Web.Common.config (transforms) to Web.Template.config
- package\EPiServer-Application-Files.x.x.x.x\Configuration\log4net.Template.config with log4net.Common.config (transforms) to log4net.Template.config
2.
- log4net.Template.config with log4net.$(Configuration).config (transforms) to log4net.config
- Web.Template.config with Web.$(Configuration).config (transforms) to Web.config
- Views\Web.Template.config with Views\Web.$(Configuration).config (transforms) to Views\Web.config
- Any Custom.Template.config with Custom.$(Configuration).config (transforms) to Custom.config

##### 5.3.1.1 Web.config

	<Content Include="Web.config" />
	<None Include="Web.Common.config">
		<DependentUpon>Web.Template.config</DependentUpon>
	</None>
	<None Include="Web.Debug.config">
		<DependentUpon>Web.Common.config</DependentUpon>
	</None>
	<None Include="Web.Release.config">
		<DependentUpon>Web.Common.config</DependentUpon>
	</None>
	<None Include="Web.Template.config">
		<DependentUpon>Web.config</DependentUpon>
	</None>

**Do not include the Web.config and Web.Template.config in source-control.**

##### 5.3.1.2 log4net.config

	<Content Include="log4net.config" />
	<None Include="log4net.Common.config">
		<DependentUpon>log4net.Template.config</DependentUpon>
	</None>
	<None Include="log4net.Debug.config">
		<DependentUpon>log4net.Common.config</DependentUpon>
	</None>
	<None Include="log4net.Release.config">
		<DependentUpon>log4net.Common.config</DependentUpon>
	</None>
	<None Include="log4net.Template.config">
		<DependentUpon>log4net.config</DependentUpon>
	</None>

**Do not include the log4net.config and log4net.Template.config in source-control.**

##### 5.3.1.3 Views\Web.config

	<Content Include="Views\Web.config" />
	<None Include="Views\Web.Debug.config">
		<DependentUpon>Web.Template.config</DependentUpon>
	</None>
	<None Include="Views\Web.Release.config">
		<DependentUpon>Web.Template.config</DependentUpon>
	</None>
	<None Include="Views\Web.Template.config">
		<DependentUpon>Web.config</DependentUpon>
	</None>

**Do not include the Views\Web.config in source-control.**

##### 5.3.1.4 log4net and log4net.config
The EPiServer.dll configures log4net:

	[assembly: log4net.Config.XmlConfigurator(ConfigFile="episerverlog.config", Watch=true)]

So you can not use this in the application assembly:

	[assembly: log4net.Config.XmlConfigurator(ConfigFile="log4net.config", Watch=true)]

Because it will only work if not log4net is already configured, and it is, by the EPiServer.dll.

So you have to do it in your Global.asax.cs:

	namespace Company.EPiServer.WebApplication
	{
		public class Global : EPiServer.Global
		{
			#region Constructors

			static Global()
			{
				// Check if log4net is configured, if not configure it with "log4net.config".
				if(!log4net.LogManager.GetRepository().Configured)
					log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
			}

			#endregion

			#region Methods

			protected void Application_Start()
			{
				// Now logging should work.
				log4net.LogManager.GetLogger(typeof(Global)).Info("Application start");
			}

			#endregion
		}
	}

#### 5.3.2 Regarding HansKindberg-EPiServer-Binaries
This package contains build scripts/targets that copies all the binaries to the output-directory (bin) on build, even the NuGet-dependant assemblies/dll's. It only copies a dll if
it not already exist or if it is newer.