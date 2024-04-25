using System;

namespace TestApp.Fundamentals;

// Testing Events
public class TemperatureSensor
{
    private const float HighTemperatureThreshold = 30.0f;

    public event EventHandler<TemperatureEventArgs> OnHighTemperatureAlert;
    private float _temperature;

    public void SetTemperature(float temperature)
    {
        _temperature = temperature;
        Console.WriteLine($"Current Temperature: {_temperature}°C");

        if (_temperature > HighTemperatureThreshold)
        {
            OnHighTemperatureAlert?.Invoke(this, new TemperatureEventArgs(_temperature));
        }
    }
}


public class TemperatureEventArgs : EventArgs
{
    public float Temperature { get; }

    public TemperatureEventArgs(float temperature)
    {
        Temperature = temperature;
    }
}