using HoppCode.ViewModels;

namespace HoppCode.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel(Navigation);
    }
}