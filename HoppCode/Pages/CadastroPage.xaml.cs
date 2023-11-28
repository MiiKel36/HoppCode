using HoppCode.ViewModels;

namespace HoppCode.Pages;

public partial class CadastroPage : ContentPage
{
	public CadastroPage()
	{
		InitializeComponent();
		BindingContext = new CadastroViewModel(Navigation);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}