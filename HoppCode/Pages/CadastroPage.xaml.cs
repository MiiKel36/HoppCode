using HoppCode.ViewModels;

namespace HoppCode.Pages;

public partial class CadastroPage : ContentPage
{
	public CadastroPage()
	{
		InitializeComponent();
		BindingContext = new CadastroViewModel(Navigation);
    }
}