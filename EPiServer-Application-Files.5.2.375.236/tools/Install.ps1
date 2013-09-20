param($rootPath, $toolsPath, $package, $project)

# Try to delete EPiServer-Application-Files-Content-Dummy.txt
if ($project) {
	$project.ProjectItems | ?{ $_.Name -eq "EPiServer-Application-Files-Content-Dummy.txt" } | %{ $_.Delete() }
}