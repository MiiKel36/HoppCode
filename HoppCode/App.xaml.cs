using HoppCode.Pages;
namespace HoppCode;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		// Aplicativo inicia na tela de login
		MainPage = new NavigationPage(new Pages.LoginPage());
	}
}
