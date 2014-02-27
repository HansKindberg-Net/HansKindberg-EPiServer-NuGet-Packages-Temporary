# HansKindberg-EPiServer-NuGet-Packages
This repository contains a Visual Studio solution for building NuGet-packages for EPiServer. In EPiServer CMS 7.5 you can get the most files for your EPiServer project/solution from [**EPiServer NuGet**](https://nuget.episerver.com/). So now some of the previous packages in this solution is not needed anymore, and thats nice.
- ~~**HansKindberg-EPiServer-Binaries** (all the EPiServer binaries)~~ - *not needed anymore, you get it from [**EPiServer NuGet**](https://nuget.episerver.com/)*
- ~~**HansKindberg-EPiServer-Application-Files** (all the application-files, Admin, Edit etc.)~~ - *not needed anymore, you get it from [**EPiServer NuGet**](https://nuget.episerver.com/)*
- **HansKindberg-EPiServer-Build** (build scripts/targets for building EPiServer solutions/projects)

The most important packages (my opinion) from [**EPiServer NuGet**](https://nuget.episerver.com/)

- EPiServer.CMS.Core
- EPiServer.CMS.UI
- EPiServer.Framework
- EPiServer.Search

## 1 Version mappings
Write about the branches

- **HansKindberg-EPiServer-*** packages **5.2.1.*** mappes to **EPiServer CMS 5 R2 SP1** (**5.2.375.133**)
- **HansKindberg-EPiServer-*** packages **5.2.2.*** mappes to **EPiServer CMS 5 R2 SP2** (**5.2.375.236**)
- **HansKindberg-EPiServer-*** packages **6.0.0.*** mappes to **EPiServer CMS 6.0** (**6.0.530.0**)
- **HansKindberg-EPiServer-*** packages **6.1.0.*** mappes to **EPiServer CMS 6 R2** (**6.1.379.0**)
- **HansKindberg-EPiServer-*** packages **7.0.0.*** mappes to **EPiServer CMS 7** (**7.0.586.1**)
- **how should we do with 7.1**
- **HansKindberg-EPiServer-*** packages **7.5.0.*** mappes to **EPiServer CMS 7.5** (**7.5.394.2**)

## 2 Package-projects
Each package-project in this solution is a **Class Library**. The target framework for each project is **.NET Framework 4.5**. Even if the target framework does not matter keep the same framework when adding more projects.
When adding a new package project:
* Change the output-path from bin\Debug to ..\Package-output\Debug and from bin\Release to ..\Package-output\Release.
* Change **Properties** (project) -> **Build** -> **Advanced...** -> **Debug Info:** to **none** for all configurations **Debug/Release**. This is to avoid [Project-name].pdb files in the output directory.

## 3 Packages

### 3.1 HansKindberg-EPiServer-Build
This package contains build-targets. The build-targets are:

- Configuration transformations
- Copy files to the target-directory

The functionality/base functionality for these build-targets are from [**HansKindberg-Build**](https://github.com/HansKindberg-Net/HansKindberg-Build). The build-targets from [**HansKindberg-Build-Files**](https://github.com/HansKindberg-Net/HansKindberg-Build-Temporary#2-hanskindberg-build-files) are transformed into the package.

#### 3.1.1 Configuration transformations

The package contains a **Configuration** directory that contains templates:
- log4net.Template.config
- Web.Template.config

The templates contains the default-settings for an EPiServer-site installation.

##### 3.1.1.1 Howto get the content for the templates

###### 3.1.1.1.1 log4net.Template.config

- copy the content from **EPiServer\CMS\7.5.394.2\Application\EPiServerLog.config** to **/Configuration/log4net.Template.config**.

###### 3.1.1.1.1 Web.Template.config
Install a site, **Install site and SQL Server database**, and set the relative path to the EPiServer User Interface to **/EPiServer/**. Then merge **connectionStrings.config**, **episerver.config**, **EPiServerFramework.config** and **Web.config** from the site root directory to **/Configuration/Web.Template.config**.

Search and replace (whole word and case-sensitive):
- replace **admin** with **Admin**
- replace **/modules/** with **/Modules/**
- replace **modulesbin** with **ModulesBin**
- replace **True** with **true**
- replace **util** with **Util**

Clear:
- /coonnectionStrings[@connectionString]
- /episerver.search/namedIndexingServices/services/add[@baseUri]

Add a location element with path set to "." around the root system.web and root system.webServer.

	<location path=".">
		<system.web>
			...
		</system.web>
		<system.webServer>
			...
		</system.webServer>
	</location>

##### 3.1.1.2 How the package works

1.
- packages\HansKindberg-EPiServer-Build.x.x.x.x\Configuration\log4net.Template.config is transformed with log4net.Common.config (transforms) to log4net.Template.config
- packages\HansKindberg-EPiServer-Build.x.x.x.x\Configuration\Web.Template.config is transformed with Web.Common.config (transforms) to Web.Template.config

2.
- log4net.Template.config is transformed with log4net.$(Configuration).config (transforms) to log4net.config
- Web.Template.config is transformed with Web.$(Configuration).config (transforms) to Web.config
- Views\Web.Template.config is transformed with Views\Web.$(Configuration).config (transforms) to Views\Web.config
- Any Custom.Template.config is transformed with Custom.$(Configuration).config (transforms) to Custom.config

###### 3.1.1.2.1 Web.config

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

###### 3.1.1.2.2 log4net.config

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

###### 3.1.1.2.3 Views\Web.config

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

###### 3.1.1.2.4 log4net and log4net.config
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

#### 3.1.2 Copy files to the target-directory

See [**HansKindberg.EPiServer.NuGet.Packages.Tests.WebApplication**](/HansKindberg.EPiServer.NuGet.Packages.Tests.WebApplication/Build/Build.props) for an example howto do it.