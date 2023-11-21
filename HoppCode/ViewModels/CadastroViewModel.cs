using Firebase.Auth;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace HoppCode.ViewModels
{
    // Sistema para tratar os eventos da página de cadastro
    internal class CadastroViewModel : INotifyPropertyChanged
    {
        public string webApiKey = "AIzaSyB1m5xiuM-tOk0GUHnhrcJ2uVmkJr1ogwE"; // Não é dado sensível 👍

        private INavigation _navigation;
        private string email;
        private string senha;
        private string confirmasenha;

        // Detecta mudanças nos campos e altera as variáveis de acordo V
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public string Email { get => email; set {
                email = value;
                RaisePropertyChanged("Email");
            }
        }
        public string Senha { get => senha; set {
                senha = value;
                RaisePropertyChanged("Senha");
            }
        }
        public string ConfirmaSenha { get => confirmasenha; set {
                confirmasenha = value;
                RaisePropertyChanged("ConfirmaSenha");
            }
        }
        //-------------------------------------------------------------^

        public Command CadastrarUsuario { get; }

        public CadastroViewModel(INavigation navigation) 
        {
            this._navigation = navigation;

            // Registra o único comando possível da página (cadastrar-se)
            CadastrarUsuario = new Command(CadastrarUsuarioTappedAsync);
        }

        private async void CadastrarUsuarioTappedAsync(object obj)
        {
            // Tratativa de erro básica
            if (Senha.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert("ERRO!", "A sua senha precisa conter ao menos 6 caracteres.", "OK");
                return;
            }

            if (Senha != ConfirmaSenha)
            {
                await App.Current.MainPage.DisplayAlert("ERRO!", "O campo \"Senha\" e o campo \"Confirmar Senha\" não são iguais!", "OK");
                return;
            }


            // Estabelece conexão com o Firebase
            var AuthProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                var auth = await AuthProvider.CreateUserWithEmailAndPasswordAsync(Email, Senha);
                string token = auth.FirebaseToken;
                if (token != null)
                    await App.Current.MainPage.DisplayAlert("SUCESSO!", "Usuário cadastrado com êxito. Agora, faça login!", "OK");

                // Redireciona o usuário de volta a página de login
                await this._navigation.PopAsync();
            }
            catch (FirebaseAuthException ex)
            {
                // Tratativa de erro mais avançada (verificação de validade de email, deixa que o próprio Firebase resolve isso tô fora)
                // Retira o "código" de erro do JSON
                var errorJSON = JObject.Parse(ex.ResponseData);
                var errorMessage = errorJSON["error"]["message"].ToString();

                // Mostra um aviso preparado para esse erro e retorna
                if (errorMessage == "EMAIL_EXISTS")
                {
                    await App.Current.MainPage.DisplayAlert("ERRO!", "Esse email já está em uso.", "OK");
                    return;
                }

                if (errorMessage == "INVALID_EMAIL")
                {
                    await App.Current.MainPage.DisplayAlert("ERRO!", "O email fornecido é inválido.", "OK");
                    return;
                }

                // Se de alguma forma o erro passou, manda pro debugger
                throw;
            }
        }
    }
}
