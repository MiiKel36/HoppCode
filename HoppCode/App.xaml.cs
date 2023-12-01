using HoppCode.Pages;
using Newtonsoft.Json;
namespace HoppCode;

public partial class App : Application
{
	public App()
	{
        InitializeComponent();
        var infoUsuario = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
        if (infoUsuario != null)
        {
            // Aplicativo inicia na tela de classes (já fez login)
            MainPage = new NavigationPage(new ClassesPage());
        } 
        else
        // Aplicativo inicia na tela de login
        MainPage = new NavigationPage(new LoginPage());
	}
}
