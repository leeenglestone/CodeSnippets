$appPools = Get-childItem IIS:\AppPools
# foreach ($appPool in $appPools)
# {
    # $path = "IIS:\AppPools\$($appPool.Name)"
    # $appPool.name
	
	# $appPool
	
    # #Set-ItemProperty $path -Name recycling.disallowRotationOnConfigChange -value True
    # #Set-ItemProperty $path -Name recycling.disallowOverlappingRotation -value True
# }


function get-aspnetwp([string]$name="*")
{
   $list = get-process w3wp
   foreach($p in $list)
   { 
      $filter = "Handle='" + $p.Id + "'"
      $wmip = get-WmiObject Win32_Process -filter $filter 

      if($wmip.CommandLine -match "-ap `"(.+)`"")
      {
         $appName = $matches[1]
         $p | add-member NoteProperty AppPoolName $appName
      }
   }
   $list | where { $_.AppPoolName -like $name }
}