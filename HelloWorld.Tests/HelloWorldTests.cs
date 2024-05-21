namespace HelloWorld.Tests;

public class HelloWorldTests
{
    [Fact]
    public void HelloWorld_Prints_HelloWorld()
    {
        // Arrange
        var writer = new StringWriter();
        Console.SetOut(writer);

        // Act
        Program.Main(default!);

        // Assert
        Assert.Equal("Hello, World!", writer.ToString().Trim());
    }

    [Fact]
    public void HelloWorld_Never_Wawaweewah()
    {
        // Arrange
        var writer = new StringWriter();
        Console.SetOut(writer);

        // Act
        Program.Main(default!);

        // Assert
        Assert.NotEqual("Wawaweewah!", writer.ToString().Trim());
    }
}