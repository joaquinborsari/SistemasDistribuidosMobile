using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Maps;
using SistemasDistribuidos.Helpers;
using SistemasDistribuidos.Models;
using static System.Net.Mime.MediaTypeNames;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace SistemasDistribuidos.ViewModels
{
	public class HomeViewModel : INotifyPropertyChanged
    {

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private ObservableCollection<HistoricalSearch> historicalSearchList;

        public ObservableCollection<HistoricalSearch> HistoricalSearchList
        {
            get => historicalSearchList; set
            {
                historicalSearchList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Leaks> leaksList;

        public ObservableCollection<Leaks> LeaksList
        {
            get => leaksList; set
            {
                leaksList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Leak> leaList = new ObservableCollection<Leak>();

        public ObservableCollection<Leak> LeaList
        {
            get => leaList; set
            {
                leaList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<HistoricDataSensor> hsList = new ObservableCollection<HistoricDataSensor>();

        public ObservableCollection<HistoricDataSensor> HsList
        {
            get => hsList; set
            {
                hsList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Pin> pinSensors = new ObservableCollection<Pin>();

        public ObservableCollection<Pin> PinSensors
        {
            get => pinSensors; set
            {
                pinSensors = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
		{
            new Microsoft.Maui.Controls.Command(async () =>
            {
                await GetData();

            }).Execute(null);
        }

        public Polyline polylineZone1 = new Polyline();
        
        public void CreateSensorsPolyline()
        {
            polylineZone1.StrokeColor = Color.FromArgb("#72A6B5");
            polylineZone1.StrokeWidth = 4;
            foreach (var item in PinSensors)
            {
                polylineZone1.Add(item.Location);
            }
        }


        #region hardcoded

        public List<HistoricalSearch> hardcodedHSearch = new List<HistoricalSearch>();
        public List<Leaks> hardcodedLeaks = new List<Leaks>();

        public void GetHardCodedHS()
        {
            hardcodedHSearch = new List<HistoricalSearch>
            {
                new HistoricalSearch{Date = DateTime.Now.AddDays(-1), SensorID = 01},
                new HistoricalSearch{Date = DateTime.Now.AddDays(-2), SensorID = 02},
                new HistoricalSearch{Date = DateTime.Now.AddDays(-3), SensorID = 03},
                new HistoricalSearch{Date = DateTime.Now.AddDays(-4), SensorID = 04}
            };
        }

        public void GetHardCodedLeaks()
        {
            hardcodedLeaks = new List<Leaks>
            {
                new Leaks{Date = DateTime.Now, Probability = 8, SensorID = 01},
                new Leaks{Date = DateTime.Now.AddDays(-1), Probability = 40, SensorID = 02},
                new Leaks{Date = DateTime.Now.AddDays(-1).AddHours(5), Probability = 12, SensorID = 03},
                new Leaks{Date = DateTime.Now.AddDays(-1).AddHours(3), Probability = 77, SensorID = 04}
            };
        }

        #endregion hardcoded

        

        public async Task GetData()
        {
            await GetSensorsData();
            await GetHistoricalSearchData(DateTime.Now.AddDays(-10), DateTime.Now);
            await GetLeaksData(DateTime.Now.AddDays(-10), DateTime.Now);
        }

        public async Task GetSensorsData()
        {
            string url = $"http://lb-reader-param-377588952.us-east-1.elb.amazonaws.com/getCurrentState";
            var service = new HttpHelper<SensorsDTOList>();
            var data = await service.GetRestServiceDataAsync(url);
            if (data != null)
            {
                foreach (var item in data.sensors)
                {
                    Pin pin = new Pin
                    {
                        Label = "Sensor ID: " + item.id.ToString(),
                        Address = "Area: " + item.area + "\n Flujo de corriente: " + item.currentFlow.ToString(),
                        Type = PinType.Place,
                        Location = new Location(item.localization.latitude, item.localization.longitude)
                    };
                    PinSensors.Add(pin);
                }
                CreateSensorsPolyline();
            }
        }

        
        public async Task GetHistoricalSearchData(DateTime start, DateTime end)
        {
            HsList.Clear();
            DateTimeOffset startOffset = new DateTimeOffset(start);
            long startTimestamp = startOffset.ToUnixTimeSeconds();

            DateTimeOffset endOffset = new DateTimeOffset(end);
            long endTimestamp = endOffset.ToUnixTimeSeconds();

            string url = $"http://lb-reader-param-377588952.us-east-1.elb.amazonaws.com/getHistoricalData?start={startTimestamp}&end={endTimestamp}";
            var service = new HttpHelper<HistoricalSearchDTO>();
            var data = await service.GetRestServiceDataAsync(url);
            if (data != null)
            {
                foreach (var item in data.sensors)
                {
                    HsList.Add(item);
                }
            }
        }

        public async Task GetLeaksData(DateTime start, DateTime end)
        {
            LeaList.Clear();
            DateTimeOffset startOffset = new DateTimeOffset(start);
            long startTimestamp = startOffset.ToUnixTimeSeconds();

            DateTimeOffset endOffset = new DateTimeOffset(end);
            long endTimestamp = endOffset.ToUnixTimeSeconds();
            #region test
            //test
            //DateTimeOffset startDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(startTimestamp);
            //DateTime startDateTime = startDateTimeOffset.UtcDateTime;
            //DateTimeOffset endDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(endTimestamp);
            //DateTime endDateTime = endDateTimeOffset.UtcDateTime;
            #endregion test
            string url = $"http://lb-reader-param-377588952.us-east-1.elb.amazonaws.com/getLeaks?start={startTimestamp}&end={endTimestamp}";
            var service = new HttpHelper<LeakDTO>();
            var data = await service.GetRestServiceDataAsync(url);
            if (data != null)
            {
                foreach (var item in data.leaks)
                {
                    LeaList.Add(item);
                }
            }
        }
        
    }
}

