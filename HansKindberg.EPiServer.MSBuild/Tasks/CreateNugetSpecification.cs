using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO.Abstractions;
using Microsoft.Build.Framework;

namespace HansKindberg.EPiServer.MSBuild.Tasks
{
	public abstract class CreateNuGetSpecification : LogTask
	{




/*
	<ItemGroup Condition="@(ApplicationFilesPackageVersion) == ''">
		<ApplicationFilesPackageVersion Include="5.2.1.0">
			<BinariesPackageVersion>5.2.1.0</BinariesPackageVersion>
			<ConfigurationVersion>5.2.1.0</ConfigurationVersion>
			<EPiServerVersion>5.2.375.133</EPiServerVersion>
			<NuspecTransformers>.nuspec\Files\Application-Files-5to7.0-Files-Transform.nuspec</NuspecTransformers>
		</ApplicationFilesPackageVersion>
	</ItemGroup>
	<ItemGroup Condition="@(BinariesPackageVersion) == ''">
		<BinariesPackageVersion Include="6.0.0.0">
			<EPiServerVersion>6.0.530.0</EPiServerVersion>
			<EPiServerFrameworkVersion>6.0.318.113</EPiServerFrameworkVersion>
			<NuspecTransformers>.nuspec\Dependencies\Binaries-5to6-Dependencies-Transform.nuspec</NuspecTransformers>
			<NuspecTransformers>.nuspec\Files\Binaries-6-Files-Transform.nuspec</NuspecTransformers>
			<NuspecTransformers>.nuspec\References\Binaries-6-References-Transform.nuspec</NuspecTransformers>
		</BinariesPackageVersion>
	</ItemGroup>
	<ItemGroup Condition="@(BuildPackageVersion) == ''">
		<BuildPackageVersion Include="5.2.1.0">
			<ApplicationFilesPackageVersion>5.2.1.0</ApplicationFilesPackageVersion>
			<BinariesPackageVersion>5.2.1.0</BinariesPackageVersion>
			<PackageBinariesDirectories>lib\net20\**\*.dll</PackageBinariesDirectories>
			<EPiServerVersion>5.2.375.133</EPiServerVersion>
			<ReferencedBinariesDirectories>log4net.1.2.10\lib\2.0\**\*.dll;Microsoft.Web.Services3.3.0.0.0\lib\net20\**\*.dll</ReferencedBinariesDirectories>
		</BuildPackageVersion>
	</ItemGroup>
		*/











		#region Fields

		private readonly IFileSystem _fileSystem;
		private string _outputDirectory;
		private string _epiServerDirectory;
		private const string _epiServerVersionMetadataName = "EPiServerVersion";
		private const string _nuspecTransformersMetadataName = "NuspecTransformers";

		
		

		#endregion




		#region Constructors



		protected CreateNuGetSpecification(IFileSystem fileSystem)
		{
			if(fileSystem == null)
				throw new ArgumentNullException("fileSystem");

			this._fileSystem = fileSystem;
		}

		#endregion


		protected internal virtual string GetEPiServerVersion(ITaskItem packageVersion)
		{
			if(packageVersion == null)
				throw new ArgumentNullException("packageVersion");

			return packageVersion.GetMetadata(_epiServerVersionMetadataName);
		}

		protected internal virtual string GetNuspecTransformers(ITaskItem packageVersion)
		{
			if (packageVersion == null)
				throw new ArgumentNullException("packageVersion");

			return packageVersion.GetMetadata(_nuspecTransformersMetadataName);
		}


		#region Properties

		[Required]
		public virtual string DefaultNuGetSpecificationTransformers { get; set; }

		[Required]
		public virtual string EPiServerDirectory
		{
			get { return this._epiServerDirectory; }
			set
			{
				if (!this.FileSystem.Directory.Exists(value))
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The directory '{0}' does not exist", value), "value");

				this._epiServerDirectory = value;
			}
		}

		protected internal virtual IFileSystem FileSystem
		{
			get { return this._fileSystem; }
		}

		[Required]
		public virtual string NuGetSpecificationTemplate { get; set; }

		[Required]
		public virtual string OutputDirectory
		{
			get { return this._outputDirectory; }
			set
			{
				if(!this.FileSystem.Directory.Exists(value))
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The directory '{0}' does not exist", value), "value");

				this._outputDirectory = value;
			}
		}

		[Required]
		public virtual string PackageName { get; set; }

		[Required]
		public virtual ITaskItem[] PackageVersion { get; set; }

		#endregion

