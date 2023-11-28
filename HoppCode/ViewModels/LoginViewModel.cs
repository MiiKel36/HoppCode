using Firebase.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace HoppCode.ViewModels
{
    // Sistema para tratar os eventos da página de login
    internal class LoginViewModel : INotifyPropertyChanged
    {
        public string webApiKey = "AIzaSyB1m5xiuM-tOk0GUHnhrcJ2uVmkJr1ogwE"; // Não é dado sensível 👍

        private INavigation _navigation;
        private string loginemail;
        private string loginsenha;

        // Detecta mudanças nos campos e altera as variáveis de acordo V
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public string LoginEmail
        {
            get => loginemail; set
            {
                loginemail = value;
                RaisePropertyChanged("LoginEmail");
            }
        }
        public string LoginSenha
        {
            get => loginsenha; set
            {
                loginsenha = value;
                RaisePropertyChanged("LoginSenha");
            }
        }
        //-------------------------------------------------------------^

        public Command HLCadastrar { get; }
        public Command BtnLogin { get; }

        public LoginViewModel(INavigation navigation)
        {
            this._navigation = navigation;

            // Registra os comandos possíveis da página
            HLCadastrar = new Command(HLCadastrarTappedAsync);
            BtnLogin = new Command(BtnLoginTappedAsync);
        }

        private async void HLCadastrarTappedAsync(object obj)
        {
            // Envia a tela de cadastro para a primeira camada
            await this._navigation.PushAsync(new Pages.CadastroPage());
        }

        private async void BtnLoginTappedAsync(object obj)
        {
            // Estabelece conexão com o Firebase
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(LoginEmail, LoginSenha);

                // Adquirir dados do usuário para outras funções
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("FreshFirebaseToken", serializedContent);

                // Envia a tela principal para a primeira camada
                await this._navigation.PushAsync(new Pages.ClassesPage());
            }
            catch (FirebaseAuthException ex)
            {
                // Retira o "código" de erro do JSON
                var errorJSON = JObject.Parse(ex.ResponseData);
                var errorMessage = errorJSON["error"]["message"].ToString();

                // Mostra um aviso preparado para esse erro e retorna
                if (errorMessage == "INVALID_EMAIL")
                {
                    await App.Current.MainPage.DisplayAlert("ERRO!", "O email fornecido é inválido.", "OK");
                    return;
                }

                if (errorMessage == "MISSING_PASSWORD")
                {
                    await App.Current.MainPage.DisplayAlert("ERRO!", "A senha está faltando!", "OK");
                    return;
                }

                if (errorMessage == "INVALID_LOGIN_CREDENTIALS")
                {
                    await App.Current.MainPage.DisplayAlert("ERRO!", "O sistema não encontrou um usuário com essas credenciais. Verifique o que digitou e tente novamente.", "OK");
                    return;
                }

                // Se de alguma forma o erro passou, manda pro debugger
                throw;
            }
        }
    }
}
