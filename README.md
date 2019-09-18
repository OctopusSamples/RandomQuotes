# Random Quotes
Fun random quotes web app.  App is built using Microsoft's ASP.NET core framework and it's very simple.

## Build

There is a Cake script for building the Random Quotes application in the root folder.

The Octo .NET Core CLI extension must be installed prior to running the build, using the command line

```shell
dotnet tool install Octopus.DotNet.Cli --tool-path .\tools\
```

Running locally will place the resulting Zip package into a folder called `LocalPackages`, as a sibling to the repo's folder.