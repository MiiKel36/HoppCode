using HoppCode.Classes;

namespace HoppCode.Pages;

using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Newtonsoft.Json;
using Microsoft.Maui.Platform;

public partial class ExercisesPage : ContentPage
{

    List<string> readLineList = new List<string>();
    string classe, aula;

    string exercicioOutput, exercicioSetup;
    public ExercisesPage(string classeFromOtherPage, string aulaFromOtherPage)
    {
        InitializeComponent();
        FunçãoInicial();

        classe = classeFromOtherPage;
        aula = aulaFromOtherPage;

        PuxaColocaTextoExercicio();
        PuxaOutputExercicio();


    }

    private async void FunçãoInicial()
    {
        //Cria objeto do identificar aula
        IdentificarAulaOuExercicioPage identificarAulaOuExercicio = new IdentificarAulaOuExercicioPage();

        //Contem o valor "Classe" e "Aulas" do json
        string[] ClasseAula = await identificarAulaOuExercicio.JsonReadReturnClasseAula();

        ExercisesClass exercisesClass = new ExercisesClass();
        var readJsonAndReturStyle = exercisesClass.ReadJsonAndReturnStyle(ClasseAula[0], ClasseAula[1]);

        editorWebView.Navigated += async (o, s) => {

            string setupJsonCode = await PuxaSetupExercicio();
            if(setupJsonCode == "null")
            {
                setupJsonCode = "//Escreva seu código aqui";
            }

            string rootCode = @"using System;\npublic class Exercicio\n{\npublic static void Main(string[] args)\n{\n"+ setupJsonCode + "\\n\\n}\\n}";

            editorWebView.Eval($"SetTextOnCodeEditor(\"{rootCode}\");");
        };
    }

    public async Task<string> PuxaSetupExercicio()
    {
        CreateLocalStorageFolder createFolder = new CreateLocalStorageFolder();
        string jsonOfAulas = await createFolder.PushAulaJson();

        dynamic objJson = JsonConvert.DeserializeObject(jsonOfAulas);

        string textoExercicio = objJson.cSharp.classes[Convert.ToInt32(classe)].aulas[Convert.ToInt32(aula)].Setup;

        return textoExercicio;
    }

