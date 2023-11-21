using HoppCode.Classes;

namespace HoppCode.Pages;

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
    public ExercisesPage()
    {
        InitializeComponent();

        //Serve para colocar "{" no botão que adiiona { ao código
        btnEspecialCharacter.Text = "{";

        //Cria objeto do identificar aula
        IdentificarAulaOuExercicioPage identificarAulaOuExercicio = new IdentificarAulaOuExercicioPage();

        //Contem o valor "Classe" e "Aulas" do json
        string[] ClasseAula = identificarAulaOuExercicio.JsonReadReturnClasseAula();

        ExercisesClass exercisesClass = new ExercisesClass();
        var coco = exercisesClass.ReadJsonAndReturStyle(ClasseAula[0], ClasseAula[1]);

    }


    //Função onde adiciona os caracere especiais ao codigo
    private void AddCharacter(object sender, EventArgs e)
    {
        /* Button btn = sender as Button;
         string CodeText = "" + editorWebView.Text;
         int MousePos = editorWebView.CursorPosition;

         switch (btn.Text)
         {
             case "TAB":
                 CodeText = CodeText.Insert(MousePos, "     ");
                 editorWebView.Text = CodeText;
                 editorWebView.CursorPosition = MousePos + 5;
                 break;

             default:
                 CodeText = CodeText.Insert(MousePos, btn.Text);
                 editorWebView.Text = CodeText;
                 editorWebView.CursorPosition = MousePos + 1;
                 break;
    }*/

    }
    private static readonly HttpClient client = new HttpClient();
    private async void RunCode(object sender, EventArgs e)
    {/*
        var values = new Dictionary<string, string>
      {
        { "code", @"using System;
                    public class Program{
                        public static void Main(){
                           Console.WriteLine(""bictor, bictorr, bicotrrrc"");
                 }}" },
          { "language",  "cs"},
         { "input", "" }
       };
        
        var content = new FormUrlEncodedContent(values);
        var response = await client.PostAsync("https://api.codex.jaagrav.in", content);
        var responseString = await response.Content.ReadAsStringAsync();

        var user = JsonConvert.DeserializeObject<ResponseData>(responseString);

        lblOutPut.Text = user.output;
    */
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
}