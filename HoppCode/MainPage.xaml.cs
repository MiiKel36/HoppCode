namespace HoppCode;
using HoppCode.Classes;
using Newtonsoft.Json;

public partial class MainPage : ContentPage
{
    
    public MainPage()
    {
        InitializeComponent();

        // funções para verificar e criar pasta de arquivo local
        CreateLocalStorageFolder createFolder = new CreateLocalStorageFolder();
        createFolder.CreateStorage();

        GetInfoPerfil();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("ClassesPage");
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("LoginPage");
    }

    private void GetInfoPerfil()
    {
        // Acessa as informações que já coletamos do usuário
        var infoUsuario = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
        EmailSessao.Text += infoUsuario.User.Email;
    }
}
