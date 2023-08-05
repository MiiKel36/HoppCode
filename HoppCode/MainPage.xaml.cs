namespace HoppCode;
using Newtonsoft;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;


public partial class MainPage : ContentPage
{
 
    public MainPage()
    {
        InitializeComponent();

        //cria objeto do CreateButtons
        CreateButtons classButons = new CreateButtons();

        //a var buttons vira uma list contendo os objetos dos botões
        dynamic buttons = classButons.CreatingButtonsToPage();

        foreach (var button in buttons)
        {   //adiciona no stackLayout os botões com o valor dentro do button
            stackClasses.Add(button);
        }

    }

}






