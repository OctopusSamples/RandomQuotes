#
# DeployDatabase.ps1
#

Param (
	[Parameter(Mandatory=$True)]
	[string]$SqlFilePath,
	[Parameter(Mandatory=$True)]
	[string]$SqlServerName,
	[Parameter(Mandatory=$True)]
	[string]$SqlServerUserName,
	[Parameter(Mandatory=$True)]
	[string]$SqlServerUserPassword
)
 
Write-Host "Starting Database Deployment to $SqlServerName"

#$results = @() 

#-InputFile "C:\ScriptFolder\TestSqlCmd.sql" `
#Invoke-Sqlcmd -Query "select SERVERPROPERTY('ServerName') as Server, count(*) as 'DB Count' from sys.databases" `
#	-ServerInstance $instanceName `
#    -U $db.user.name -P $db.user.password

#$results += Invoke-Sqlcmd `
 
# Print Results
#$results | Format-Table -autosize

Write-Host "Finished Database Deployment..."