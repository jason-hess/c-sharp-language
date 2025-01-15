# c-sharp-language

## Overview

`c-sharp-language` contains an overview of the features available in C# spanning versions 1.0 through
to 13.0 based on [The History of C#](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history).

## Structure

`CSharpLang.sln` contains a C# project file for each version of C#. Each C# project file will target a
specific version of C# using the [`<LangVersion>`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version#c-language-version-reference)
attribute in the `.csproj` file:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>8.0</LangVersion>
  </PropertyGroup>
</Project>
```

This downgrades the Roslyn compiler to the features of the C# language at that point in time. The .NET Target Framework is .NET 9.0 for all C# projects
in this solution - it's only the C# language features that shift from project to project.

## Tools

In order to build and run this solution you'll need the following:

- [.NET 9 SDK or higher](https://www.microsoft.com/net/download)

## Build

To build this solution:

1. `dotnet build` within the folder that contains `CSharpLang.sln`

OR

2. Load Visual Studio 2022 or higher and build `CSharpLang.sln`

## External Resources

- [SharpLab](https://sharplab.io/) is useful for seeing the compilation of particular C# language features
  which have been implemented as simple [syntactic sugar](https://en.wikipedia.org/wiki/Syntactic_sugar)
  implementing the new language feature in terms of older language features and then compiling
  into [IL](https://en.wikipedia.org/wiki/Common_Intermediate_Language).

## Tasks

- [x] Update NuGet Packages to Latest
- [x] Update C# Versions to include up to C# 13
- [ ] Implement Setup Script that installs all necessary tools.
