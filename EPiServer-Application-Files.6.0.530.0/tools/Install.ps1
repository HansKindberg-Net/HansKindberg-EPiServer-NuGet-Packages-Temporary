param($rootPath, $toolsPath, $package, $project)

# Try to delete EPiServer-Application-Files-Content-Dummy.txt
if ($project) {
	$project.ProjectItems | ?{ $_.Name -eq "EPiServer-Application-Files-Content-Dummy.txt" } | %{ $_.Delete() }
}

# log4net.config
$log4netConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "log4net.config" }
$log4netCommonConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "log4net.Common.config" }
$log4netDevelopmentConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "log4net.Development.config" }
$log4netProductionConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "log4net.Production.config" }
$log4netTemplateConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "log4net.Template.config" }
$log4netTestConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "log4net.Test.config" }

if($log4netTemplateConfig -ne $null)
{
	$log4netTemplateConfig.Properties.Item("ItemType").Value = "None"

	if($log4netConfig -ne $null)
	{
		$log4netConfig.ProjectItems.AddFromFile($log4netTemplateConfig.Properties.Item("FullPath").Value)
	}
}

if($log4netCommonConfig -ne $null)
{
	$log4netCommonConfig.Properties.Item("ItemType").Value = "None"

	if($log4netTemplateConfig -ne $null)
	{
		$log4netTemplateConfig.ProjectItems.AddFromFile($log4netCommonConfig.Properties.Item("FullPath").Value)
	}
}

if($log4netDevelopmentConfig -ne $null)
{
	$log4netDevelopmentConfig.Properties.Item("ItemType").Value = "None"

	if($log4netCommonConfig -ne $null)
	{
		$log4netCommonConfig.ProjectItems.AddFromFile($log4netDevelopmentConfig.Properties.Item("FullPath").Value)
	}
}

if($log4netProductionConfig -ne $null)
{
	$log4netProductionConfig.Properties.Item("ItemType").Value = "None"

	if($log4netCommonConfig -ne $null)
	{
		$log4netCommonConfig.ProjectItems.AddFromFile($log4netProductionConfig.Properties.Item("FullPath").Value)
	}
}

if($log4netTestConfig -ne $null)
{
	$log4netTestConfig.Properties.Item("ItemType").Value = "None"

	if($log4netCommonConfig -ne $null)
	{
		$log4netCommonConfig.ProjectItems.AddFromFile($log4netTestConfig.Properties.Item("FullPath").Value)
	}
}

# Web.config
$webConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "Web.config" }
$webCommonConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "Web.Common.config" }
$webDevelopmentConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "Web.Development.config" }
$webProductionConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "Web.Production.config" }
$webTemplateConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "Web.Template.config" }
$webTestConfig = $project.ProjectItems | Where-Object { $_.Properties.Item("Filename").Value -eq "Web.Test.config" }

if($webTemplateConfig -ne $null)
{
	$webTemplateConfig.Properties.Item("ItemType").Value = "None"

	if($webConfig -ne $null)
	{
		$webConfig.ProjectItems.AddFromFile($webTemplateConfig.Properties.Item("FullPath").Value)
	}
}

if($webCommonConfig -ne $null)
{
	$webCommonConfig.Properties.Item("ItemType").Value = "None"

	if($webTemplateConfig -ne $null)
	{
		$webTemplateConfig.ProjectItems.AddFromFile($webCommonConfig.Properties.Item("FullPath").Value)
	}
}

if($webDevelopmentConfig -ne $null)
{
	$webDevelopmentConfig.Properties.Item("ItemType").Value = "None"

	if($webCommonConfig -ne $null)
	{
		$webCommonConfig.ProjectItems.AddFromFile($webDevelopmentConfig.Properties.Item("FullPath").Value)
	}
}

if($webProductionConfig -ne $null)
{
	$webProductionConfig.Properties.Item("ItemType").Value = "None"

	if($webCommonConfig -ne $null)
	{
		$webCommonConfig.ProjectItems.AddFromFile($webProductionConfig.Properties.Item("FullPath").Value)
	}
}

if($webTestConfig -ne $null)
{
	$webTestConfig.Properties.Item("ItemType").Value = "None"

	if($webCommonConfig -ne $null)
	{
		$webCommonConfig.ProjectItems.AddFromFile($webTestConfig.Properties.Item("FullPath").Value)
	}
}