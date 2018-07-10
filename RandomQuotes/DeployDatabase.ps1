#
# DeployDatabase.ps1
#

Param (
	[Parameter(Mandatory=$True)]
	[string]$SqlFilePath,
	[Parameter(Mandatory=$True)]
	[string]$SqlServerName,
	[Parameter(Mandatory=$True)]
	[string]$SqlServerDatabaseName,
	[Parameter(Mandatory=$True)]
	[string]$SqlServerUserName,
	[Parameter(Mandatory=$True)]
	[string]$SqlServerUserPassword
)
 
Write-Host "Starting Database Deployment to $SqlServerName executing $SqlFilePath on $SqlServerDatabaseName..."

$results = @() 

Invoke-Sqlcmd -InputFile $SqlFilePath `
	-ServerInstance $SqlServerName `
	-Database $SqlServerDatabaseName `
    -U $SqlServerUserName -P $SqlServerUserPassword

# Print Results
$results | Format-Table -autosize

Write-Host "Finished Database Deployment."