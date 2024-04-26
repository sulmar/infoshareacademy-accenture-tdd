using System;
using System.Drawing;
using System.IO;
using System.Text.Json;

namespace TestApp.Mocking
{
    public interface IFileService
    {
        string ReadAllText(string path);
    }

    public class RealFileService : IFileService
    {
        public string ReadAllText(string path)
        {
            return File.ReadAllText("tracking.txt");
        }
    }

    public class TrackingService
    {
        private readonly IFileService _fileService;

        public TrackingService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public TrackingService()
            : this(new RealFileService())
        {
        }

        public Location Get()
        {
            string json = _fileService.ReadAllText("tracking.txt");

            try
            {
                Location location = JsonSerializer.Deserialize<Location>(json);


                if (location == null)
                    throw new ApplicationException("Error parsing the location");

                return location;

            }
            catch(JsonException e)
            {
                throw new FormatException();
            }
        }
    }


    public class Location
    {
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude} {Longitude}";
        }

    }
}
