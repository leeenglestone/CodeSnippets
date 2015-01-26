Get-WmiObject -Authentication PacketPrivacy -Impersonation Impersonate
                       -ComputerName "UKCHAKITO007000" -namespace "root/MicrosoftIISv2"
                       -class IIsApplicationPool |
Select-Object -property @{N="Name";E={$name = $_.Name; $name.Split("/")[2] }} |
Format-Table