		protected internal virtual bool EPiServerCmsDirectoryExists(ITaskItem packageVersion)
		{
			return this.FileSystem.Directory.Exists(this.EPiServerDirectory + @"CMS\" + this.GetEPiServerVersion(packageVersion));
		}

		#region Methods

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MetadataName"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EPiServerVersion"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NuspecTransformers"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "PackageVersion"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "HansKindberg.EPiServer.MSBuild.Tasks.LogTask.LogMessage(System.String)")]
		public override bool Execute()
		{
			foreach(var packageVersion in this.PackageVersion)
			{
				string EPiServerVersion = this.GetEPiServerVersion(packageVersion);

				if()

				this.LogMessage("Identity = " + packageVersion.ItemSpec + " (EPiServerVersion=" + packageVersion.GetMetadata("EPiServerVersion") + ")");


				//packageVersion.MetadataNames.ItemSpec

				//this.LogMessage("PackageVersion = " + packageVersion.ItemSpec + ", EPiServerVersion = " + packageVersion.GetMetadata("EPiServerVersion") + ", NuspecTransformers = " + packageVersion.GetMetadata("NuspecTransformers"));
				
			}

			//if(!this.FileSystem.Directory.Exists(this.EPiServerCmsDirectory))
			//{
			//	this.LogThatSpecificationWillNotBeCreated(string.Format(CultureInfo.InvariantCulture, "The EPiServer CMS dirctory \"{0}\" does not exist.", this.EPiServerCmsDirectory));
			//	return false;
			//}

			//if (!Directory.Exists(this.EPiServerDirectory))
			//	this.Log.LogMessage(messageImportance, "{0}", new object[] { messagePrefix + "The '" });

			////this.Log.LogMessage(messageImportance, "{0}", new object[] { messagePrefix + "Clearing directory '" + this.DirectoryPath + "'." });

			return true;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "HansKindberg.EPiServer.MSBuild.Tasks.LogTask.LogMessage(System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "nuspec")]
		protected internal virtual void LogThatSpecificationWillNotBeCreated(string cause)
		{
			this.LogMessage(string.Format(CultureInfo.InvariantCulture, "The \"{0}.{1}.nuspec\" will not be created. {2}", this.PackageName, this.PackageVersion, cause));
		}

		#endregion
	}

//<?xml version="1.0" encoding="utf-8"?>
//<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
//	<UsingTask
//		TaskName="CreateNuspec"
//		TaskFactory="CodeTaskFactory"
//		AssemblyFile="$(MSBuildToolsPath)\Microsoft.Build.Tasks.v4.0.dll"
//	>
//		<ParameterGroup>
//			<OutputDirectory ParameterType="System.String" Required="true" />
//			<PackageName ParameterType="System.String" Required="true" />
//			<PackageVersion ParameterType="Microsoft.Build.Framework.ITaskItem[]" Required="true" />
//			<EPiServerDirectory ParameterType="System.String" Required="true" />
//			<EPiServerFrameworkRequired ParameterType="System.Boolean" Required="true" />
//			<NuspecTemplate ParameterType="System.String" Required="true" />
//			<DefaultNuspecTransformers ParameterType="System.String" Required="true" />
//			<AdditionalNuspecTransformers ParameterType="System.String" Required="false" />
//			<LogImportance ParameterType="System.String" Required="false" />
//			<TargetName ParameterType="System.String" Required="false" />
//		</ParameterGroup>
//		<Task>
//			<Code Type="Class" Language="cs">
//				<![CDATA[
//					public class CreateNuspec : Microsoft.Build.Utilities.Task
//					{
//						public override bool Execute()
//						{
//							MessageImportance messageImportance = MessageImportance.Normal;
//							if(!string.IsNullOrEmpty(this.LogImportance))
//							{
//								try
//								{
//									messageImportance = (MessageImportance)Enum.Parse(typeof(MessageImportance), this.LogImportance, true);
//								}
//								catch (ArgumentException)
//								{
//									throw new FormatException(string.Format("\"{0}\" is an invalid value for the \"LogImportance\" parameter. Valid values are: High, Normal and Low.", this.LogImportance));
//								}
//							}
//							string messagePrefix = !string.IsNullOrEmpty(this.TargetName) ? "Target '" + this.TargetName + "': " : string.Empty;
//							if(!Directory.Exists(this.OutputDirectory))
//								throw new ArgumentException(string.Format("The directory '{0}' does not exist", this.OutputDirectory), "OutputDirectory");
//							if(!Directory.Exists(this.EPiServerDirectory))
//								this.Log.LogMessage(messageImportance, "{0}", new object[] { messagePrefix + "The '" });
//							//this.Log.LogMessage(messageImportance, "{0}", new object[] { messagePrefix + "Clearing directory '" + this.DirectoryPath + "'." });
//						}					
//					}
//				]]>
//			</Code>
//		</Task>
//	</UsingTask>
//</Project>
}