    private void ChangePage(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        switch (btn.ClassId)
        {
            case "exercisePart":
                exercisePart.IsVisible = true;
                codePart.IsVisible = false;
                outputPart.IsVisible = false;

                btnExercisePart.Background = Color.FromRgb(73, 46, 168);
                btnCodePart.Background = Color.FromRgb(48, 29, 114);
                btnOutputPart.Background = Color.FromRgb(48, 29, 114);

                break;
            case "codePart":
                exercisePart.IsVisible = false;
                codePart.IsVisible = true;
                outputPart.IsVisible = false;

                btnExercisePart.Background = Color.FromRgb(48, 29, 114);
                btnCodePart.Background = Color.FromRgb(73, 46, 168);
                btnOutputPart.Background = Color.FromRgb(48, 29, 114);
                break;
            case "outputPart":
                exercisePart.IsVisible = false;
                codePart.IsVisible = false;
                outputPart.IsVisible = true;

                btnExercisePart.Background = Color.FromRgb(48, 29, 114);
                btnCodePart.Background = Color.FromRgb(48, 29, 114);
                btnOutputPart.Background = Color.FromRgb(73, 46, 168);
                break;
        }
    }

    
    private static readonly HttpClient client = new HttpClient();
    /*
     Organizar pensamento:
        -criar uma função que retorna um Entry onde o class id Representa o numero no laço de repetição
        -para pegar os valores dentro dos entry,
     */
    private async void RunCode(object sender, EventArgs e)
    {
        //desativa o botão de rodar código
        btnRun.BackgroundColor = Color.FromRgb(58, 36, 112);
        btnRun.IsEnabled = false;

        //Código do webview
        string editorCode = await editorWebView.EvaluateJavaScriptAsync(@"editor.getValue();");
        string code = returnCode(editorCode);

        stackInputs.Clear();
        
        //Rtorna quantas Console.ReadLine() tem
        int numOfReadLines = verificarQuantasReadLineTem(code);

        //Adiciona os entry com base em quantos Console.ReadLine() tem no código
        if(numOfReadLines > 0)
        {
            List<Border> entrys = ReturnEntryIput(numOfReadLines);

            foreach (Border entry in entrys)
            {
                stackInputs.Add(entry);
            }

            scrollViewIputSection.IsVisible = true;
        }
        else 
        {
            
            ExecuteCode();
        }
    }
    //Executa o código sem Console.ReadLine()
    public async void ExecuteCode()
    {
        string editorCode = await editorWebView.EvaluateJavaScriptAsync(@"editor.getValue();");
        string code = returnCode(editorCode);

        SendCodeToApi(code, "");
    }
    //Executa o codigo caso tenha Console.ReadLine()
    public async void ExecuteCodeWithInput(object sender, EventArgs e)
    {
        readLineList.Clear();

        string editorCode = await editorWebView.EvaluateJavaScriptAsync(@"editor.getValue();");
        string code = returnCode(editorCode);
        int numOfReadLines = verificarQuantasReadLineTem(code);
                      
        for (int i = 1; i <= numOfReadLines; i++)
        {
            var stackLayout = this.FindByName<StackLayout>("stackInputs");

            // Substitua "entradaDesejada" pelo ClassId da entrada que você está procurando
            string classIdDesejado = $"entry-{i}";

            // Procura a entrada no StackLayout pelo ClassId
            var BordaEntradaDesejada = stackLayout.Children.FirstOrDefault(c => (c is Border border) && border.ClassId == classIdDesejado) as Border;
            var entradaDesejada = BordaEntradaDesejada.Content as Entry;

            if (entradaDesejada != null)
            {
                lblOutput.Text = $"-- O código esta sendo executado --";
                // Obtém o texto do Entry
                string textoDoEntry = entradaDesejada.Text;

                //Adiciona texto para a list de inputs
                bool verificadorUltimoEntry = (numOfReadLines == i);
                readLineList.Add(verificadorUltimoEntry ? textoDoEntry : textoDoEntry + "\n");
                
            }

        }
        string codeInput = string.Join("", readLineList);
        SendCodeToApi(code, codeInput);
        
        scrollViewIputSection.IsVisible = false;

    }

    public async void SendCodeToApi(string code, string input)
    {
        lblOutput.Text = $"-- O código esta sendo executado --";
        //Criao uma variavel contendo os valores que serão enviados para a api
        var values = new Dictionary<string, string>
      {
        { "code", code},
        { "language",  "cs"},
        { "input", input}
      };
        
        //Faz o envio para a api
        var content = new FormUrlEncodedContent(values);
        var response = await client.PostAsync("https://codex-api-7q3s.onrender.com/", content);
        var responseString = await response.Content.ReadAsStringAsync();

        var user = JsonConvert.DeserializeObject<ResponseData>(responseString);
        string respostaUsuario = user.output;
        string respostaCortadaUsuario = "";
        //Verificação de erros

        if (user.error != "")
        {
            lblOutput.Text = $"SEU CÓDIGO RETORNOU UM ERRO: \n{user.error}";
        }
        else 
        {
            lblOutput.Text = respostaUsuario;
            if(respostaUsuario != "")
            {
                respostaCortadaUsuario = respostaUsuario.Substring(0, respostaUsuario.Length - 1);
            }           
        }
        
        //Realiza a verificação se a resposta no json e a do usuário esta certo
        string respostaCerta = await returnAnswerWithInput();
        //string respostaUsuario = user.output;

        if (respostaCerta == respostaCortadaUsuario || respostaCerta == "null")
        {
            frameDeVerificacao.IsVisible = true;
            lblDeVerificacao.Text = "Parabéns, Você conseguiu!";
            lblDeVerificacao.TextColor = Color.FromRgb(0, 187, 69);
            btnPassarPraProxima.IsVisible = true;
        }
        else
        {
            frameDeVerificacao.IsVisible = true;
            lblDeVerificacao.Text = "Hmm, sua resposta não está correta...";
            lblDeVerificacao.TextColor = Color.FromRgb(248, 49, 49);
            btnPassarPraProxima.IsVisible = false;
        }

        

        btnRun.BackgroundColor = Color.FromRgb(99, 50, 155); 
        btnRun.IsEnabled = true;
    }
    public List<Border> ReturnEntryIput(int numOfReadLines)
    {   List<Border> entryList = new List<Border>();
     
        for (int i = 1; i <= numOfReadLines; i++)
        {
            Border border = new Border
            {
                BackgroundColor = Color.FromRgb(49, 24, 80),
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(10, 10, 10, 10)
                },
                ClassId = $"entry-{i}",

            };

            Entry entry = new Entry()
            {
                BackgroundColor = Color.FromRgb(49, 24, 80),
                
                ClassId = $"entry-{i}",
                
            };

            border.Content = entry;
            entryList.Add(border);
        }

