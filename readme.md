
[![Build Status](https://ci.appveyor.com/api/projects/status/github/FlukeFan/MvcForms?svg=true)](https://ci.appveyor.com/project/FlukeFan/MvcForms) <pre>

MvcForms
========

Some utility classes to help create simple form input in a (pre core) MVC application.


Building
========

To build, open CommandPrompt.bat, and type 'b'.

Build commands:

b                               : build
b /t:clean                      : clean
b /t:RestorePackages            : Restore NuGet packages
b /t:setApiKey /p:apiKey=[key]  : set the api key
b /t:push                       : Push packages to NuGet and publish them (setApiKey before running this)
