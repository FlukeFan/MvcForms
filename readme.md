
[![Build Status](https://ci.appveyor.com/api/projects/status/github/FlukeFan/MvcForms?svg=true)](https://ci.appveyor.com/project/FlukeFan/MvcForms) <pre>

MvcForms
========

Some utility classes to help create simple form input in an MVC application.


Building
========

To build, open CommandPrompt.bat as administrator, and type 'br' (to restore) then 'b'.

Build commands:

br                                      Restore dependencies (execute this first)
b                                       Dev-build
ba                                      Build all (including slow tests and coverage)
bw                                      Watch dev-build
bt [test]                               Run tests with filter Name~[test]
btw [test]                              Watch run tests with filter Name~[test]
bc                                      Clean the build outputs

b /t:setApiKey /p:apiKey=[key]          Set the api key
b /t:push                               Push packages to NuGet and publish them (setApiKey before running this)
