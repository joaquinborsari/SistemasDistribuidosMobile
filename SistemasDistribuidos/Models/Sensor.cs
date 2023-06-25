using System;
namespace SistemasDistribuidos.Models
{
	public class Sensor
	{
        public int SensorID { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }

    public class SensorsDTOList
    {
        public List<Sensors> sensors { get; set; }
    }

    public class Sensors
    {
        public int id { get; set; }
        public string area { get; set; }
        public Localization localization { get; set; }
        public double currentFlow { get; set; }
    }

    public class Localization
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}

