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
 
Write-Host "Starting Database Deployment to $SqlServerName"

Write-Host "Executing $SqlFilePath against $SqlServerName\$SqlServerDatabaseName w/ $SqlServerUserName : $SqlServerUserPassword"

$results = @() 

#-InputFile "C:\ScriptFolder\TestSqlCmd.sql" `
Invoke-Sqlcmd -Query "select SERVERPROPERTY('ServerName') as Server, count(*) as 'DB Count' from sys.databases" `
	-ServerInstance $SqlServerName `
	-Database $SqlServerDatabaseName `
    -U $SqlServerUserName -P "$SqlServerUserPassword"

# Print Results
$results | Format-Table -autosize

Write-Host "Finished Database Deployment..."