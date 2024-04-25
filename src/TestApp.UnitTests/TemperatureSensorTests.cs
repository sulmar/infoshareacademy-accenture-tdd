using TestApp.Fundamentals;
using FluentAssertions;

namespace TestApp.UnitTests;

[TestClass]
public class TemperatureSensorTests
{
    const float HighTemperatureThreshold = 30.0f;

    TemperatureSensor sut;

    [TestInitialize]
    public void Setup()
    {
        sut = new TemperatureSensor(HighTemperatureThreshold);
    }

    [TestMethod]
    public void SetTemperature_AboveHighTemperatureThreshold_ShouldRiseOnHighTemperatureAlert()
    {
        // Arrange        
        var monitor = sut.Monitor();

        // Act
        sut.SetTemperature(HighTemperatureThreshold + 0.01f);

        // Assert
        monitor.Should().Raise(nameof(TemperatureSensor.OnHighTemperatureAlert))
            .WithSender(sut);        
        
    }

    [TestMethod]
    public void SetTemperature_BelowHighTemperatureThreshold_ShouldNotRiseOnHighTemperatureAlert()
    {
        // Arrange
        var monitor = sut.Monitor();

        // Act
        sut.SetTemperature(HighTemperatureThreshold);

        // Assert
        monitor.Should().NotRaise(nameof(TemperatureSensor.OnHighTemperatureAlert));

    }

}
