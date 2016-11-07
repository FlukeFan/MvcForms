@CD /D "%~dp0"
@title MvcForms Command Prompt
@SET PATH=C:\Program Files (x86)\MSBuild\14.0\Bin\;%PATH%
@doskey b=msbuild $* MvcForms.proj
type readme.txt
%comspec%
