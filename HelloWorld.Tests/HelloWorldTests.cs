using System.Diagnostics;
using System.Reflection;

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