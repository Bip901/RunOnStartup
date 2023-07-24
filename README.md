# RunOnStartup

[![NuGet version](https://img.shields.io/nuget/v/RunOnStartup.svg)](https://www.nuget.org/packages/RunOnStartup/)

A cross-platform .NET library to register programs to run at computer startup.

# Usage

```csharp
using RunOnStartup;

// Define a unique key - this string will identify your program to unregister later
const string UNIQUE_NAME = "cool-executable-42dad1492ea57616";

// Register an executable to run whenever the current user signs in
RunOnStartup.Instance.Register(UNIQUE_NAME, @"/path/to/executable", allUsers: false);

// To unregister:
RunOnStartup.Instance.Unregister(UNIQUE_NAME, allUsers: false);
```
