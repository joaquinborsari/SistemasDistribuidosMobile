using SistemasDistribuidos.Models;
using SistemasDistribuidos.ViewModels;
using SistemasDistribuidos.Views;

namespace SistemasDistribuidos;

public partial class MainPage : ContentPage
{
    private LoginViewModel model = null;

    public MainPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
        model = BindingContext as LoginViewModel;
    }

    void registerBtn_Clicked(System.Object sender, System.EventArgs e)
    {
    }

    void logInButton_Clicked(System.Object sender, System.EventArgs e)
    {
        login();
    }

    void login()
    {
        //requestPermissionsAsync();
        if (user != null & password != null & user.Text != null & password.Text != null & user.Text != "" && password.Text != "")
        {
            //int userID = 0;
            if (model.ValidateCredentials(user.Text.ToLower(), password.Text))
            {
                //SecureStorage.SetAsync(nameof(App.Token), model.Token);
                //Para luego utilizar en las requests
                //SecureStorage.GetAsync(nameof(App.Token));
                //Preferences.Set("user_id", userID.ToString());
                Navigation.PushAsync(new HomeView());
            }
            else
            {
                DisplayAlert("Error", "Credenciales incorrectas", "Ok");
                password.Text = String.Empty;

            }
        }
        else
        {
            DisplayAlert("Error", "Ingrese usuario y contraseña", "Ok");
        }
    }

    /*
    async Task requestPermissionsAsync()
    {
        var status = PermissionStatus.Unknown;
        status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status == PermissionStatus.Granted)
            return;

        if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
        {
            await Shell.Current.DisplayAlert("Needs permissions", "BECAUSE!!!", "OK");
        }

        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        if (status != PermissionStatus.Granted)
            await Shell.Current.DisplayAlert("Permission required", "Location permission is required for bluetooth scanning.", "OK");
    }
    */
}


