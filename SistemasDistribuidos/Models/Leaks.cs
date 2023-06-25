using System;
namespace SistemasDistribuidos.Models
{
	public class Leaks
	{
        public int SensorID { get; set; }
        public DateTime Date { get; set; }
        public int Probability { get; set; }
    }

    public class Leak
    {
        public int id { get; set; }
        public string area { get; set; }
        public Localization localization { get; set; }
        public double leakProbability { get; set; }
        public long timestamp { get; set; }

        
        public DateTime timestampDate
        {
            get
            {
                if (timestamp != 0)
                {
                    return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
                }
                else
                {
                    return DateTime.Now;
                }
                
            }
        }
        
    }

    public class LeakDTO
    {
        public List<Leak> leaks { get; set; }
    }
}

