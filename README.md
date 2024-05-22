[![.NET](https://github.com/iPlexor/Hello-GitHub/actions/workflows/dotnet.yml/badge.svg)](https://github.com/iPlexor/Hello-GitHub/actions/workflows/dotnet.yml)
 
 # Hello World

Technically its the default project template for a C# Console App, ostensibly a 1 line program thanks to Top Level Statements. 

**Starting Template & Finished App**

![image](attachments/consoleapp.png)

## Problem 1: Making it testable

So to make it testable, we add a test project.

1. Add an `XUnit` test project.
1. Add a reference the `HelloWorld` project.
1. Add this test:

```csharp
namespace HelloWorld.Tests;

public class HelloWorldTests
{
    [Fact]
    public void HelloWorld_Prints_HelloWorldEx()
    {
        // Arrange
        var location = Assembly.GetExecutingAssembly().Location;
        var baseDir = location.Remove(
        location.IndexOf(
            "\\HelloWorld.Tests\\bin\\Debug\\net8.0\\HelloWorld.Tests.dll", StringComparison.InvariantCultureIgnoreCase));
        var process = new Process
        {
            StartInfo = new ProcessStartInfo("dotnet", $"run --project {baseDir}\\HelloWorld\\HelloWorld.csproj")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        // Act
        process.Start();
        process.WaitForExit();

        var output = process.StandardOutput.ReadToEnd();

        // Assert
        Assert.Equal("Hello, World!\r\n", output);
    }
}
```

`Pro Tip:` If you're doing TDD, remember to _pretend_ you wrote the failing tests _before_ you wrote the code to make them pass.

## Problem 2: Measuring Code Coverage

The easy, but very expensive way to do this is to buy a Visual Studio Enterprise license and switch Code Coverage on. If you prefer to save a few grand a year, you're not going to do that.

Instead, we'll use `Coverlet` to generate code coverage metrics when we run our tests and use `ReportGenerator` to show us the results.

1. Add the `Coverlet.MSBuild` NuGet package to the both projects.

   ```powershell
   dotnet add package coverlet.msbuild
   ```

1. Install the `ReportGenerator` tool:

   ```powershell
   dotnet tool install -g dotnet-reportgenerator-globaltool
   ```

1. Run the tests and collect coverage using the dotnet test command with an absolute path.

   ```powershell
   dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput="$(pwd)/coverage/"
   ```

1. Run the `ReportGenerator` tool:

   ```powershell
   dotnet reportgenerator "-reports:HelloWorld.Tests/coverage.opencover.xml" "-targetdir:coveragereport" "-reporttypes:Html"
   ```

Amazing, 💯% code coverage!

`Pro Tip:` If it doesn't generate `coverage.opencover.xml`, you've successfully reproduced my results. Somethings broken and needs fixing. Good luck!

Congratulations, you're a real Software Engineer now!

## Conclusion

Technically, `Main` is the entrypoint of `Program`, so testing it is technically an End-to-End test anyway! 

We might as well test the whole process, right? 

![image](attachments/sparklemotion.gif)

Well, no. That's not how we do things. We test the smallest unit of code that we can, and we test it in isolation.

This is a silly example.


