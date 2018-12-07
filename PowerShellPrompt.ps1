param($openPrompt, $folder)

#re-open powershell as an administrative prompt that won't exit
if (!$openPrompt) {
    $script = $MyInvocation.MyCommand.Path
    $folder = Split-Path -Parent $script
    Start-Process powershell -Verb runAs -ArgumentList "-noexit -file ""$script"" 1 ""$folder"""
    return
}

Set-Location $folder
