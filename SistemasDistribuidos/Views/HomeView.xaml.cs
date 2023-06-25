using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using SistemasDistribuidos.Models;
using SistemasDistribuidos.ViewModels;

namespace SistemasDistribuidos.Views;

public partial class HomeView : TabbedPage
{
    private HomeViewModel model = null;

    public HomeView()
	{
		InitializeComponent();
        BindingContext = new HomeViewModel();
        model = BindingContext as HomeViewModel;

        var location = new Location(-34.887774, -56.157419);
		var mapSpan = new MapSpan(location, 0.01, 0.01);
		MyMap.MoveToRegion(mapSpan);

        LeakStartDate.Date = DateTime.Now.AddDays(-10);
        HsStartDate.Date = DateTime.Now.AddDays(-10);

        MyMap.MapElements.Add(model.polylineZone1);
        HSearchListView.ItemsSource = model.HsList;
        LeaksListView.ItemsSource = model.LeaList;
        new Microsoft.Maui.Controls.Command(async () =>
        {
            await Task.Delay(1000);
            refreshMap();
        }).Execute(null);

    }

    void testBtn_Clicked(System.Object sender, System.EventArgs e)
    {
		Navigation.PushAsync(new TestPage());
    }

    void LeaksPage_Appearing(System.Object sender, System.EventArgs e)
    {
        this.Title = "Fugas";
    }

    void HistoricalSearchPage_Appearing(System.Object sender, System.EventArgs e)
    {
        this.Title = "Búsqueda histórica";
    }

    void MapPage_Appearing(System.Object sender, System.EventArgs e)
    {
        this.Title = "Mapa de sensores";
    }

    void refreshMap()
    {
        foreach (var item in model.PinSensors)
        {
            MyMap.Pins.Add(item);
        }
    }

    void LeaksBtn_Clicked(System.Object sender, System.EventArgs e)
    {
        model.GetLeaksData(LeakStartDate.Date, LeakEndDate.Date);
        
    }

    void HsBtn_Clicked(System.Object sender, System.EventArgs e)
    {
        model.GetHistoricalSearchData(HsStartDate.Date, HsEndDate.Date);
    }
}
