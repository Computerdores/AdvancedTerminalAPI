﻿# Make sure that the environment variable ProfileName is set to the name of the Thunderstore profile you want to use
Copy-Item "AdvancedTerminalAPI\bin\AdvancedTerminalAPI.dll" -Destination "$Env:appdata\Thunderstore Mod Manager\DataFolder\LethalCompany\profiles\$Env:ProfileName\BepInEx\plugins\"
&"C:/Program Files (x86)/Steam/steam.exe" -applaunch 1966720 --doorstop-enable true --doorstop-target "$Env:appdata\Thunderstore Mod Manager\DataFolder\LethalCompany\profiles\$Env:ProfileName\BepInEx\core\BepInEx.Preloader.dll"