using System;
namespace SistemasDistribuidos.Models
{
	public class HistoricalSearch
	{
        public int SensorID { get; set; }
        public DateTime Date { get; set; }
    }

    public class HistoricDataSensor
    {
        public int id { get; set; }
        public string area { get; set; }
        public Location localization { get; set; }
        public float mean { get; set; }
        public float p95 { get; set; }
        public float p99 { get; set; }
    }

    public class HistoricalSearchDTO
    {
        public List<HistoricDataSensor> sensors { get; set; }
        public int start { get; set; }
        public int end { get; set; }
    }
}

