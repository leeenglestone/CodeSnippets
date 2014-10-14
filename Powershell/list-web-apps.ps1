
try{
Import-Module WebAdministration
Get-WebApplication
 
$webapps = Get-WebApplication
$list = @()
foreach ($webapp in get-childitem IIS:\AppPools\)
{
$name = "IIS:\AppPools\" + $webapp.name
$item = @{}
 
$item.WebAppName = $webapp.name
$item.Version = (Get-ItemProperty $name managedRuntimeVersion).Value
$item.State = (Get-WebAppPoolState -Name $webapp.name).Value
$item.UserIdentityType = $webapp.processModel.identityType
$item.Username = $webapp.processModel.userName
$item.Password = $webapp.processModel.password
 
$obj = New-Object PSObject -Property $item
$list += $obj
}
 
 #$list
 
#$list | Format-Table -a -Property "WebAppName", "Version", "State", "UserIdentityType", "Username", "Password"
 
}catch
{
$ExceptionMessage = "Error in Line: " + $_.Exception.Line + ". " + $_.Exception.GetType().FullName + ": " + $_.Exception.Message + " Stacktrace: " + $_.Exception.StackTrace
$ExceptionMessage
}

#$iispools = [ADSI]"IIS://UKCHAKITO007000/W3SVC/AppPools" | foreach {$_.children} | select Name, StartTime

$iispools = [ADSI]"IIS://UKCHAKITO007000/W3SVC/AppPools" | foreach {$_.children} | slect Name

$iispools