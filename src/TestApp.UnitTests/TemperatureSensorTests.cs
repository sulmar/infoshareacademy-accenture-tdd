using TestApp.Fundamentals;

namespace TestApp.UnitTests;

[TestClass]
public class TemperatureSensorTests
{
    const float HighTemperatureThreshold = 30.0f;

    TemperatureSensor sut;
    bool isTriggered = false;

    [TestInitialize]
    public void Setup()
    {
        sut = new TemperatureSensor(HighTemperatureThreshold);
        sut.OnHighTemperatureAlert += (sender, args) => { isTriggered = true; };
    }

    [TestMethod]
    public void SetTemperature_AboveHighTemperatureThreshold_ShouldRiseOnHighTemperatureAlert()
    {
        // Arrange

        // Act
        sut.SetTemperature(HighTemperatureThreshold + 0.01f);

        // Assert
        Assert.IsTrue(isTriggered);
        
    }

    [TestMethod]
    public void SetTemperature_BelowHighTemperatureThreshold_ShouldNotRiseOnHighTemperatureAlert()
    {
        // Arrange

        // Act
        sut.SetTemperature(HighTemperatureThreshold);

        // Assert
        Assert.IsFalse(isTriggered);

    }

}
