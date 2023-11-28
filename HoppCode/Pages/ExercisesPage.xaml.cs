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

public partial class ExercisesPage : ContentPage
{
    /*
        LIST TO DO:

            Modfy the json{
                Adicionar parte de exercicios, dentro dos exercicios será uma string, 
                caso tenha mais de uma frase, era separado por "@"
            };
            
            Adicionar WebView para editor de texto;
            Adicionar Api para compilar código;
            Adicionar objeto que lê o json e volta frames contendo os textos;
     */
    List<string> readLineList = new List<string>();
    string classe, aula;
    public ExercisesPage(string classeFromOtherPage, string aulaFromOtherPage)
    {
        InitializeComponent();
        FunçãoInicial();
        classe = classeFromOtherPage;
        aula = aulaFromOtherPage;

    }

    private async void FunçãoInicial()
    {
        //Cria objeto do identificar aula
        IdentificarAulaOuExercicioPage identificarAulaOuExercicio = new IdentificarAulaOuExercicioPage();

        //Contem o valor "Classe" e "Aulas" do json
        string[] ClasseAula = await identificarAulaOuExercicio.JsonReadReturnClasseAula();

        ExercisesClass exercisesClass = new ExercisesClass();
        var readJsonAndReturStyle = exercisesClass.ReadJsonAndReturnStyle(ClasseAula[0], ClasseAula[1]);
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
        //Código do webview
        string editorCode = await editorWebView.EvaluateJavaScriptAsync(@"editor.getValue();");
        string code = returnCode(editorCode);

        stackInputs.Clear();
        
        //Rtorna quantas Console.ReadLine() tem
        int numOfReadLines = verificarQuantasReadLineTem(code);

        //Adiciona os entry com base em quantos Console.ReadLine() tem no código
        if(numOfReadLines > 0)
        {
            List<Entry> entrys = ReturnEntryIput(numOfReadLines);

            foreach (Entry entry in entrys)
            {
                stackInputs.Add(entry);
            }

            TurnIputSectionVisible();
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
        string editorCode = await editorWebView.EvaluateJavaScriptAsync(@"editor.getValue();");
        string code = returnCode(editorCode);
        int numOfReadLines = verificarQuantasReadLineTem(code);
                      
        for (int i = 1; i <= numOfReadLines; i++)
        {
            var stackLayout = this.FindByName<StackLayout>("stackInputs");

            // Substitua "entradaDesejada" pelo ClassId da entrada que você está procurando
            string classIdDesejado = $"entry-{i}";

            // Procura a entrada no StackLayout pelo ClassId
            var entradaDesejada = stackLayout.Children.FirstOrDefault(c => (c is Entry entry) && entry.ClassId == classIdDesejado) as Entry;

            if (entradaDesejada != null)
            {
                lblOutput.Text = $"-- O código esta sendo executado --";
                // Obtém o texto do Entry
                string textoDoEntry = entradaDesejada.Text;

                //Adiciona texto para a list de inputs
                bool verificadorUltimoEntry = (numOfReadLines == i);
                readLineList.Add(verificadorUltimoEntry ? textoDoEntry : textoDoEntry + "\\n");
                
            }

        }
        string codeInput = string.Join("", readLineList);
        SendCodeToApi(code, codeInput);
        
        readLineList.Clear();

    }

    public async void SendCodeToApi(string code, string input)
    {
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

        var user = JsonSerializer.Deserialize<ResponseData>(responseString);
        
        //Verificação de erros
        if (user.error != "")
        {
            lblOutput.Text = $"Seu código retornou um erro: \n{responseString}";
        }
        else
        {
            lblOutput.Text = user.output;
        }
    }
    public List<Entry> ReturnEntryIput(int numOfReadLines)
    {   List<Entry> entryList = new List<Entry>();
     
        for (int i = 1; i <= numOfReadLines; i++)
        {
            Entry entry = new Entry()
            {
                BackgroundColor = Colors.Blue,
                ClassId = $"entry-{i}",
                
            };
            entryList.Add(entry);
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
    static string returnCode(string editorCode)
    {
        string codigoSemduasBarras = "";
        for (int i = 0; i < editorCode.Length - 1; i++)
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
        string codigoFinal = codigoComMenorQue.Substring(0, codigoComMenorQue.Length - 1);

        return codigoFinal;
    }
    //Teste para usar o readLine
    private string returnAnswerWithOutput()
    {
        string outputJson = "{readLineArray[0]} kkk mais um olha {readLineArray[1]}";
        // Use alguma lógica para encontrar e substituir os marcadores de posição pelos valores do array.
        for (int i = 0; i < readLineList.Count; i++)
        {
            string marcador = $"{{readLineArray[{i}]}}";
            outputJson = outputJson.Replace(marcador, readLineList[i]);
        }
        return outputJson;
    }

    //Usado quando se clica em rodar o códio
    public void TurnIputSectionVisible()
    {
        scrollViewIputSection.IsVisible = true;
    }
    //Usado quando se clica no X do IputSection
    public void TurnIputSectionInvisible(object sender, EventArgs e)
    {
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