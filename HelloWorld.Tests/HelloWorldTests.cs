using System.Diagnostics;
using System.Reflection;

namespace HelloWorld.Tests;

public class HelloWorldTests
{
    [Fact]
    public void HelloWorld_Prints_HelloWorld()
    {
        // Arrange
        var location = Assembly.GetExecutingAssembly().Location;
        var baseDir = location.Remove(
            location.IndexOf(
                $"{Path.DirectorySeparatorChar}HelloWorld.Tests{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}Debug{Path.DirectorySeparatorChar}net8.0{Path.DirectorySeparatorChar}HelloWorld.Tests.dll",
                StringComparison.InvariantCultureIgnoreCase));

        var projectPath = Path.Combine(baseDir, "HelloWorld", "HelloWorld.csproj");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo("dotnet", $"run --project \"{projectPath}\"")
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
        Assert.Equal("Hello, World!", output.Trim());
    }
}