        return entryList;
    }
    public int verificarQuantasReadLineTem(string code) 
    {
        string findOnCode = "Console.ReadLine()";

        int quantidade = 0;
         
        for (int i = 0; i < code.Length; i++)
        {   try
            {
                if (code.Substring(i, findOnCode.Length) == findOnCode)
                {
                    quantidade++;
                }
            }
            catch{}
        }
        return quantidade;
    }
    //Tratação de string para evitar erros
    public string returnCode(string editorCode)
    {
        
        string codigoSemduasBarras = "";
        for (int i = 0; i < editorCode.Length; i++)
        {
            try
            {
                if (editorCode.Substring(i, 2) != "\\\\")
                {
                    codigoSemduasBarras += editorCode[i];
                }
            }
            catch
            {
                codigoSemduasBarras += editorCode[i];
            }
        }
        string codigoSemBarraN = codigoSemduasBarras.Replace("\\n", "\n");
        string codigoSemBarraAspas = codigoSemBarraN.Replace("\\\"", "\"");
        string codigoComMenorQue = codigoSemBarraAspas.Replace("\\u003C", "<");
        string codigoFinal = codigoComMenorQue;        
        
        return codigoFinal;
    }


    //Teste para usar o readLine
    private async Task<string> returnAnswerWithInput()
    {
        string outputJson = await PuxaOutputExercicio();
        // Use alguma lógica para encontrar e substituir os marcadores de posição pelos valores do array.
        for (int i = 0; i < readLineList.Count; i++)
        {
            string marcador = $"readLineArray[{i}]";
            outputJson = outputJson.Replace(marcador, readLineList[i].Replace("\n",""));
        }
        return outputJson;
    }
    public async Task<string> PuxaOutputExercicio()
    {
        CreateLocalStorageFolder createFolder = new CreateLocalStorageFolder();
        string jsonOfAulas = await createFolder.PushAulaJson();

        dynamic objJson = JsonConvert.DeserializeObject(jsonOfAulas);

        string textoExercicio = objJson.cSharp.classes[Convert.ToInt32(classe)].aulas[Convert.ToInt32(aula)].Output;

        return textoExercicio;
    }
    public async void PuxaColocaTextoExercicio()
    {
        CreateLocalStorageFolder createFolder = new CreateLocalStorageFolder();
        string jsonOfAulas = await createFolder.PushAulaJson();

        dynamic objJson = JsonConvert.DeserializeObject(jsonOfAulas);

        string textoExercicio = objJson.cSharp.classes[Convert.ToInt32(classe)].aulas[Convert.ToInt32(aula)].Texto;
        lblExercicioTxt.Text = textoExercicio;
    }

    public void NextPage(object sender, EventArgs e)
    {
        string proximaAula = (1 + Convert.ToInt32(aula)).ToString();

        //subAula.WriteJson(proximaAula.ToString());

        //Envia para AulasPage
        Navigation.PushAsync(new Pages.IdentificarAulaOuExercicio(classe, proximaAula));
    }
    public void TurnIputSectionInvisible(object sender, EventArgs e)
    {
        btnRun.BackgroundColor = Color.FromRgb(99, 50, 155);
        btnRun.IsEnabled = true;
        scrollViewIputSection.IsVisible = false;
    }
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AulasPage(classe));
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }


    class ResponseData
    {
        public long timeStamp { get; set; }
        public int status { get; set; }
        public string output { get; set; }
        public string error { get; set; }
        public string language { get; set; }
        public string info { get; set; }
    }

    


}