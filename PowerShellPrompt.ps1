param($openPrompt, $folder)

#re-open powershell as an administrative prompt that won't exit
if (!$openPrompt) {
    $script = $MyInvocation.MyCommand.Path
    $folder = Split-Path -Parent $script
    Start-Process powershell -Verb runAs -ArgumentList "-noexit -file ""$script"" 1 ""$folder"""
    return
}

Set-Location $folder

function bc  { dotnet clean build.proj $args }
function bw  { dotnet watch msbuild build.proj /p:RunTests=true /p:FilterTest=TestCategory!=Slow /m:999 $args }
function ba  { dotnet msbuild build.proj /p:RunTests=true /m:999 $args }
function b   { dotnet msbuild build.proj /p:RunTests=true /p:FilterTest=TestCategory!=Slow /m:999 $args }
function br  { dotnet restore build.proj $args }

function btw ($test) { dotnet watch msbuild build.proj /p:RunTests=true /m:999 /p:FilterTest=FullyQualifiedName~$test $args }
function bt  ($test) { dotnet msbuild build.proj /p:RunTests=true /m:999 /p:FilterTest=FullyQualifiedName~$test $args }

type readme.md

echo ""

"bc  = $function:bc"
"bw  = $function:bw"
"ba  = $function:ba"
"b   = $function:b"
"br  = $function:br"
"btw = $function:btw"
"bt  = $function:bt"

echo ""
