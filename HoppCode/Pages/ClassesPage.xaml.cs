using HoppCode.Classes;
using HoppCode.Pages;
using HoppCode.ViewModels;
using Newtonsoft.Json;

namespace HoppCode.Pages;

public partial class ClassesPage : FlyoutPage
{
    public ClassesPage()
	{
		InitializeComponent();
        GetInfoPerfil();
        AddButtonsToStack();
    }

    private async void LogoutBtn_Clicked(object sender, EventArgs e)
    {
        bool logoutResposta = await App.Current.MainPage.DisplayAlert("CONFIRMAR AÇÃO", "Tem certeza que deseja sair?", "Sim", "Não");
        if (logoutResposta)
        {
            Preferences.Remove("FreshFirebaseToken", "");
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }

    public async void AddButtonsToStack()
    {
        //Cria objeto do CreateButtons
        CreateButtonsClasses classButons = new CreateButtonsClasses();

        //A var buttons vira uma list contendo os objetos dos botões
        dynamic buttons = await classButons.CreatingButtonsToPage();

        foreach (Button button in buttons)
        {
            button.Clicked += (sender, e) =>
            {
                Button clickedButton = (Button)sender;
                string ButtonId = clickedButton.ClassId;

                // Executa a função
                ChangePage(ButtonId);
            };

            //Adiciona no stackLayout os botões com o valor dentro do button
            stackClasses.Add(button);
        }
    }

    private void GetInfoPerfil()
    {
        // Acessa as informações que já coletamos do usuário
        var infoUsuario = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
        EmailSessao.Text += infoUsuario.User.Email;
    }

    //Daixa o botão de voltar não funcional
    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    public void ChangePage(string ClassesEscolhida)
    {
        Navigation.PushAsync(new AulasPage(ClassesEscolhida));
    }
}