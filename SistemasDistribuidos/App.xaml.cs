namespace SistemasDistribuidos;

public partial class App : Application
{
    public static string Token;
	public App()
	{
		InitializeComponent();

        //MainPage = new MainPage();
        NavigationPage navPage = new NavigationPage(new MainPage());
        MainPage = navPage;
    }
}

