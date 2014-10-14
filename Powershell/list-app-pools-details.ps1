$server_name = Read-Host 'SERVER NAME:'

foreach($vm in $server_name)
{

	$iis = Get-WmiObject -Namespace root\MicrosoftIISv2 -Class IIsApplicationPoolSetting -ComputerName $server_name -Authentication PacketPrivacy

	<#
	$iis

	$i = $iis.Count

	for ($k = 0; $k -lt $i; $k++)
	{
		$name = $iis[$k].Name
	}
	#>
} 