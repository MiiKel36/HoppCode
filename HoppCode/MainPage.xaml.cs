namespace HoppCode;

using HoppCode.Classes;
using HoppCode.Pages;
using Newtonsoft.Json;

public partial class MainPage : FlyoutPage
{
    public string webApiKey = "AIzaSyB1m5xiuM-tOk0GUHnhrcJ2uVmkJr1ogwE";


    public MainPage()
    {
        InitializeComponent();
        GetInfoPerfil();

        
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.ClassesPage());      
    }

    private async void LogoutBtn_Clicked(object sender, EventArgs e)
    {
        bool logoutResposta = await App.Current.MainPage.DisplayAlert("CONFIRMAR AÇÃO", "Tem certeza que deseja sair?", "Sim", "Não");
        if (logoutResposta)
        {
            await Navigation.PopToRootAsync();
        }
    }

    private void GetInfoPerfil()
    {
        // Acessa as informações que já coletamos do usuário
        var infoUsuario = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
        EmailSessao.Text += infoUsuario.User.Email;
    }
}
