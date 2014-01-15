# HansKindberg-EPiServer-NuGet-Packages
Description here

## 1 Package-projects
Each project in this solution is a **Class Library**. The target framework for each project is **.NET Framework 4.5**. Even if the target framework does not matter keep the same framework when adding more projects.
When adding a new package project:
* Change the output-path from bin\Debug to ..\bin\Debug and from bin\Release to ..\bin\Release.
* Add the following lines to the post-build event:
<pre>
DEL "$(TargetPath)"
"$(SolutionDir).nuget\NuGet.exe" pack "$(ProjectDir)$(ProjectName).nuspec" -Properties Configuration=$(ConfigurationName) -Version "X.X.X.X"
</pre>
* Change **Properties** (project) -> **Build** -> **Advanced...** -> **Debug Info:** to **none** for all configurations **Debug/Release**. This is to avoid [Project-name].pdb files in the output directory.

To be able to build the packages you have to copy %PROGRAMFILES%\EPiServer to the solution root.

Reminder: Maybe we should delete the Debug configuration.

## 2 Configuration
Do not include the *.config in source-control.
Include *.Template.config and *.$(Configuration).config in source-control.

### 2.1 Web.config

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

### 2.2 Views\Web.config

	<Content Include="Views\Web.config" />
	<None Include="Views\Web.Debug.config">
		<DependentUpon>Views\Web.Template.config</DependentUpon>
	</None>
	<None Include="Views\Web.Release.config">
		<DependentUpon>Views\Web.Template.config</DependentUpon>
	</None>
	<None Include="Views\Web.Template.config">
		<DependentUpon>Views\Web.config</DependentUpon>
	</None>

### 2.3 log4net.config

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

## 3 log4net and log4net.config
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
				// I think you should do it here, in the static constructor. 
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



